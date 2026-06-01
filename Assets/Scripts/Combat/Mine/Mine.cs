using UnityEngine;

public class MineConfig
{
    public readonly LayerMask Target;
    public readonly float Damage;
    public readonly float AreaRadius;
    public readonly float Lifetime;
    public readonly float ArmDelay;

    public MineConfig(LayerMask target, float damage, float areaRadius, float lifetime, float armDelay = 0f)
    {
        Target = target;
        Damage = damage;
        AreaRadius = areaRadius;
        Lifetime = lifetime;
        ArmDelay = armDelay;
    }
}

public class Mine : CombatItemBase
{
    private const int k_hitBufferSize = 32;
    private static readonly Collider[] _hitBuffer = new Collider[k_hitBufferSize];

    private MineConfig _config;
    private float _elapsed;

    public override void Init()
    {
    }

    public override void Reset()
    {
        _config = null;
    }

    public void SetConfig(MineConfig config) => _config = config;

    public override void Fire(Vector3 _)
    {
        if (_config == null) return;

        _elapsed = 0f;
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _config.Lifetime)
        {
            Explode(ParticleKind.WaterSplash);
            return;
        }

        if (_elapsed < _config.ArmDelay) return;
        if (Physics.CheckSphere(transform.position, _config.AreaRadius, _config.Target, QueryTriggerInteraction.Collide))
        {
            Explode(ParticleKind.Explosion);
        }
    }

    private void Explode(ParticleKind particleKind)
    {
        Vector3 center = transform.position;
        int count = Physics.OverlapSphereNonAlloc(
            center,
            _config.AreaRadius,
            _hitBuffer,
            _config.Target,
            QueryTriggerInteraction.Collide);

        for (int i = 0; i < count; i++)
        {
            var ship = _hitBuffer[i].GetComponentInParent<ShipBody>();
            if (ship != null) ship.OnDamaged(_config.Damage);
        }

        ParticlePoolRegistry.Get(particleKind).Play(center);
        ReturnToPool();
    }
}
