using UnityEngine;

public abstract class UpgradeDefinition : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _displayName;
    [SerializeField][TextArea] private string _description;

    public UpgradeOption BuildDisplay(ShipComponent ship, ShipStats stats)
    {
        return new UpgradeOption
        {
            Icon = _icon,
            Name = _displayName,
            Level = GetDisplayLevel(ship, stats),
            Description = _description,
        };
    }

    public abstract bool IsAvailable(ShipComponent ship, ShipStats stats);

    public abstract int GetDisplayLevel(ShipComponent ship, ShipStats stats);

    public abstract void Apply(ShipComponent ship, ShipStats stats);
}