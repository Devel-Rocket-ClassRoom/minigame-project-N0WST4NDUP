using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Proximity Channel",
    story: "[Self] channels damage to nearby ships with [Config]",
    category: "Action/Boss",
    id: "c7d4f3a8-6b29-4d5e-91a2-4cd83b6e7f03")]
public partial class ProximityChannelAction : Action
{
    private const int k_bufferSize = 16;
    private static readonly Collider[] _buffer = new Collider[k_bufferSize];

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<ProximityChannelConfig> Config;
    [SerializeReference] public BlackboardVariable<Phase> RunOn;

    private Transform _selfTr;
    private BehaviorGraphAgent _agent;
    private float _tickTimer;

    protected override Status OnStart()
    {
        if (Self.Value == null || Config.Value == null) return Status.Failure;

        _selfTr = Self.Value.transform;
        _agent = Self.Value.GetComponent<BehaviorGraphAgent>();
        _tickTimer = Config.Value.DpsTickInterval;

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

        _tickTimer -= Time.deltaTime;
        if (_tickTimer > 0f) return Status.Running;
        _tickTimer = config.DpsTickInterval;

        int count = Physics.OverlapSphereNonAlloc(
            _selfTr.position, config.ZoneRadius, _buffer, config.TargetLayerMask);

        for (int i = 0; i < count; i++)
        {
            if (_buffer[i].TryGetComponent<ShipBody>(out var body))
            {
                body.OnDamaged(config.DpsPerTick);
            }
            ParticlePoolRegistry.Get(ParticleKind.Die).Play(_buffer[i].transform.position);
        }

        return Status.Running;
    }
}
