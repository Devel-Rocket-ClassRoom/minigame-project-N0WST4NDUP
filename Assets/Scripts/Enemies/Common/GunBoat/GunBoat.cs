using UnityEngine;

public class GunBoat : CommonEnemyBase
{
    public EnemyStateMachine StateMachine { get; } = new();

    [Header("Default Config")]
    [SerializeField] private CombatData _combatData;
    [SerializeField] private float _detectRange = 10f;
    [SerializeField] private float _detectInterval = 2f;
    [SerializeField] private float _moveSpeed = 3f;

    [Header("State Config")]
    [SerializeField] private float _idlingInterval = 3f;
    [SerializeField] private int _patrolPointCountMin = 3;
    [SerializeField] private int _patrolPointCountMax = 7;
    [SerializeField] private float _patrolPointRadius = 5f;

    [Header("Combat Config")]
    [SerializeField] private GameObject _head;
    [SerializeField] private Transform _firePoint;

    private float _detectTimer;

    public Transform Target { get; private set; }
    private readonly Collider[] _detectBuffer = new Collider[8];
    private Vector3[] _patrolPoints; // TODO: 풀로 반환할 때 null 전환해서 GC가 수거할 수 있게
    private int _currentPatrolIndex = -1;

    public CombatData CombatData => _combatData;
    public float MoveSpeed => _moveSpeed;
    public float IdlingInterval => _idlingInterval;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        _body.Init(_data);
        GeneratePatrolPoints();
        StateMachine.ChangeState(new GunBoatIdleState(this));
    }

    private void GeneratePatrolPoints()
    {
        int count = Random.Range(_patrolPointCountMin, _patrolPointCountMax + 1);
        _patrolPoints = new Vector3[count];
        Vector3 origin = transform.position;
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * _patrolPointRadius;
            _patrolPoints[i] = new Vector3(origin.x + offset.x, origin.y, origin.z + offset.y);
        }
        _currentPatrolIndex = -1;
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    public void FindClosestShip()
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
        Target = bestTarget;

        _detectTimer = Time.time + _detectInterval;
    }

    public void OnFire()
    {
        if (Target == null) return;

        var dir = (Target.position - _firePoint.position).normalized;
        _head.transform.rotation = Quaternion.LookRotation(dir);
        if (Target.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDamaged(CombatData.Damage);
        }
    }

    public Vector3 GetNextPatrolPoint()
    {
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
        return _patrolPoints[_currentPatrolIndex];
    }

    private void ClearPatrolPoints()
    {
        _patrolPoints = null;
    }
}