using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : CombatItemBase
{
    private const float SplashDuration = 3.5f;

    [SerializeField] private GameObject _waterSplashPrefab; // TODO: ParticlePool 도입 시 제거
    private Rigidbody _rigidBody;
    private bool _splashFlag = false;
    private float _splashTimer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        // Init();
    }

    public override void Init()
    {
        DespawnSplash();
        // var direction = Vector3.up * 10f + Vector3.right * 2f;
        // Fire(direction);
    }

    public override void Reset()
    {
        DespawnSplash();
        _splashFlag = false;
        _rigidBody.isKinematic = false;
    }

    public override void Fire(Vector3 force)
    {
        _rigidBody.AddForce(force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (_splashFlag)
        {
            TickSplash();
            return;
        }

        if (transform.position.y < 0f)
        {
            OnHitWater();
        }
    }

    private void OnHitWater()
    {
        _rigidBody.linearVelocity = Vector3.zero;
        _rigidBody.isKinematic = true;

        var pos = transform.position;
        pos.y = 0f;
        transform.position = pos;

        SpawnSplash();
        _splashFlag = true;
        _splashTimer = SplashDuration;
    }

    private void TickSplash()
    {
        _splashTimer -= Time.deltaTime;
        if (_splashTimer > 0f) return;

        DespawnSplash();
        _splashFlag = false;
        ReturnToPool();
    }

    // TODO: ParticlePool 도입 시 풀에서 splash 인스턴스 Get
    private void SpawnSplash()
    {
        if (_waterSplashPrefab == null) return;
        _waterSplashPrefab.SetActive(true);
    }

    // TODO: ParticlePool 도입 시 splash 인스턴스 Release
    private void DespawnSplash()
    {
        if (_waterSplashPrefab == null) return;
        _waterSplashPrefab.SetActive(false);
    }
}
