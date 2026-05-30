using UnityEngine;

public class Lv2_MachineGun : MachineGunBase
{
    protected override float DamageMultiplier => 1.2f;

    public Lv2_MachineGun(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("머신건 레벨 2");
    }

    public override int Level => 2;
    public override bool CanUpgrade => true;

    public override MachineGunBase Upgrade()
    {
        var next = new Lv3_MachineGun(_data, _stats);
        next.SetBarrel(Target, FirePoint);
        return next;
    }
}
