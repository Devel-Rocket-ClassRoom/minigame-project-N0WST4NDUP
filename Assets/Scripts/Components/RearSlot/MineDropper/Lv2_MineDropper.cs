using UnityEngine;

public class Lv2_MineDropper : MineDropperBase
{
    protected override float CooldownMultiplier => 0.9f;

    public Lv2_MineDropper(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
    }

    public override int Level => 2;
    public override bool CanUpgrade => true;

    public override MineDropperBase Upgrade()
    {
        var next = new Lv3_MineDropper(_data, _stats);
        next.SetDropper(Target, SpawnPoint, MineLifetime);
        return next;
    }
}
