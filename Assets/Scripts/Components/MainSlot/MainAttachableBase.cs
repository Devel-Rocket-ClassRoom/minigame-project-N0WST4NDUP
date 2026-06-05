using UnityEngine;

public abstract class MainAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    // [SerializeField] protected GameObject _prefab;

    protected LayerMask _targetLayer;
    protected ShipStats _stats;

    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }
    public Sprite Icon => _data != null ? _data.Icon : null;

    public virtual void Attach(LayerMask targetLayer, ShipStats stats)
    {
        _targetLayer = targetLayer;
        _stats = stats;
    }

    public virtual void Detach()
    {
        Destroy(gameObject);
    }

    public abstract void Upgrade();

    public abstract void WrapDouble();

    public abstract void WrapTriple();
}