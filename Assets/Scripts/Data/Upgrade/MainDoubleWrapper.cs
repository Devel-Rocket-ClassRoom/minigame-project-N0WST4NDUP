using UnityEngine;

[CreateAssetMenu(
    fileName = "MainDoubleWrapper",
    menuName = "Upgrade/Double", order = 1)]
public class MainDoubleWrapper : UpgradeDefinition
{
    public override bool IsAvailable(ShipComponent ship, ShipStats stats)
    {
        return ship.MainSlot != null;
    }

    public override int GetDisplayLevel(ShipComponent ship, ShipStats stats) => 0;

    public override void Apply(ShipComponent ship, ShipStats stats)
    {
        ship.MainSlot.WrapDouble();
    }
}
