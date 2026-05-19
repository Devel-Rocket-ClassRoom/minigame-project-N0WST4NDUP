public class EnemyStateMachine
{
    private IEnemyState _current;

    public IEnemyState Current => _current;

    public void ChangeState(IEnemyState state)
    {
        _current?.OnExit();
        _current = state;
        _current?.OnEnter();
    }

    public void OnTick()
    {
        _current?.OnTick();
    }
}