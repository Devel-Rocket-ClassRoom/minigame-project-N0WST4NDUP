using UnityEngine;

public abstract class RearAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    // [SerializeField] protected GameObject _prefab;

    protected LayerMask _target;
    protected ShipStats _stats;

    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }

    public virtual void Attach(LayerMask target, ShipStats stats)
    {
        _target = target;
        _stats = stats;
    }

    public virtual void Detach()
    {
        Destroy(gameObject);
    }

    public abstract void Upgrade();
}