using UnityEngine;

public class GunBoatAttackState : IEnemyState
{
    private readonly GunBoat _gunBoat;

    public GunBoatAttackState(GunBoat gunBoat)
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
        if (!_gunBoat.CanFire) return;

        _gunBoat.OnFire();
        _gunBoat.ScheduleFireAfter(_gunBoat.CombatData.Cooldown);

        _gunBoat.FindClosestShip();
        if (_gunBoat.Target == null)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatIdleState(_gunBoat));
        }
        else if (Vector3.Distance(_gunBoat.transform.position, _gunBoat.Target.position) > _gunBoat.CombatData.MaxRange)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatChaseState(_gunBoat));
        }
    }
}