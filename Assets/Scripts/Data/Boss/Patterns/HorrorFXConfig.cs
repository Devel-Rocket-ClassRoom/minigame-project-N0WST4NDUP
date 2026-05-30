using UnityEngine;

[CreateAssetMenu(
    fileName = "HorrorFXConfig",
    menuName = "Boss/Patterns/HorrorFXConfig")]
public class HorrorFXConfig : ScriptableObject
{
    [Header("Volume / Shake")]
    public float VolumeWeightLerpSec;
    public float PerlinAmplitude;
    public float PerlinFrequency;

    [Header("Player Slow")]
    [Range(0f, 1f)] public float PlayerSlowPercent;
    public float PlayerSlowDuration;
    public float PlayerSlowInterval;
}
