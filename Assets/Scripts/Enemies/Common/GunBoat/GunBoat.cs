using UnityEngine;

public class GunBoat : CommonEnemyBase
{
    protected const int k_patrolPointRadius = 8;
    public EnemyStateMachine StateMachine { get; } = new();

    [Header("GunBoat Config")]
    [SerializeField] private CombatData _combatData;
    [SerializeField] private float _idlingInterval = 3f;
    [SerializeField] private float _patrolPointRadius = 5f;

    [Header("Combat Config")]
    [SerializeField] private GameObject _head;
    [SerializeField] private Transform _firePoint;

    private Vector3[] _patrolPoints = new Vector3[k_patrolPointRadius];
    private int _currentPatrolIndex = -1;

    public CombatData CombatData => _combatData;
    public float IdlingInterval => _idlingInterval;

    public override void Init()
    {
        base.Init();
        GeneratePatrolPoints();
        StateMachine.ChangeState(new GunBoatIdleState(this));
    }

    public override void Reset()
    {
        Target = null;
        ClearPatrolPoints();
    }

    private void Update()
    {
        StateMachine.OnTick();
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

    private void GeneratePatrolPoints()
    {
        var origin = transform.position;
        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            var offset = Random.insideUnitCircle * _patrolPointRadius;
            _patrolPoints[i] = new(origin.x + offset.x, origin.y, origin.z + offset.y);
        }
        _currentPatrolIndex = -1;
    }

    public Vector3 GetNextPatrolPoint()
    {
        _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
        return _patrolPoints[_currentPatrolIndex];
    }

    private void ClearPatrolPoints()
    {
        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            _patrolPoints[i] = Vector3.zero;
        }
    }

    protected override void OnDead()
    {
        _pool?.Release(this);
    }
}