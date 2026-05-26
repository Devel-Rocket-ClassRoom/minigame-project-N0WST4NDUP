using UnityEngine;

public enum AttachmentType
{
    Main,
    Sub,
    Rear,
}

[CreateAssetMenu(
    fileName = "Equipment",
    menuName = "Upgrade/Equipment", order = 0)]
public class EquipmentUpgrade : UpgradeDefinition
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private AttachmentType _attachmentType;

    public override bool IsAvailable(ShipComponent ship, ShipStats stats) => false; // TODO: 추후 슬롯 상태 보고 결정하는 로직

    public override int GetDisplayLevel(ShipComponent ship, ShipStats stats) => 1; // TODO: 추후 장착된 장비 레벨 보고 결정하는 로직

    public override void Apply(ShipComponent ship, ShipStats stats)
    {
        // Implementation for applying equipment upgrade
    }
}