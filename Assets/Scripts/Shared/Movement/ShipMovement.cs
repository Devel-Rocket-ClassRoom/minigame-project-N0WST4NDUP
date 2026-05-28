using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] private ShipMovementData _data;

    private Rigidbody _rigidbody;

    private float _throttle;
    private float _turn;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    public void Init(ShipMovementData data)
    {
        _data = data;
    }

    private void FixedUpdate()
    {
        if (_data == null) return;

        float forward = Mathf.Max(_throttle, 0f);
        float brake = Mathf.Max(-_throttle, 0f);
        float forwardSpeed = Vector3.Dot(_rigidbody.linearVelocity, transform.forward);
        float speedFactor = Mathf.Clamp01(forwardSpeed / _data.MaxSpeed);

        // 1) 전진 추력 (감속·최대속도는 Rigidbody.linearDamping이 처리)
        _rigidbody.AddForce(transform.forward * forward * _data.Acceleration,
                            ForceMode.Acceleration);
        if (brake > 0f)
        {
            float step = _data.BrakeStrength * brake * Time.fixedDeltaTime;
            _rigidbody.linearVelocity =
                Vector3.MoveTowards(_rigidbody.linearVelocity, Vector3.zero, step);
        }

        // 2) 선회 — 속도에 비례. 토크 대신 회전 직접 적용이 튜닝하기 쉬움
        float turnRate = _turn * _data.TurnSpeed * speedFactor; // deg/s
        Quaternion delta = Quaternion.Euler(0f, turnRate * Time.fixedDeltaTime, 0f);
        _rigidbody.MoveRotation(_rigidbody.rotation * delta);

        // 3) 측면 저항 — "배다움"의 핵심
        Vector3 v = _rigidbody.linearVelocity;
        Vector3 forwardV = transform.forward * Vector3.Dot(v, transform.forward);
        Vector3 lateralV = v - forwardV;
        _rigidbody.linearVelocity = forwardV + lateralV * (1f - _data.LateralGrip);
    }

    public void UpdateMove(float throttle, float turn)
    {
        _throttle = Mathf.Clamp(throttle, -1f, 1f);
        _turn = Mathf.Clamp(turn, -1f, 1f);
    }
}