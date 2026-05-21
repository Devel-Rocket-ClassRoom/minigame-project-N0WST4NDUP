using UnityEngine;

public class DoubleCannon : CannonBase
{
    private CannonBase _cannon;

    public DoubleCannon(CannonBase cannon)
    {
        _cannon = cannon;
        Debug.Log($"더블 캐넌, 캐넌: {_cannon.GetType().Name}");
    }

    public override void Tick()
    {
        _cannon.TickCooldown();

        if (!_cannon.CanFire) return;

        _cannon.FireProcess();
        _cannon.FireProcess();
    }

    public override CannonBase Upgrade()
    {
        _cannon = _cannon.Upgrade();
        return this;
    }
}