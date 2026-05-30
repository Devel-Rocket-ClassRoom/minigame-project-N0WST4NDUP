using UnityEngine;

public class RudderAttachable : RearAttachableBase
{
    [SerializeField] private int _maxLevel = 3;
    [SerializeField] private float _baseSpeedMultiplier = 0.2f;
    [SerializeField] private float _incrementPerLevel = 0.05f;

    private int _level;
    private Modifier _modifier;

    public override int Level => _level;
    public override bool CanUpgrade => _level < _maxLevel;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        Debug.Log("Rudder Attached");

        base.Attach(target, stats);

        _level = 1;
        _modifier = new Modifier
        {
            Stat = StatType.TurnSpeed,
            Op = ModifierOp.PercentAdd,
            Value = _baseSpeedMultiplier
        }; // 회전 속도 +20% 시작

        _stats?.AddModifier(_modifier);
    }

    public override void Detach()
    {
        _stats?.RemoveModifier(_modifier);

        base.Detach();
    }

    public override void Upgrade()
    {
        if (!CanUpgrade) return;

        _modifier.Value = _baseSpeedMultiplier + _incrementPerLevel * _level++;
        _stats?.UpdateModifier();
    }
}