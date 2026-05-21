using UnityEngine;

public abstract class MainAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    [SerializeField] protected GameObject _prefab;

    public abstract void Attach(Transform transform);

    public abstract void Detach();

}