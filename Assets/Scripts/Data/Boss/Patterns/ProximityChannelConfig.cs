using UnityEngine;

[CreateAssetMenu(
    fileName = "ProximityChannelConfig",
    menuName = "Boss/Patterns/ProximityChannelConfig")]
public class ProximityChannelConfig : ScriptableObject
{
    public float ZoneRadius;
    public float DpsTickInterval;
    public float DpsPerTick;
    public LayerMask TargetLayerMask;
}
