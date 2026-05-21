using UnityEngine;

public abstract class CannonBase : IComponent, IUpgradable<CannonBase>
{
    protected CombatData _data;

    protected float _cooldownTimer;
    public bool CanFire => _cooldownTimer <= 0;

    public Transform FirePoint { get; private set; }
    public float ArcHeight { get; private set; }
    public float FlightDuration { get; private set; }
    public void SetBarrel(Transform firePoint, float arcHeight, float flightDuration)
    {
        FirePoint = firePoint;
        ArcHeight = arcHeight;
        FlightDuration = flightDuration;
    }

    public abstract void Tick();

    public abstract CannonBase Upgrade();

    public void FireProcess()
    {
        var ball = CombatPoolRegistry.Get<CannonBall>();
        ball.transform.position = FirePoint.position;

        CannonConfig config = new(
            _data.Damage,
            ArcHeight,
            FlightDuration,
            _data.AreaRadius);
        ball.SetConfig(config);
        ball.Init();
        ball.Fire(GetRandomTargetPoint());
        _cooldownTimer = _data.Cooldown;
    }

    protected Vector3 GetRandomTargetPoint()
    {
        var yaw = Random.Range(0f, Mathf.PI * 2f);
        var horizontal = new Vector3(Mathf.Cos(yaw), 0f, Mathf.Sin(yaw));
        var distance = Random.Range(_data.MinRange, _data.MaxRange);
        var target = FirePoint.position + horizontal * distance;
        target.y = 0f;
        return target;
    }

    public void TickCooldown() => _cooldownTimer -= Time.deltaTime;
}