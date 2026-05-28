using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "HP Threshold Watch",
    story: "Wait until [Self] HP <= [ThresholdRatio]",
    category: "Action/Boss",
    id: "3f8a6c47-2b91-4e15-87cd-9a3f7c6d8b05")]
public partial class HpThresholdWatchAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> ThresholdRatio;

    private ShipBody _body;

    protected override Status OnStart()
    {
        if (Self.Value == null) return Status.Failure;
        _body = Self.Value.GetComponent<ShipBody>();
        if (_body == null) return Status.Failure;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_body == null) return Status.Failure;
        if (_body.CurrentHealth <= _body.MaxHealth * ThresholdRatio.Value)
        {
            Debug.Log($"[BT HpThresholdWatch] Triggered at HP {_body.CurrentHealth}/{_body.MaxHealth} (ratio {ThresholdRatio.Value})");
            return Status.Success;
        }
        return Status.Running;
    }
}
