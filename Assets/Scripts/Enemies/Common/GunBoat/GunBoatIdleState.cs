using UnityEngine;

public class GunBoatIdleState : IEnemyState
{
    private readonly GunBoat _gunBoat;

    private float _idlingTimer;

    public GunBoatIdleState(GunBoat gunBoat)
    {
        _gunBoat = gunBoat;
    }

    public void OnEnter()
    {
        _idlingTimer = Time.time + _gunBoat.IdlingInterval;
    }

    public void OnExit()
    {
        return;
    }

    public void OnTick()
    {
        _gunBoat.FindClosestShip();
        if (_gunBoat.Target != null)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatChaseState(_gunBoat));
        }
        else if (Time.time > _idlingTimer)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatPatrolState(_gunBoat));
        }
    }
}