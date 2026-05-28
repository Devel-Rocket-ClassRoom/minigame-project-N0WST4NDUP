using UnityEngine;

[CreateAssetMenu(
    fileName = "PirateLordData",
    menuName = "Boss/PirateLordData", order = 0)]
public class PirateLordData : ShipData
{
    [Header("Phase Config")]
    public float Phase1ToPhase2HpThreshold = 0.5f;
    public float Phase3DecayPerSecond = 50f;

    [Header("Movements")]
    public ShipMovementData[] PhaseMovements = new ShipMovementData[3];
}