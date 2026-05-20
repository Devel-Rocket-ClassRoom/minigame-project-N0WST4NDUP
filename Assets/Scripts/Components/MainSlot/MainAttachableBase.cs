using UnityEngine;

public abstract class MainAttachableBase : MonoBehaviour, IAttachable, IUpgradable<MainAttachableBase>
{
    [Header("Default Config")]
    [SerializeField] protected CombatData _data;
    [SerializeField] private GameObject _prefab;

    public abstract void Attach();

    public abstract void Detach();

    public abstract void Tick();

    public abstract MainAttachableBase Upgrade();
}