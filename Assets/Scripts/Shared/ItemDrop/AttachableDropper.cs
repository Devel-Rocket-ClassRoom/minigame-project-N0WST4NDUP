using UnityEngine;

[RequireComponent(typeof(Named))]
public class AttachableDropper : ItemDropper
{
    [SerializeField] private AttachableWrapper _wrapperPrefab;

    protected override void Drop(Vector3 position)
    {
        if (_wrapperPrefab == null) return;
        if (!GetComponent<Named>().TryGetRandomEquippedDefinition(out var def)) return;

        var wrapper = Instantiate(_wrapperPrefab, position, Quaternion.identity);
        wrapper.SetDefinition(def);
    }
}