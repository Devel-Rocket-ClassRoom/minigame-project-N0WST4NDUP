using UnityEngine;

public class GunBoatPatrolState : IEnemyState
{
    private readonly GunBoat _gunBoat;

    private Vector3 _patrolPoint;
    private float _threshold = 0.2f;

    public GunBoatPatrolState(GunBoat gunBoat)
    {
        _gunBoat = gunBoat;
    }

    public void OnEnter()
    {
        _patrolPoint = _gunBoat.GetNextPatrolPoint();
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
            return;
        }

        Vector3 dir = (_patrolPoint - _gunBoat.transform.position).normalized;
        _gunBoat.transform.forward = dir;
        _gunBoat.transform.position += _gunBoat.transform.forward * (_gunBoat.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_gunBoat.transform.position, _patrolPoint) < _threshold)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatIdleState(_gunBoat));
        }
    }
}