public class SubmarineIdleState : IEnemyState
{
    private readonly Submarine _submarine;

    public SubmarineIdleState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter() { }
    public void OnExit() { }

    public void OnTick()
    {
        _submarine.FindClosestShip();
        if (_submarine.Target != null)
        {
            _submarine.StateMachine.ChangeState(new SubmarineDivingState(_submarine));
        }
    }
}
