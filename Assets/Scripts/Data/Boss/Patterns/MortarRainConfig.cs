using UnityEngine;

[CreateAssetMenu(
    fileName = "MortarRainConfig",
    menuName = "Boss/Patterns/MortarRainConfig")]
public class MortarRainConfig : ScriptableObject
{
    [Header("Behavior Config")]
    public int ShellCount;
    public float Cooldown;
    public float TelegraphDuration;
    public float AreaRadius;

    [Header("Projectile Config")]
    public float Damage;
    public float ArcHeight;
    public float FlightDuration;
    public float ScatterRadius;
    public LayerMask TargetLayerMask;
}
