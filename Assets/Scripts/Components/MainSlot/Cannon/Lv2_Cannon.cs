using UnityEngine;

public class Lv2_Cannon : CannonBase
{
    public Lv2_Cannon(CombatData data)
    {
        _data = data;
        Debug.Log("캐넌 레벨 2");
    }

    public override void Tick()
    {
        _cooldownTimer -= Time.deltaTime * 1.3f; // 임시

        if (!CanFire) return;

        FireProcess();
    }

    public override CannonBase Upgrade()
    {
        return this;
    }
}