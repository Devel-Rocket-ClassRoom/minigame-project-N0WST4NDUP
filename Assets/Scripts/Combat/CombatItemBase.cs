using UnityEngine;

public abstract class CombatItemBase : MonoBehaviour
{
    private CombatPool _pool;

    public void SetPool(CombatPool pool) => _pool = pool;

    public abstract void Init();

    public abstract void Reset();

    public abstract void Fire(Vector3 direction);

    public void ReturnToPool() => _pool?.Release(this);
}