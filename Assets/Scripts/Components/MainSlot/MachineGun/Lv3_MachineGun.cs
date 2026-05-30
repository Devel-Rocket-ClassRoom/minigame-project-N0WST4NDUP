using UnityEngine;

public class Lv3_MachineGun : MachineGunBase
{
    protected override float DamageMultiplier => 1.4f;

    public Lv3_MachineGun(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("머신건 레벨 3");
    }

    public override int Level => 3;
    public override bool CanUpgrade => false;

    public override MachineGunBase Upgrade()
    {
        return this;
    }
}
