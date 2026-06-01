using UnityEngine;

public class Submarine : CommonEnemyBase
{
    public EnemyStateMachine StateMachine { get; } = new();

    [Header("Submarine Config")]
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _submarine;
    [SerializeField] private Vector3 _divingOffset = Vector3.down;
    [SerializeField] private float _surfacedHoldDuration = 2f;
    [SerializeField] private float _submergedHoldDuration = 3f;
    [SerializeField] private float _transitionDuration = 1f;
    [SerializeField] private float _submergedSpeedMult = 1.3f;

    [Header("Mine Config")]
    [SerializeField] private float _mineDamage = 10f;
    [SerializeField] private float _mineRadius = 2f;
    [SerializeField] private float _mineLifetime = 20f;
    [SerializeField] private float _mineArmDelay = 1f;

    private Vector3 _surfacePos;

    public float TransitionDuration => _transitionDuration;
    public float SurfacedDuration => _surfacedHoldDuration;
    public float SubmergedDuration => _submergedHoldDuration;
    public float SubmergedSpeedMult => _submergedSpeedMult;

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

    public void FleeStep(float speedMult)
    {
        FindClosestShip();
        if (Target == null)
        {
            return;
        }

        var diff = transform.position - Target.position; diff.y = 0f;
        if (diff.sqrMagnitude < k_DeadzoneRadius) return;

        var dir = diff.normalized;
        transform.forward = dir;
        transform.position += transform.forward * (MoveSpeed * speedMult * Time.deltaTime);
    }

    public void LayMine()
    {
        var mine = CombatPoolRegistry.Get<Mine>();
        mine.transform.position = transform.position;
        mine.SetConfig(new(
            _shipLayerMask,
            _mineDamage,
            _mineRadius,
            _mineLifetime,
            _mineArmDelay));
        mine.Init();
        mine.Fire(Vector3.zero);

    }

    protected override void OnDead()
    {
        ParticlePoolRegistry.Get(ParticleKind.Die).Play(transform.position);
        _pool?.Release(this);
    }
}
