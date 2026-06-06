using UnityEngine;

[RequireComponent(typeof(ShipBody))]
public abstract class CommonEnemyBase : MonoBehaviour
{
    protected const float k_DeadzoneRadius = 0.001f;
    protected const int k_DetectBufferSize = 8;

    [Header("Default Config")]
    [SerializeField] protected LayerMask _shipLayerMask;
    [SerializeField] private ShipData _data;
    [SerializeField] private float _detectRange = 10f;
    [SerializeField] private float _detectInterval = 2.5f;
    [SerializeField] private float _moveSpeed = 6f;

    protected ShipBody _body;
    protected CommonPool _pool;

    public Transform Target { get; protected set; }
    protected readonly Collider[] _detectBuffer = new Collider[k_DetectBufferSize];
    protected float _detectTimer;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _body = GetComponent<ShipBody>();
        _body.OnDeadEvent += HandleDead;
    }

    private void OnDestroy()
    {
        if (_body != null) _body.OnDeadEvent -= HandleDead;
    }

    private void HandleDead()
    {
        GameManager.Instance.AddScore(_data.ScoreReward);
        OnDead();
    }

    public void SetPool(CommonPool pool) => _pool = pool;

    public virtual void Init() // TODO: stage 추가시 체력 증가 등 초기화 작업 추가
    {
        _body.Init(_data);
    }

    public abstract void Reset();

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

    protected abstract void OnDead();

    public void ReturnToPool()
    {
        _pool?.Release(this);
    }
}