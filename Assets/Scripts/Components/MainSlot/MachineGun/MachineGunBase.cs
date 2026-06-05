using System.Collections.Generic;
using UnityEngine;

public abstract class MachineGunBase : IComponent, IUpgradable<MachineGunBase>
{
    private const int k_targetBufferSize = 32;
    private static readonly Collider[] _targetBuffer = new Collider[k_targetBufferSize];
    private static readonly List<(float sqr, ShipBody body)> _sortBuffer = new(k_targetBufferSize);
    private static readonly HashSet<ShipBody> _seenBodies = new(k_targetBufferSize);

    protected CombatData _data;
    protected ShipStats _stats;

    protected float _cooldownTimer;
    public virtual bool CanFire => _cooldownTimer <= 0;

    public LayerMask Target { get; private set; }
    public Transform FirePoint { get; private set; }

    // 연출용 조준점 — 마지막 사격의 primary 타겟 위치(우선순위·최근접). 래퍼는 내부 gun 값을 forward.
    protected Vector3? _aimPoint;
    public virtual Vector3? AimPoint => _aimPoint;

    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }

    protected virtual float DamageMultiplier => 1f;
    public virtual int TargetsPerShot => 1;

    public void SetBarrel(LayerMask target, Transform firePoint)
    {
        Target = target;
        FirePoint = firePoint;
    }

    public virtual void Tick()
    {
        TickCooldown();

        if (!CanFire) return;

        FireProcess();
    }

    public abstract MachineGunBase Upgrade();

    protected float Effective(StatType type, float baseValue) => _stats != null ? _stats.GetEffective(type, baseValue) : baseValue;

    public virtual void FireProcess() => FireAt(TargetsPerShot);

    public virtual void FireAt(int targetCount)
    {
        float range = Effective(StatType.Range, _data.MaxRange);
        int count = Physics.OverlapSphereNonAlloc(FirePoint.position, range, _targetBuffer, Target);

        _sortBuffer.Clear();
        _seenBodies.Clear();
        for (int i = 0; i < count; i++)
        {
            var collider = _targetBuffer[i];
            var body = collider.GetComponentInParent<ShipBody>();
            if (body == null) continue;
            if (!_seenBodies.Add(body)) continue; // 동일 ShipBody 다중 콜라이더 dedupe
            float sqr = (body.transform.position - FirePoint.position).sqrMagnitude;
            _sortBuffer.Add((sqr, body));
        }

        if (_sortBuffer.Count > 0)
        {
            // 무조건 가까운 순
            _sortBuffer.Sort((a, b) => a.sqr.CompareTo(b.sqr));

            var closest = _sortBuffer[0].body.transform;
            _aimPoint = closest.position;

            var fireRot = Quaternion.LookRotation(closest.position - FirePoint.position);
            ParticlePoolRegistry.Get(ParticleKind.FireFlash).Play(FirePoint.position, fireRot);

            float damage = Effective(StatType.Damage, _data.Damage) * DamageMultiplier;
            int hits = Mathf.Min(targetCount, _sortBuffer.Count);
            for (int i = 0; i < hits; i++)
            {
                var (_, body) = _sortBuffer[i];
                body.OnDamaged(damage);
                ParticlePoolRegistry.Get(ParticleKind.HitFlash).Play(body.transform.position);
            }
        }

        _cooldownTimer = _data.Cooldown / Effective(StatType.FireRate, 1f);
    }

    public virtual void TickCooldown() => _cooldownTimer -= Time.deltaTime;
}
