using UnityEngine;

public class SubmarineSurfacingState : IEnemyState
{
    private readonly Submarine _submarine;
    private float _progress;

    public SubmarineSurfacingState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter()
    {
        _progress = 1f;
    }

    public void OnExit()
    {
        _submarine.ApplyDiveT(0f); // 끝 위치 보정
        _submarine.SetCollider(true);
    }

    public void OnTick()
    {
        _submarine.FleeStep(1f);

        _progress -= Time.deltaTime / _submarine.TransitionDuration;
        if (_progress <= 0f)
        {
            if (_submarine.Target != null)
                _submarine.StateMachine.ChangeState(new SubmarineSurfacedFleeState(_submarine));
            else
                _submarine.StateMachine.ChangeState(new SubmarineIdleState(_submarine));
            return;
        }
        _submarine.ApplyDiveT(_progress);
    }
}
