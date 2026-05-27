using UnityEngine;

public class PropellerAttachable : RearAttachableBase
{
    [SerializeField] private int _maxLevel = 3;
    [SerializeField] private float _baseSpeedMultiplier = 0.12f;
    [SerializeField] private float _incrementPerLevel = 0.07f;

    private int _level;
    private Modifier _modifier;

    public override int Level => _level;
    public override bool CanUpgrade => _level < _maxLevel;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        Debug.Log("Propeller Attached");

        base.Attach(target, stats);

        _level = 1;
        _modifier = new Modifier
        {
            Stat = StatType.MoveSpeed,
            Op = ModifierOp.PercentAdd,
            Value = _baseSpeedMultiplier
        }; // 속도 +12% 시작

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