using UnityEngine;

public abstract class MineDropperBase : IComponent, IUpgradable<MineDropperBase>
{
    protected CombatData _data;
    protected ShipStats _stats;

    protected float _cooldownTimer;
    public virtual bool CanFire => _cooldownTimer <= 0;

    public LayerMask Target { get; private set; }
    public Transform SpawnPoint { get; private set; }
    public float MineLifetime { get; private set; }
    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }

    protected virtual float CooldownMultiplier => 1f;

    public void SetDropper(LayerMask target, Transform spawnPoint, float mineLifetime)
    {
        Target = target;
        SpawnPoint = spawnPoint;
        MineLifetime = mineLifetime;
    }

    public virtual void Tick()
    {
        TickCooldown();

        if (!CanFire) return;

        DropProcess();
    }

    public abstract MineDropperBase Upgrade();

    protected float Effective(StatType type, float baseValue) => _stats != null ? _stats.GetEffective(type, baseValue) : baseValue;

    public virtual void DropProcess()
    {
        var mine = CombatPoolRegistry.Get<Mine>();
        mine.transform.position = SpawnPoint.position;

        MineConfig config = new(
            Target,
            Effective(StatType.Damage, _data.Damage),
            Effective(StatType.AreaRadius, _data.AreaRadius),
            MineLifetime
        );
        mine.SetConfig(config);
        mine.Init();
        mine.Fire(Vector3.zero);
        _cooldownTimer = _data.Cooldown * CooldownMultiplier;
    }

    public virtual void TickCooldown() => _cooldownTimer -= Time.deltaTime;
}
