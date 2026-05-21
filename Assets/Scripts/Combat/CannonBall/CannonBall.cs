using UnityEngine;

public class CannonConfig
{
    public readonly float Damage;
    public readonly float ArcHeight;
    public readonly float FlightDuration;
    public readonly float AreaRadius;

    public CannonConfig(float damage, float arcHeight, float flightDuration, float areaRadius)
    {
        Damage = damage;
        ArcHeight = arcHeight;
        FlightDuration = flightDuration;
        AreaRadius = areaRadius;
    }
}

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : CombatItemBase
{
    private Rigidbody _rigidBody;

    private CannonConfig _config;
    private Vector3 _p0, _p1, _p2;
    private float _duration;
    private float _elapsed;
    private bool _flying;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

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
            ReturnToPool();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out ShipBody ship))
            {
                ship.OnDamaged(_config.Damage);
            }

            ParticlePoolRegistry.Get(ParticleKind.Explosion).Play(transform.position);
            ReturnToPool();
        }
    }
}
