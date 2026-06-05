using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("전진 속도 (units/sec)")]
    [SerializeField] private float _moveSpeed = 6f;
    [Tooltip("선회 속도 (deg/sec) — 클수록 빠르게 방향 전환")]
    [SerializeField] private float _turnSpeed = 240f;

    private Vector3 _specificPoint;

    public void SetSpecificPoint(Vector3 point) => _specificPoint = point;

    private void Update()
    {
        // 목표점으로 부드럽게 선회하며 항상 전진 — 전투기는 멈추지 않고 선회해 다시 접근(자연스러운 비행).
        Vector3 toTarget = _specificPoint - transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude > 0.01f)
        {
            Quaternion want = Quaternion.LookRotation(toTarget);
            // RotateTowards는 정렬에 가까울수록 더 안 돌아 핑퐁이 없음(Mathf.Sign 방식의 데드존 불필요).
            transform.rotation = Quaternion.RotateTowards(transform.rotation, want, _turnSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * (_moveSpeed * Time.deltaTime);
    }

    // 배정받은 적을 즉발(히트스캔) 타격. 감지·중복 배제는 AircraftAttachable이 끝낸 뒤 호출됨.
    public void FireAt(ShipBody target, float damage)
    {
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0f;
        Quaternion rot = dir.sqrMagnitude > 0.0001f ? Quaternion.LookRotation(dir) : transform.rotation;

        ParticlePoolRegistry.Get(ParticleKind.FireFlash).Play(transform.position, rot);
        target.OnDamaged(damage);
        ParticlePoolRegistry.Get(ParticleKind.HitFlash).Play(target.transform.position);
    }
}
