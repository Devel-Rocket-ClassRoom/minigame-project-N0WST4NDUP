using UnityEngine;

public abstract class MainAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    [SerializeField] protected GameObject _prefab;

    protected LayerMask _target;
    protected ShipStats _stats;

    public virtual void Attach(LayerMask target, ShipStats stats)
    {
        _target = target;
        _stats = stats;
    }

    public virtual void Detach()
    {
        Destroy(gameObject);
    }
}