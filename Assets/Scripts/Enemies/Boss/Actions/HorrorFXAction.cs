using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Horror FX",
    story: "[Self] runs Horror FX",
    category: "Action/Boss",
    id: "e9f5a2b1-7c38-4f6a-8b34-5de92a4f8c04")]
public partial class HorrorFXAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Phase> RunOn;

    private HorrorFXController _fx;
    private BehaviorGraphAgent _agent;

    protected override Status OnStart()
    {
        if (Self.Value == null) return Status.Failure;
        _fx = Self.Value.GetComponent<HorrorFXController>();
        if (_fx == null) return Status.Failure;
        _agent = Self.Value.GetComponent<BehaviorGraphAgent>();
        _fx.enabled = true;
        Debug.Log($"[BT HorrorFX] Activated (RunOn={RunOn?.Value})");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (RunOn != null && _agent != null
            && _agent.GetVariable<Phase>("Phase", out var phaseVar)
            && phaseVar.Value != RunOn.Value)
        {
            Debug.Log($"[BT HorrorFX] Phase mismatch ({phaseVar.Value} vs RunOn={RunOn.Value}) → end");
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        if (_fx != null) _fx.enabled = false;
        Debug.Log("[BT HorrorFX] Deactivated");
    }
}
