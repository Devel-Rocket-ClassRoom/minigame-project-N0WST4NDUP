using UnityEngine;

public class SubmarineSurfacedFleeState : IEnemyState
{
    private readonly Submarine _submarine;
    private float _elapsed;

    public SubmarineSurfacedFleeState(Submarine submarine)
    {
        _submarine = submarine;
    }

    public void OnEnter()
    {
        _submarine.LayMine();
        _elapsed = 0f;
    }

    public void OnExit() { }

    public void OnTick()
    {
        _submarine.FleeStep(1f);
        _elapsed += Time.deltaTime;

        if (_submarine.Target == null)
        {
            // 플레이어 이탈 → 대기 복귀
            _submarine.StateMachine.ChangeState(new SubmarineIdleState(_submarine));
        }
        else if (_elapsed >= _submarine.SurfacedDuration)
        {
            // 출수 노출 시간 경과 & 아직 근처 → 재잠수
            _submarine.StateMachine.ChangeState(new SubmarineDivingState(_submarine));
        }
    }
}
