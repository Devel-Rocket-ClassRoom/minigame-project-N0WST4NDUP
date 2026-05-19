using UnityEngine;

[CreateAssetMenu(
    fileName = "ShipData",
    menuName = "Ship/ShipData")]
public class ShipData : ScriptableObject
{
    [Header("Info")]

    [Tooltip("이름")]
    public string Name = "Default Ship";

    [Tooltip("설명")]
    [TextArea] public string Description = "A sturdy ship body.";

    [Header("Stats")]

    [Tooltip("체력")]
    public float Health = 100f;

    [Tooltip("무적 시간(초)")]
    public float InvincibleTime = 1f;

    [Tooltip("적에게 가하는 피해량")]
    public float Damage = 20f;
}