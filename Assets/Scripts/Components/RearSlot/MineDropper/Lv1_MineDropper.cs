using UnityEngine;

public class Lv1_MineDropper : MineDropperBase
{
    protected override float CooldownMultiplier => 1f;

    public Lv1_MineDropper(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
    }

    public override int Level => 1;
    public override bool CanUpgrade => true;

    public override MineDropperBase Upgrade()
    {
        var next = new Lv2_MineDropper(_data, _stats);
        next.SetDropper(Target, SpawnPoint, MineLifetime);
        return next;
    }
}
