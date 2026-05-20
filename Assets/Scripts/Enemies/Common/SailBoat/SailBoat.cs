using UnityEngine;

public class SailBoat : CommonEnemyBase
{
    public override void Reset()
    {
        Target = null;
        _detectTimer = 0f;
    }

    private void Update()
    {
        FindClosestShip();
        if (Target == null) return;

        var dir = (Target.position - transform.position).normalized;
        transform.forward = dir;
        transform.position += transform.forward * (MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_shipLayerMask == (_shipLayerMask | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable?.OnDamaged(_body.CurrentHealth);
            }
            Destroy(gameObject); // TODO: pool로 반환
        }
    }
}