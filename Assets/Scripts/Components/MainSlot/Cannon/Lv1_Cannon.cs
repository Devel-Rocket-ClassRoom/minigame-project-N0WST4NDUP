using UnityEngine;

public class Lv1_Cannon : CannonBase
{
    public Lv1_Cannon(CombatData data, ShipStats stats)
    {
        _data = data;
        _stats = stats;
        Debug.Log("캐넌 레벨 1");
    }

    public override void Tick()
    {
        TickCooldown();

        if (!CanFire) return;

        FireProcess();
    }

    public override CannonBase Upgrade()
    {
        return new Lv2_Cannon(_data, _stats);
    }
}
