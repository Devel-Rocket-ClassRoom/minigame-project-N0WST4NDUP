using UnityEngine;

public class Lv3_MineDropper : MineDropperBase
{
    protected override float CooldownMultiplier => 0.7f;

    public Lv3_MineDropper(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("지뢰 살포 레벨 3");
    }

    public override int Level => 3;
    public override bool CanUpgrade => false;

    public override MineDropperBase Upgrade()
    {
        return this;
    }
}
