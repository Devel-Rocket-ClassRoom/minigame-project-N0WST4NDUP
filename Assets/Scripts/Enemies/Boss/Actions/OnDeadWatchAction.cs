using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "On Dead Watch",
    story: "Wait until [Self] dies",
    category: "Action/Boss",
    id: "7d4b9e23-5c18-4a76-b2f8-6e91c4a3d706")]
public partial class OnDeadWatchAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private ShipBody _body;
    private bool _triggered;

    protected override Status OnStart()
    {
        if (Self.Value == null) return Status.Failure;
        _body = Self.Value.GetComponent<ShipBody>();
        if (_body == null) return Status.Failure;
        _triggered = false;
        _body.OnDeadEvent += OnDead;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _triggered ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        if (_body != null) _body.OnDeadEvent -= OnDead;
    }

    private void OnDead()
    {
        _triggered = true;
    }
}
