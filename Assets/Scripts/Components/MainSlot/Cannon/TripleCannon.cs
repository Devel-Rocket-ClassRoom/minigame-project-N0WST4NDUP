using UnityEngine;

public class TripleCannon : CannonBase
{
    private CannonBase _cannon;

    public TripleCannon(CannonBase cannon)
    {
        _cannon = cannon;
        Debug.Log($"더블 캐넌, 캐넌: {_cannon.GetType().Name}");
    }

    public override int Level => _cannon.Level;
    public override bool CanUpgrade => _cannon.CanUpgrade;

    public override void Tick()
    {
        _cannon.TickCooldown();

        if (!_cannon.CanFire) return;

        FireProcess();
    }

    public override CannonBase Upgrade()
    {
        _cannon = _cannon.Upgrade();
        return this;
    }

    public override void FireProcess()
    {
        _cannon.FireProcess();
        _cannon.FireProcess();
        _cannon.FireProcess();
    }
}