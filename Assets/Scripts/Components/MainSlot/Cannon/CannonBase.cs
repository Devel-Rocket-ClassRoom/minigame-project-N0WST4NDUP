using UnityEngine;

public abstract class CannonBase : IComponent, IUpgradable<CannonBase>
{
    protected CombatData _data;
    protected ShipStats _stats;

    protected float _cooldownTimer;
    public bool CanFire => _cooldownTimer <= 0;

    public LayerMask Target { get; private set; }
    public Transform FirePoint { get; private set; }
    public float ArcHeight { get; private set; }
    public float FlightDuration { get; private set; }
    public void SetBarrel(LayerMask target, Transform firePoint, float arcHeight, float flightDuration)
    {
        Target = target;
        FirePoint = firePoint;
        ArcHeight = arcHeight;
        FlightDuration = flightDuration;
    }

    public abstract void Tick();

    public abstract CannonBase Upgrade();

    protected float Effective(StatType type, float baseValue) => _stats != null ? _stats.GetEffective(type, baseValue) : baseValue;

    public void FireProcess()
    {
        var ball = CombatPoolRegistry.Get<CannonBall>();
        ball.transform.position = FirePoint.position;

        CannonConfig config = new(
            Target,
            Effective(StatType.Damage, _data.Damage),
            ArcHeight,
            FlightDuration,
            Effective(StatType.AreaRadius, _data.AreaRadius)
        );
        ball.SetConfig(config);
        ball.Init();
        ball.Fire(GetRandomTargetPoint());
        _cooldownTimer = _data.Cooldown / Effective(StatType.FireRate, 1f);
    }

    protected Vector3 GetRandomTargetPoint()
    {
        var yaw = Random.Range(0f, Mathf.PI * 2f);
        var horizontal = new Vector3(Mathf.Cos(yaw), 0f, Mathf.Sin(yaw));
        var distance = Random.Range(_data.MinRange, Effective(StatType.Range, _data.MaxRange));
        var target = FirePoint.position + horizontal * distance;
        target.y = 0f;
        return target;
    }

    public void TickCooldown() => _cooldownTimer -= Time.deltaTime;
}