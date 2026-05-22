using UnityEngine;

[CreateAssetMenu(
    fileName = "DropData",
    menuName = "ItemDrop/DropData")]
public class DropData : ScriptableObject
{
    [Tooltip("드롭하는 EXP 젬의 경험치 양")]
    public int XPReward = 1;
}