using UnityEngine;

[CreateAssetMenu(
    fileName = "ShipMovementData",
    menuName = "Movement/ShipMovementData")]
public class ShipMovementData : ScriptableObject
{
    [Header("Movement")]

    [Tooltip("최대 전진 속도")]
    public float MaxSpeed = 10f;

    [Tooltip("전진 가속도")]
    public float Acceleration = 2f;

    [Tooltip("최대 선회 각속도(초당 도)")]
    public float TurnSpeed = 20f;

    [Tooltip("측면 미끄러짐 저항")]
    [Range(0f, 1f)] public float LateralGrip = 0.5f;
}
