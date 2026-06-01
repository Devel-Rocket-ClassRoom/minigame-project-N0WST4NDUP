using UnityEngine;

public class SubmarineDivingState : IEnemyState
{
    private readonly Submarine _submarine;
    private float _progress;

    public SubmarineDivingState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter()
    {
        _progress = 0f;
        _submarine.LayMine();
    }

    public void OnExit()
    {
        _submarine.ApplyDiveT(1f);
        _submarine.SetCollider(false);
    }

    public void OnTick()
    {
        _submarine.FleeStep(_submarine.SubmergedSpeedMult);

        _progress += Time.deltaTime / _submarine.TransitionDuration;
        if (_progress >= 1f)
        {
            _submarine.StateMachine.ChangeState(new SubmarineSubmergedFleeState(_submarine));
            return;
        }
        _submarine.ApplyDiveT(_progress);
    }
}
