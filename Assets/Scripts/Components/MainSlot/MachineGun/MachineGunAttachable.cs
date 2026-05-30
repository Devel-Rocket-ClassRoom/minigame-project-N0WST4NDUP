using UnityEngine;

public class MachineGunAttachable : MainAttachableBase
{
    [Header("Machine Gun Config")]
    [SerializeField] protected Transform _firePoint;

    private MachineGunBase _gun;

    public MachineGunBase Gun => _gun;
    public override int Level => _gun?.Level ?? 0;
    public override bool CanUpgrade => _gun == null ? true : _gun.CanUpgrade;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _gun = new Lv1_MachineGun(_data, _stats);
        _gun.SetBarrel(_targetLayer, _firePoint);
    }

    private void Update()
    {
        _gun?.Tick();
    }

    public override void Upgrade()
    {
        _gun = _gun.Upgrade();
    }

    public override void WrapDouble()
    {
        _gun = new DoubleMachineGun(_gun);
    }

    public override void WrapTriple()
    {
        _gun = new TripleMachineGun(_gun);
    }
}
