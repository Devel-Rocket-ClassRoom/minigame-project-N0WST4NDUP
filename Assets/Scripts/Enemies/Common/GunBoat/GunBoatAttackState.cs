using UnityEngine;

public class GunBoatAttackState : IEnemyState
{
    private readonly GunBoat _gunBoat;

    private float _attackTimer;

    public GunBoatAttackState(GunBoat gunBoat)
    {
        _gunBoat = gunBoat;
    }

    public void OnEnter()
    {
        _attackTimer = 0f;
    }

    public void OnExit()
    {
        return;
    }

    public void OnTick()
    {
        if (Time.time < _attackTimer) return;

        _gunBoat.OnFire();
        _attackTimer = Time.time + _gunBoat.CombatData.Cooldown;

        _gunBoat.FindClosestShip();
        if (_gunBoat.Target == null)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatIdleState(_gunBoat));
        }
        else if (Vector3.Distance(_gunBoat.transform.position, _gunBoat.Target.position) > _gunBoat.CombatData.Range)
        {
            _gunBoat.StateMachine.ChangeState(new GunBoatChaseState(_gunBoat));
        }
    }
}