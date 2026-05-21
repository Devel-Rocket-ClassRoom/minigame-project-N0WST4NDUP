
using UnityEngine;

public class GunBoatChaseState : IEnemyState
{
    private readonly GunBoat _gunBoat;

    public GunBoatChaseState(GunBoat gunBoat)
    {
        _gunBoat = gunBoat;
    }

    public void OnEnter()
    {
        return;
    }

    public void OnExit()
    {
        return;
    }

    public void OnTick()
    {
        _gunBoat.FindClosestShip();
        if (_gunBoat.Target == null)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatIdleState(_gunBoat));
            return;
        }

        Vector3 dir = (_gunBoat.Target.position - _gunBoat.transform.position).normalized;
        _gunBoat.transform.forward = dir;
        _gunBoat.transform.position += _gunBoat.transform.forward * (_gunBoat.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_gunBoat.transform.position, _gunBoat.Target.position) < _gunBoat.CombatData.MaxRange)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatAttackState(_gunBoat));
        }
    }
}