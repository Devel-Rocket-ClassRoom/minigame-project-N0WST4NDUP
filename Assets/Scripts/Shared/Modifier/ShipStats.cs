using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    private readonly List<Modifier> _modifiers = new();
    public event Action OnStatsChanged;

    public IReadOnlyList<Modifier> Modifiers => _modifiers;

    public void AddModifier(Modifier m)
    {
        _modifiers.Add(m);
        OnStatsChanged?.Invoke();
    }

    public void RemoveModifier(Modifier m)
    {
        if (_modifiers.Remove(m)) OnStatsChanged?.Invoke();
    }

    public float GetEffective(StatType stat, float baseValue)
    {
        float add = 0f, percent = 0f;
        foreach (var m in _modifiers)
        {
            if (m.Stat != stat) continue;
            if (m.Op == ModifierOp.Add) add += m.Value;
            else percent += m.Value;
        }
        return (baseValue + add) * (1f + percent);
    }
}
