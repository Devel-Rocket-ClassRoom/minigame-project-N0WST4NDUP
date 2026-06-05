using System.Collections.Generic;
using UnityEngine;

public class AircraftAttachable : MainAttachableBase
{
    [Header("Aircraft Config")]
    [SerializeField] private Fighter _aircraftPrefab;
    [Tooltip("전투기 로밍 목표 재설정 간격(초)")]
    [SerializeField] private float _roamInterval = 1.5f;

    private AircraftBase _aircraft;
    private readonly List<Fighter> _fighters = new();

    // 타게팅 임시 버퍼 — 단일 플레이어 함대가 메인 스레드에서 동기 처리하므로 static 공유 안전(MachineGunBase 선례).
    private const int k_targetBufferSize = 32;
    private static readonly Collider[] _targetBuffer = new Collider[k_targetBufferSize];
    private static readonly List<(float sqr, ShipBody body)> _candidates = new(k_targetBufferSize);
    private static readonly HashSet<ShipBody> _seen = new(k_targetBufferSize);
    private static readonly HashSet<ShipBody> _claimed = new(k_targetBufferSize);

    private float _fireTimer;
    private float _roamTimer;

    public AircraftBase Aircraft => _aircraft;
    public override int Level => _aircraft?.Level ?? 0;
    public override bool CanUpgrade => _aircraft?.CanUpgrade ?? true;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _aircraft = new Lv1_Aircraft();
        SyncFleet();
    }

    private void Update()
    {
        if (_aircraft == null) return;

        TickRoam();
        TickFire();
    }

    public override void Upgrade()
    {
        _aircraft = _aircraft.Upgrade();
        SyncFleet();
    }

    public override void WrapDouble()
    {
        _aircraft = new DoubleAircraft(_aircraft);
        SyncFleet();
    }

    public override void WrapTriple()
    {
        _aircraft = new TripleAircraft(_aircraft);
        SyncFleet();
    }

    public override void Detach()
    {
        for (int i = 0; i < _fighters.Count; i++)
        {
            if (_fighters[i] != null) Destroy(_fighters[i].gameObject);
        }
        _fighters.Clear();

        base.Detach();
    }

    // 실제 전투기 수를 로직이 산출한 목표치(FightersCount)에 맞춰 증감. config가 갖춰진 뒤 호출.
    private void SyncFleet()
    {
        int want = _aircraft.FightersCount;

        while (_fighters.Count < want)
        {
            var fighter = Instantiate(_aircraftPrefab, transform);
            _fighters.Add(fighter);
        }
        while (_fighters.Count > want)
        {
            int last = _fighters.Count - 1;
            if (_fighters[last] != null) Destroy(_fighters[last].gameObject);
            _fighters.RemoveAt(last);
        }
    }

    // 로밍: 일정 간격마다 각 전투기에 모체 반경 내 새 목표점 부여.
    private void TickRoam()
    {
        _roamTimer -= Time.deltaTime;
        if (_roamTimer > 0f) return;
        _roamTimer = _roamInterval;

        float radius = Effective(StatType.Range, _data.MaxRange);
        foreach (var fighter in _fighters)
        {
            var offset = Random.insideUnitCircle * radius;
            var point = new Vector3(offset.x, 0f, offset.y) + transform.position;
            fighter.SetSpecificPoint(point);
        }
    }

    // 함대 단위 사격. 감지·분배를 한 곳에서 처리해 같은 적에 두 전투기가 몰리는 무적시간 낭비를 차단.
    private void TickFire()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer > 0f || _fighters.Count == 0) return;

        AcquireTargets();
        if (_candidates.Count > 0) DistributeAndFire();

        _fireTimer = _data.Cooldown / Effective(StatType.FireRate, 1f);
    }

    // 모체 기준 사거리 내 적 ShipBody 수집. 이미 무적 상태인 적은 제외(이전 볼리 i-frame 잔여 → 때려도 낭비).
    private void AcquireTargets()
    {
        float range = Effective(StatType.Range, _data.MaxRange);
        int count = Physics.OverlapSphereNonAlloc(transform.position, range, _targetBuffer, _targetLayer);

        _candidates.Clear();
        _seen.Clear();
        for (int i = 0; i < count; i++)
        {
            var body = _targetBuffer[i].GetComponentInParent<ShipBody>();
            if (body == null) continue;
            if (body.IsInvincible || body.IsDestroyed) continue;
            if (!_seen.Add(body)) continue; // 동일 ShipBody 다중 콜라이더 dedupe
            float sqr = (body.transform.position - transform.position).sqrMagnitude;
            _candidates.Add((sqr, body));
        }
    }

    // 각 전투기에 '가장 가까운 미점유' 적을 배정. 한 볼리 안에서 같은 적은 한 번만 맞도록 claim.
    private void DistributeAndFire()
    {
        float damage = Effective(StatType.Damage, _data.Damage);
        int targetsPerFighter = _aircraft.TargetsPerFighter;
        _claimed.Clear();

        foreach (var fighter in _fighters)
        {
            for (int k = 0; k < targetsPerFighter; k++)
            {
                var target = NearestUnclaimed(fighter.transform.position);
                if (target == null) return; // 고유 타겟 소진 → 남은 전투기는 사격 보류(중복=낭비라 쏘지 않음)

                _claimed.Add(target);
                fighter.FireAt(target, damage);
            }
        }
    }

    private ShipBody NearestUnclaimed(Vector3 from)
    {
        ShipBody best = null;
        float bestSqr = float.MaxValue;
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
