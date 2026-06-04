using UnityEngine;

public class MachineGunAttachable : MainAttachableBase
{
    [Header("Machine Gun Config")]
    [SerializeField] protected Transform _firePoint;
    [Tooltip("터렛 조준 회전 속도 (도/초)")]
    [SerializeField] private float _turnSpeed = 720f;

    private MachineGunBase _gun;

    public MachineGunBase Gun => _gun;
    public override int Level => _gun?.Level ?? 0;
    public override bool CanUpgrade => _gun == null ? true : _gun.CanUpgrade;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _gun = new Lv1_MachineGun(_data, _stats);
        _gun.SetBarrel(_targetLayer, _firePoint);
    }

    private void Update()
    {
        _gun?.Tick();
        AimAtTarget();
    }

    // primary 타겟 쪽으로 배와 독립해 360° 회전(월드 기준). 히트스캔이라 연출 전용.
    private void AimAtTarget()
    {
        if (_gun?.AimPoint is not Vector3 target) return;

        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion want = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, want, _turnSpeed * Time.deltaTime);
    }

    public override void Upgrade()
    {
        _gun = _gun.Upgrade();
    }

    public override void WrapDouble()
    {
        _gun = new DoubleMachineGun(_gun);
    }

    public override void WrapTriple()
    {
        _gun = new TripleMachineGun(_gun);
    }
}
