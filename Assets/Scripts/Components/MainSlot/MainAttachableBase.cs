using UnityEngine;

public abstract class MainAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    [SerializeField] protected GameObject _prefab;

    protected LayerMask _target;

    public abstract void Attach();

    public abstract void Detach();

    public void SetTarget(LayerMask target)
    {
        _target = target;
    }
}