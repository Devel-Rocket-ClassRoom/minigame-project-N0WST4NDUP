using UnityEngine;

public abstract class CannonBase : IComponent, IUpgradable<CannonBase>
{
    protected CombatData _data;

    public float Upward { get; private set; }
    public Transform FirePoint { get; private set; }

    protected float _cooldownTimer;
    public bool CanFire => _cooldownTimer <= 0;

    public abstract void Tick();

    public abstract CannonBase Upgrade();

    public void Settings(float upward, Transform firePoint)
    {
        Upward = upward;
        FirePoint = firePoint;
    }

    protected Vector3 GetRandomFireDirection()
    {
        var angle = Random.Range(0f, Mathf.PI * 2f);
        var horizontal = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        var force = horizontal * Random.Range(_data.MinRange, _data.MaxRange);
        return force + Vector3.up * Upward;
    }

    public void TickCooldown() => _cooldownTimer -= Time.deltaTime;

    public void FireProcess()
    {
        var ball = CombatPoolRegistry.Get<CannonBall>();

        ball.transform.position = FirePoint.position;
        var force = GetRandomFireDirection();

        ball.Init();
        ball.Fire(force);

        _cooldownTimer = _data.Cooldown;
    }
}