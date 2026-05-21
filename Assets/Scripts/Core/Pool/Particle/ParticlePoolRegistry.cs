using System;
using System.Collections.Generic;
using UnityEngine;

public static class ParticlePoolRegistry
{
    private static readonly Dictionary<ParticleKind, ParticlePool> _pools = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetOnLoad() => _pools.Clear();

    public static void Register(ParticleKind kind, ParticlePool pool)
    {
        _pools[kind] = pool;
    }

    public static void Unregister(ParticleKind kind)
    {
        _pools.Remove(kind);
    }

    public static PooledParticle Get(ParticleKind kind)
    {
        if (!_pools.TryGetValue(kind, out var pool))
            throw new InvalidOperationException($"No ParticlePool registered for {kind}");
        return pool.Get();
    }
}
