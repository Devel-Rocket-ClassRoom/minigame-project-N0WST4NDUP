using UnityEngine;

[CreateAssetMenu(
    fileName = "SubEquipment",
    menuName = "Upgrade/SubEquipment", order = 0)]
public class SubEquipment : UpgradeDefinition
{
    [SerializeField] private SubAttachableBase _prefab;

    public override bool IsAvailable(ShipComponent ship, ShipStats stats)
    {
        return ship.CanInstall(_prefab);
    }

    public override int GetDisplayLevel(ShipComponent ship, ShipStats stats)
    {
        return ship.IsEmpty(_prefab) ? 1 : ship.GetLevel(_prefab) + 1;
    }

    public override void Apply(ShipComponent ship, ShipStats stats)
    {
        ship.Install(_prefab);
    }
}