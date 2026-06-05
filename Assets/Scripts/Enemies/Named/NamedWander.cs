using UnityEngine;

// 임시 이동: 네임드가 일정 주기마다 무작위 throttle/turn으로 떠다닌다.
// 추후 추적 AI로 교체 시 이 컴포넌트만 제거하면 된다.
[RequireComponent(typeof(ShipMovement))]
public class NamedWander : MonoBehaviour
{
    [SerializeField] private float _changeInterval = 2.5f;
    [SerializeField] private float _minThrottle = 0.3f;

    private ShipMovement _movement;
    private float _throttle;
    private float _turn;
    private float _nextChangeTime;

    private void Awake()
    {
        _movement = GetComponent<ShipMovement>();
    }

    private void Start()
    {
        PickNewTarget();
    }

    private void Update()
    {
        if (Time.time >= _nextChangeTime) PickNewTarget();
        _movement.UpdateMove(_throttle, _turn);
    }

    private void PickNewTarget()
    {
        _throttle = Random.Range(_minThrottle, 1f);
        _turn = Random.Range(-1f, 1f);
        _nextChangeTime = Time.time + _changeInterval;
    }
}
