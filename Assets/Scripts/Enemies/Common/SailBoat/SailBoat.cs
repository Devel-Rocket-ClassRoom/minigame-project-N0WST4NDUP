using UnityEngine;

public class SailBoat : CommonEnemyBase
{
    [Header("Config")]
    [SerializeField] private float _detectRange = 20f;
    [SerializeField] private float _detectInterval = 3f;
    [SerializeField] private float _moveSpeed = 3f;
    private float _detectTimer;

    private Transform _target;
    private readonly Collider[] _detectBuffer = new Collider[8];

    private void Update()
    {
        FindClosestShip();
        if (_target == null) return;

        Vector3 dir = (_target.position - transform.position).normalized;
        transform.forward = dir;
        transform.position += transform.forward * (_moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_shipLayerMask == (_shipLayerMask | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable?.OnDamaged(_body.CurrentHealth);
            }
            Destroy(gameObject);
        }
    }

    private void FindClosestShip()
    {
        if (Time.time < _detectTimer) return;

        int count = Physics.OverlapSphereNonAlloc(
            transform.position, _detectRange, _detectBuffer, _shipLayerMask);

        Transform bestTarget = null;
        float bestSqr = float.PositiveInfinity;
        for (int i = 0; i < count; i++)
        {
            var t = _detectBuffer[i].transform;
            float sqr = (t.position - transform.position).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                bestTarget = t;
            }
        }
        _target = bestTarget;

        _detectTimer = Time.time + _detectInterval;
    }
}