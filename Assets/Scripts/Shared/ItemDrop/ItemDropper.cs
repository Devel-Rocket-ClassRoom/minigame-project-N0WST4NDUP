using UnityEngine;

[RequireComponent(typeof(ShipBody))]
public abstract class ItemDropper : MonoBehaviour
{
    [Header("Default Config")]
    [SerializeField] protected bool _active;

    private ShipBody _body;

    private void Awake()
    {
        _body = GetComponent<ShipBody>();
        _body.OnDeadEvent += OnDead;
    }

    private void OnDestroy()
    {
        if (_body != null) _body.OnDeadEvent -= OnDead;
    }

    private void OnDead()
    {
        if (!_active) return;

        Drop(transform.position);
    }

    protected abstract void Drop(Vector3 position);
}
