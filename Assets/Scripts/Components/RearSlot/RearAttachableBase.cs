using UnityEngine;

public abstract class RearAttachableBase : MonoBehaviour, IAttachable
{
    [Header("Default Config")]
    [SerializeField] private CombatData _data;

    public abstract void Attach();

    public abstract void Detach();

    public abstract void Tick();
}