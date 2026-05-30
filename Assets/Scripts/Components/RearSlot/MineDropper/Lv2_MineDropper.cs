using UnityEngine;

public class Lv2_MineDropper : MineDropperBase
{
    protected override float CooldownMultiplier => 0.9f;

    public Lv2_MineDropper(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("지뢰 살포 레벨 2");
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
