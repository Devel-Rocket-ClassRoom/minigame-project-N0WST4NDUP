using System;
using System.Collections.Generic;
using UnityEngine;

public static class CombatPoolRegistry
{
    private static readonly Dictionary<Type, CombatPool> _pools = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetOnLoad() => _pools.Clear();

    public static void Register(Type itemType, CombatPool pool)
    {
        _pools[itemType] = pool;
    }

    public static void Unregister(Type itemType)
    {
        _pools.Remove(itemType);
    }

    public static T Get<T>() where T : CombatItemBase
    {
        if (!_pools.TryGetValue(typeof(T), out var pool))
            throw new InvalidOperationException($"No CombatPool registered for {typeof(T).Name}");
        return (T)pool.Get();
    }
}
