using UnityEngine;

[CreateAssetMenu(
    fileName = "MainTripleWrapper",
    menuName = "Upgrade/Triple", order = 1)]
public class MainTripleWrapper : UpgradeDefinition
{
    public override bool IsAvailable(ShipComponent ship, ShipStats stats)
    {
        return ship.MainSlot != null;
    }

    public override int GetDisplayLevel(ShipComponent ship, ShipStats stats) => 0;

    public override void Apply(ShipComponent ship, ShipStats stats)
    {
        ship.MainSlot.WrapTriple();
    }
}
