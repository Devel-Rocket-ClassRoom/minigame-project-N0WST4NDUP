using UnityEngine;

public class SubmarineFleeState : IEnemyState
{
    private readonly Submarine _submarine;

    public SubmarineFleeState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter()
    {
        _submarine.SetCollider(false);
    }

    public void OnExit()
    {
        return;
    }

    public void OnTick()
    {
        _submarine.FindClosestShip();
        if (_submarine.Target == null)
        {
            _submarine.StateMachine.ChangeState(new SubmarineSurfacingState(_submarine));
            return;
        }

        var diff = _submarine.transform.position - _submarine.Target.position;
        diff.y = 0f;
        if (diff.sqrMagnitude < 0.0001f) return;

        var dir = diff.normalized;
        _submarine.transform.forward = dir;
        _submarine.transform.position += _submarine.transform.forward * (_submarine.MoveSpeed * Time.deltaTime);
    }
}
