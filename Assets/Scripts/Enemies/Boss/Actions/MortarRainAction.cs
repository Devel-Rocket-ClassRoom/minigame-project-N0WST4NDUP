using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Mortar Rain",
    story: "[Self] rains mortars on [Target] with [Config]",
    category: "Action/Boss",
    id: "5b6c2d31-83a7-49b1-9e4c-2bf17a3c5e02")]
public partial class MortarRainAction : Action
{
    private enum State { Cooldown, Telegraph }

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<MortarRainConfig> Config;
    [SerializeReference] public BlackboardVariable<Phase> RunOn;

    private Transform _selfTr;
    private BehaviorGraphAgent _agent;
    private State _state;
    private float _timer;
    private Vector3[] _impactPoints;
    private PooledParticle[] _telegraphParticles;

    protected override Status OnStart()
    {
        if (Self.Value == null || Config.Value == null) return Status.Failure;
        _selfTr = Self.Value.transform;
        _agent = Self.Value.GetComponent<BehaviorGraphAgent>();
        _state = State.Cooldown;
        _timer = Config.Value.Cooldown;
        _telegraphParticles = new PooledParticle[Config.Value.ShellCount];
        Debug.Log($"[BT MortarRain] Activated (RunOn={RunOn?.Value})");
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Debug.Log("[BT MortarRain] Deactivated");
    }

    protected override Status OnUpdate()
    {
        if (RunOn != null && _agent != null
            && _agent.GetVariable<Phase>("Phase", out var phaseVar)
            && phaseVar.Value != RunOn.Value)
        {
            Debug.Log($"[BT MortarRain] Phase mismatch ({phaseVar.Value} vs RunOn={RunOn.Value}) → end");
            return Status.Success;
        }

        var config = Config.Value;
        _timer -= Time.deltaTime;
        if (_timer > 0f) return Status.Running;

        switch (_state)
        {
            case State.Cooldown:
                Debug.Log($"[BT MortarRain] Telegraph marking {config.ShellCount} shells");
                MarkTelegraphs(config);
                _state = State.Telegraph;
                _timer = config.TelegraphDuration;
                break;
            case State.Telegraph:
                Debug.Log($"[BT MortarRain] Firing {config.ShellCount} shells");
                FireShells(config);
                _state = State.Cooldown;
                _timer = config.Cooldown;
                break;
        }
        return Status.Running;
    }

    private void MarkTelegraphs(MortarRainConfig config)
    {
        Vector3 center = Target.Value != null ? Target.Value.transform.position : _selfTr.position;
        if (_impactPoints == null || _impactPoints.Length != config.ShellCount)
        {
            _impactPoints = new Vector3[config.ShellCount];
        }

        for (int i = 0; i < config.ShellCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * config.AreaRadius;
            Vector3 point = center + new Vector3(offset.x, 0f, offset.y);
            _impactPoints[i] = point;
            var particle = ParticlePoolRegistry.Get(ParticleKind.MortarTelegraph);
            particle.Play(point);
            _telegraphParticles[i] = particle;
        }
    }

    private void FireShells(MortarRainConfig config)
    {
        for (int i = 0; i < config.ShellCount; i++)
        {
            Vector3 target = _impactPoints[i];
            var ball = CombatPoolRegistry.Get<CannonBall>();
            ball.transform.position = target + Vector3.up * config.ArcHeight;
            ball.SetConfig(new CannonConfig(
                config.TargetLayerMask,
                config.Damage,
                config.ArcHeight,
                config.FlightDuration,
                config.ScatterRadius
            ));
            ball.Init();
            ball.Fire(target);
            _telegraphParticles[i]?.Stop();
        }
    }
}
