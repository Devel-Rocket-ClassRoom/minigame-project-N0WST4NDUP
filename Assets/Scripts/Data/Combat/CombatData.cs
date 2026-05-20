using UnityEngine;

[CreateAssetMenu(
    fileName = "CombatData",
    menuName = "Combat/CombatData")]
public class CombatData : ScriptableObject
{
    [Header("")]

    [Tooltip("데미지")]
    public float Damage = 10f;

    [Tooltip("사용 범위")]
    public float Range = 10f;

    [Tooltip("쿨다운(초)")]
    public float Cooldown = 1f;

    [Tooltip("광역 공격 여부")]
    public bool IsAreaAttack = false;

    [Tooltip("광역 공격 반경")]
    public float AreaRadius = 1f;
}