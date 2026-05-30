using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Radial Sweep",
    story: "[Self] runs Radial Sweep with [Config]",
    category: "Action/Boss",
    id: "9a1b8e62-1c4d-4a8c-bf3d-1ed4f9d7b201")]
public partial class RadialSweepAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<RadialSweepConfig> Config;
    [SerializeReference] public BlackboardVariable<Phase> RunOn;

    private Transform _selfTr;
    private BehaviorGraphAgent _agent;
    private float _cooldownTimer;
    private bool _inSweeping;
    private int _shotIndex;
    private float _shotTimer;

    protected override Status OnStart()
    {
        if (Self.Value == null || Config.Value == null) return Status.Failure;
        _selfTr = Self.Value.transform;
        _agent = Self.Value.GetComponent<BehaviorGraphAgent>();
        _cooldownTimer = Config.Value.Cooldown;
        _inSweeping = false;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    protected override Status OnUpdate()
    {
        if (RunOn != null && _agent != null
            && _agent.GetVariable<Phase>("Phase", out var phaseVar)
            && phaseVar.Value != RunOn.Value)
        {
            return Status.Success;
        }

        var config = Config.Value;

        if (!_inSweeping)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _inSweeping = true;
                _shotIndex = 0;
                _shotTimer = 0f;
            }
        }
        else
        {
            _shotTimer -= Time.deltaTime;
            if (_shotTimer <= 0f)
            {
                FireAt(_shotIndex++, config);
                if (_shotIndex < config.ProjectileCount)
                {
                    _shotTimer = config.ShotInterval;
                }
                else
                {
                    _inSweeping = false;
                    _cooldownTimer = config.Cooldown;
                }
            }
        }
        return Status.Running;
    }

    private void FireAt(int index, RadialSweepConfig config)
    {
        float angle = (360f / config.ProjectileCount) * index;
        if (!config.Clockwise) angle = -angle;
        Vector3 direction = Quaternion.Euler(0, angle, 0) * _selfTr.forward;
        Vector3 target = _selfTr.position + direction * config.Range;

        var ball = CombatPoolRegistry.Get<CannonBall>();
        ball.transform.position = _selfTr.position;
        ball.SetConfig(new CannonConfig(
            config.TargetLayerMask,
            config.Damage,
            config.ArcHeight,
            config.FlightDuration,
            config.AreaRadius
        ));
        ball.Init();
        ball.Fire(target);
    }
}
