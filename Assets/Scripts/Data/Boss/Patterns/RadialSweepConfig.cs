using UnityEngine;

[CreateAssetMenu(
    fileName = "RadialSweepConfig",
    menuName = "Boss/Patterns/RadialSweepConfig")]
public class RadialSweepConfig : ScriptableObject
{
    [Header("Behavior Config")]
    public int ProjectileCount;
    public float ShotInterval;
    public bool Clockwise;
    public float Cooldown;
    public float TelegraphDuration;
    public float Range;

    [Header("Projectile Config")]
    public float Damage;
    public float ArcHeight;
    public float FlightDuration;
    public float AreaRadius;
    public LayerMask TargetLayerMask;
}