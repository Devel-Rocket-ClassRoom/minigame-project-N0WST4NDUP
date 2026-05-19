using UnityEngine;
using UnityEngine.UIElements;

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

}