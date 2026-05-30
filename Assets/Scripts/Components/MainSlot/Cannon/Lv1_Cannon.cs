using UnityEngine;

public class Lv1_Cannon : CannonBase
{
    protected override float BallScale => 1f;

    public Lv1_Cannon(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("캐넌 레벨 1");
    }

    public override int Level => 1;
    public override bool CanUpgrade => true;

    public override CannonBase Upgrade()
    {
        var next = new Lv2_Cannon(_data, _stats);
        next.SetBarrel(Target, FirePoint, ArcHeight, FlightDuration);
        return next;
    }
}
