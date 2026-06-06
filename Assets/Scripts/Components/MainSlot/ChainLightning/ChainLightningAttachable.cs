using System.Collections.Generic;
using UnityEngine;

public class ChainLightningAttachable : MainAttachableBase
{
    [Header("Dependencies")]
    [Tooltip("번개 발사·타게팅 기점 — 미할당 시 이 오브젝트 위치 사용")]
    [SerializeField] private Transform _firePoint;

    [Header("VFX")]
    [Tooltip("hop마다 두 점을 잇는 번개 줄기 — 풀링 없이 Instantiate 후 자동 소멸(TODO: §3 풀링)")]
    [SerializeField] private LightningBolt _boltPrefab;

    private ChainLightningBase _chain;

    // 발사·탐지 기점. FirePoint 미할당 시 어태치먼트 자체 위치로 폴백.
    private Vector3 Origin => _firePoint != null ? _firePoint.position : transform.position;

    // 타게팅 임시 버퍼 — 단일 플레이어 무기가 메인 스레드에서 동기 처리하므로 static 공유 안전(MachineGunBase·AircraftAttachable 선례).
    private const int k_targetBufferSize = 32;
    private static readonly Collider[] _targetBuffer = new Collider[k_targetBufferSize];
    private static readonly List<(float sqr, ShipBody body)> _candidates = new(k_targetBufferSize);
    private static readonly HashSet<ShipBody> _seen = new(k_targetBufferSize);
    private static readonly HashSet<ShipBody> _claimed = new(k_targetBufferSize);

    private float _fireTimer;

    public ChainLightningBase Chain => _chain;
    public override int Level => _chain?.Level ?? 0;
    public override bool CanUpgrade => _chain?.CanUpgrade ?? true;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);
        _chain = new Lv1_ChainLightning();
    }

    private void Update()
    {
        if (_chain == null) return;
        TickFire();
    }

    public override void Upgrade() => _chain = _chain.Upgrade();
    public override void WrapDouble() => _chain = new DoubleChainLightning(_chain);
    public override void WrapTriple() => _chain = new TripleChainLightning(_chain);

    private void TickFire()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer > 0f) return;

        AcquireTargets();
        if (_candidates.Count > 0) DistributeAndFire();

        _fireTimer = _data.Cooldown / Effective(StatType.FireRate, 1f);
    }

    // 모체 기준 사거리 내 적 ShipBody 수집. 이미 무적 상태인 적은 제외(이전 볼리 i-frame 잔여 → 때려도 낭비).
    private void AcquireTargets()
    {
        float range = Effective(StatType.Range, _data.MaxRange);
        Vector3 origin = Origin;
        int count = Physics.OverlapSphereNonAlloc(origin, range, _targetBuffer, _targetLayer);

        _candidates.Clear();
        _seen.Clear();
        for (int i = 0; i < count; i++)
        {
            var body = _targetBuffer[i].GetComponentInParent<ShipBody>();
            if (body == null) continue;
            if (body.IsInvincible || body.IsDestroyed) continue;
            if (!_seen.Add(body)) continue; // 동일 ShipBody 다중 콜라이더 dedupe
            float sqr = (body.transform.position - origin).sqrMagnitude;
            _candidates.Add((sqr, body));
        }
    }

    // 가닥별로 '서로 다른 시작 적'에서 연쇄를 시작해, 점프 반경 내 미점유 인접 적으로 전파.
    // 전역 claim으로 한 볼리 안에서 같은 적을 두 번 때리지 않아 무적시간 낭비를 차단(Aircraft 선례).
    private void DistributeAndFire()
    {
        float damage = Effective(StatType.Damage, _data.Damage);
        float jumpRange = Effective(StatType.AreaRadius, _data.AreaRadius);
        float jumpSqr = jumpRange * jumpRange;
        int jumps = _chain.JumpsPerStrand;
        _claimed.Clear();

        for (int s = 0; s < _chain.StrandCount; s++)
        {
            // 시작 노드: 발사 기점 기준 가장 가까운 미점유 적(반경 제한 없음 — 이미 사거리 내 후보).
            var node = NearestUnclaimed(Origin, float.MaxValue);
            if (node == null) return; // 고유 시작 적 소진 → 남은 가닥은 보류(중복=낭비라 쏘지 않음)

            Strike(node, damage);
            DrawBolt(Origin, node.transform.position); // 발사점(FirePoint) → 첫 적
            Vector3 prev = node.transform.position;

            // 연쇄: 직전 노드 기준 점프 반경 내 가장 가까운 미점유 적으로 전파.
            for (int j = 0; j < jumps; j++)
            {
                var next = NearestUnclaimed(prev, jumpSqr);
                if (next == null) break; // 반경 내 신규 적 없음 → 이 가닥 종료
                Strike(next, damage);
                DrawBolt(prev, next.transform.position); // 이전 적 → 다음 적
                prev = next.transform.position;
            }
        }
    }

    private void Strike(ShipBody body, float damage)
    {
        _claimed.Add(body);
        body.OnDamaged(damage);
        ParticlePoolRegistry.Get(ParticleKind.HitFlash).Play(body.transform.position);
    }

    // 두 월드 점을 잇는 번개 한 줄기 생성(부모 없음 — 함선이 움직여도 그 순간 좌표에 고정). 풀링 없음(TODO: §3).
    private void DrawBolt(Vector3 from, Vector3 to)
    {
        if (_boltPrefab == null) return;
        Instantiate(_boltPrefab).Draw(from, to);
    }

    // from 기준 가장 가까운 미점유 적. maxSqr 이내만 후보(시작 노드는 float.MaxValue로 제한 해제).
    private ShipBody NearestUnclaimed(Vector3 from, float maxSqr)
    {
        ShipBody best = null;
        float bestSqr = maxSqr;
        for (int i = 0; i < _candidates.Count; i++)
        {
            var body = _candidates[i].body;
            if (_claimed.Contains(body)) continue;

            float sqr = (body.transform.position - from).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = body;
            }
        }
        return best;
    }

    private float Effective(StatType type, float baseValue) => _stats != null ? _stats.GetEffective(type, baseValue) : baseValue;
}
