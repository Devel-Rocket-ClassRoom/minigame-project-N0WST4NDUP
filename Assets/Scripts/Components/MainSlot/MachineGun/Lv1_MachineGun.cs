using UnityEngine;

public class Lv1_MachineGun : MachineGunBase
{
    protected override float DamageMultiplier => 1f;

    public Lv1_MachineGun(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("머신건 레벨 1");
    }

    public override int Level => 1;
    public override bool CanUpgrade => true;

    public override MachineGunBase Upgrade()
    {
        var next = new Lv2_MachineGun(_data, _stats);
        next.SetBarrel(Target, FirePoint);
        return next;
    }
}
