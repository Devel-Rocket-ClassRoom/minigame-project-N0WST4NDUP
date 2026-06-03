using UnityEngine;

public class XPMagnet : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _radius = 5f;

    private SphereCollider _collider;

    private void Awake()
    {
        _collider = gameObject.AddComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = _radius;
    }

    private void OnValidate()
    {
        if (_collider != null) _collider.radius = _radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new(0.4f, 0.9f, 1f, 0.4f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<XPGem>(out var gem))
        {
            gem.OnPick(_target);
        }
    }
}
