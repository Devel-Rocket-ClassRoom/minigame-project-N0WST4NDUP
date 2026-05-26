using UnityEngine;

[CreateAssetMenu(
    fileName = "StatModifier",
    menuName = "Upgrade/Stat Modifier", order = 1)]
public class StatModifierUpgrade : UpgradeDefinition
{
    [SerializeField] private Modifier[] _modifiers;

    public override bool IsAvailable(ShipComponent ship, ShipStats stats) => true; // 일단은 스택 무제한, 일정 개수? false로 밸런싱

    public override int GetDisplayLevel(ShipComponent ship, ShipStats stats) => 0;

    public override void Apply(ShipComponent ship, ShipStats stats)
    {
        foreach (var m in _modifiers)
        {
            stats.AddModifier(m);
        }
    }
}