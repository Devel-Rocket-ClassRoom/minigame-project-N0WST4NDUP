using UnityEngine;

public class DoubleMachineGun : MachineGunBase
{
    private MachineGunBase _gun;

    public DoubleMachineGun(MachineGunBase gun)
    {
        _gun = gun;
        Debug.Log($"더블 머신건, 머신건: {_gun.GetType().Name}");
    }

    public override int Level => _gun.Level;
    public override bool CanUpgrade => _gun.CanUpgrade;
    public override bool CanFire => _gun.CanFire;
    public override int TargetsPerShot => _gun.TargetsPerShot * 2;
    public override Vector3? AimPoint => _gun.AimPoint;

    public override void TickCooldown() => _gun.TickCooldown();

    public override void Tick()
    {
        _gun.TickCooldown();

        if (!_gun.CanFire) return;

        FireProcess();
    }

    public override void FireProcess() => _gun.FireAt(TargetsPerShot);

    public override void FireAt(int targetCount) => _gun.FireAt(targetCount);

    public override MachineGunBase Upgrade()
    {
        _gun = _gun.Upgrade();
        return this;
    }
}
