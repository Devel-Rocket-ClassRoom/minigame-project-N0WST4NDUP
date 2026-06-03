using UnityEngine;

[CreateAssetMenu(
    fileName = "MainEquipment",
    menuName = "Upgrade/MainEquipment", order = 0)]
public class MainEquipment : UpgradeDefinition
{
    [SerializeField] private MainAttachableBase _prefab;

    public IAttachable Attachable => _prefab;

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