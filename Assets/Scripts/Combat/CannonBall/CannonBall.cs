using UnityEngine;

public class CannonConfig
{
    public readonly LayerMask Target;
    public readonly float Damage;
    public readonly float ArcHeight;
    public readonly float FlightDuration;
    public readonly float AreaRadius;

    public CannonConfig(LayerMask target, float damage, float arcHeight, float flightDuration, float areaRadius)
    {
        Target = target;
        Damage = damage;
        ArcHeight = arcHeight;
        FlightDuration = flightDuration;
        AreaRadius = areaRadius;
    }
}

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : CombatItemBase
{
    private const int k_hitBufferSize = 32;
    private static readonly Collider[] _hitBuffer = new Collider[k_hitBufferSize];

    // RigidBody -> 베지어 커브로 변경
    private CannonConfig _config;
    private Vector3 _p0, _p1, _p2;
    private float _duration;
    private float _elapsed;
    private bool _flying;

    public override void Init()
    {
    }

    public override void Reset()
    {
        _config = null;
        _flying = false;
    }

    public void SetConfig(CannonConfig config) => _config = config;

    public override void Fire(Vector3 target)
    {
        if (_config == null) return;

        _p0 = transform.position;
        _p2 = target;
        _p1 = (_p0 + _p2) * 0.5f + Vector3.up * _config.ArcHeight;
        _duration = Mathf.Max(0.01f, _config.FlightDuration);
        _elapsed = 0f;
        _flying = true;
    }

    private void Update()
    {
        if (!_flying) return;

        _elapsed += Time.deltaTime;
        var t = Mathf.Clamp01(_elapsed / _duration);
        var u = 1f - t;
        transform.position = u * u * _p0 + 2f * u * t * _p1 + t * t * _p2;

        if (t >= 1f)
        {
            Explode(ParticleKind.WaterSplash);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (_config.Target == (_config.Target | (1 << other.gameObject.layer)))
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
            if (ship != null) ship?.OnDamaged(_config.Damage);
        }

        ParticlePoolRegistry.Get(particleKind).Play(center);
        ReturnToPool();
    }
}
