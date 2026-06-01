using UnityEngine;

public class SubmarineSubmergedFleeState : IEnemyState
{
    private readonly Submarine _submarine;
    private float _elapsed;

    public SubmarineSubmergedFleeState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter()
    {
        _elapsed = 0f;
        _submarine.SetCollider(false);
    }

    public void OnExit() { }

    public void OnTick()
    {
        _submarine.FleeStep(_submarine.SubmergedSpeedMult);
        _elapsed += Time.deltaTime;

        // 잠수 체류 시간 경과 or 플레이어 이탈 → 출수
        if (_submarine.Target == null || _elapsed >= _submarine.SubmergedDuration)
        {
            _submarine.StateMachine.ChangeState(new SubmarineSurfacingState(_submarine));
        }
    }
}
