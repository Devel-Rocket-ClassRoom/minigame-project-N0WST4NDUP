using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "Pursue",
    story: "[Self] pursues [Target]",
    category: "Action/Boss",
    id: "4de1c98a-f8e7-44aa-b868-7561e61e2e91")]
public partial class PursueAction : Action
{
    private const float k_alignmentThresholdDeg = 1f;

    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private ShipMovement _movement;
    private Transform _selfTr;

    protected override Status OnStart()
    {
        if (Self.Value == null) return Status.Failure;
        _selfTr = Self.Value.transform;
        _movement = Self.Value.GetComponent<ShipMovement>();
        if (_movement == null) return Status.Failure;
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    protected override Status OnUpdate()
    {
        if (Target.Value == null) return Status.Running;

        Vector3 toTarget = Target.Value.transform.position - _selfTr.position;
        toTarget.y = 0f;
        float angle = Vector3.SignedAngle(_selfTr.forward, toTarget.normalized, Vector3.up);
        float turn = Mathf.Abs(angle) < k_alignmentThresholdDeg ? 0f : Mathf.Sign(angle);
        _movement.UpdateMove(1f, turn);
        return Status.Running;
    }
}
