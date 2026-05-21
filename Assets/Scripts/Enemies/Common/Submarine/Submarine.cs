using UnityEngine;

public class Submarine : CommonEnemyBase
{
    public EnemyStateMachine StateMachine { get; } = new();

    [Header("Submarine Config")]
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _submarine;
    [SerializeField] private GameObject _minePrefab;
    [SerializeField] private Vector3 _divingOffset = Vector3.down;
    [SerializeField] private float _divingDuration = 1f;

    private Vector3 _surfacePos;

    public float DivingDuration => _divingDuration;

    public override void Init()
    {
        base.Init();
        _surfacePos = _submarine.localPosition;
        StateMachine.ChangeState(new SubmarineIdleState(this));
    }

    public override void Reset()
    {
        Target = null;
    }

    private void Update()
    {
        StateMachine.OnTick();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_shipLayerMask == (_shipLayerMask | (1 << other.gameObject.layer)))
        {
            OnDead();
        }
    }

    public void ApplyDiveT(float t)
    {
        float eased = Mathf.SmoothStep(0f, 1f, t);
        _submarine.localPosition = _surfacePos + _divingOffset * eased;
    }

    public void SetCollider(bool enabled)
    {
        if (_collider != null) _collider.enabled = enabled;
    }

    public void SetMine()
    {
        Instantiate(_minePrefab, transform.position, Quaternion.identity);
    }

    protected override void OnDead()
    {
        Debug.Log("잠수함 사망");
        _pool?.Release(this);
    }
}
