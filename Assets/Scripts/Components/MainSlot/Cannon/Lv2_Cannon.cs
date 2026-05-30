using UnityEngine;

public class Lv2_Cannon : CannonBase
{
    protected override float BallScale => 1.2f;

    public Lv2_Cannon(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("캐넌 레벨 2");
    }

    public override int Level => 2;
    public override bool CanUpgrade => true;

    public override CannonBase Upgrade()
    {
        var next = new Lv3_Cannon(_data, _stats);
        next.SetBarrel(Target, FirePoint, ArcHeight, FlightDuration);
        return next;
    }
}