using UnityEngine;

public class Lv3_Cannon : CannonBase
{
    protected override float BallScale => 1.4f;

    public Lv3_Cannon(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("캐넌 레벨 3");
    }

    public override int Level => 3;
    public override bool CanUpgrade => false;

    public override CannonBase Upgrade()
    {
        return this;
    }
}