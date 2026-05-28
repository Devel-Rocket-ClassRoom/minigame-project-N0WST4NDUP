using UnityEngine;

public class RadialSweepAttack : MonoBehaviour
{
    [SerializeField] private RadialSweepConfig _config;

    private float _cooldownTimer;
    private int _shotIndex;
    private float _shotTimer;
    private bool _inSweeping;

    private void Start()
    {
        _cooldownTimer = _config.Cooldown;
    }

    private void Update()
    {
        if (!_inSweeping)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _inSweeping = true;
                _shotIndex = 0;
            }
        }
        else
        {
            _shotTimer -= Time.deltaTime;
            if (_shotTimer <= 0f)
            {
                FireAt(_shotIndex++);
                if (_shotIndex < _config.ProjectileCount)
                {
                    _shotTimer = _config.ShotInterval;
                }
                else
                {
                    _inSweeping = false;
                    _cooldownTimer = _config.Cooldown;
                }
            }
        }
    }

    private void FireAt(int index)
    {
        float angle = (360f / _config.ProjectileCount) * index;
        if (!_config.Clockwise) angle = -angle;
        Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
        Vector3 target = transform.position + direction * _config.Range;
        Fire(target);
    }

    private void Fire(Vector3 target)
    {
        var ball = CombatPoolRegistry.Get<CannonBall>();
        ball.transform.position = transform.position;
        ball.SetConfig(
            new(
                _config.TargetLayerMask,
                _config.Damage,
                _config.ArcHeight,
                _config.FlightDuration,
                _config.AreaRadius
            )
        );
        ball.Init();
        ball.Fire(target);
    }
}