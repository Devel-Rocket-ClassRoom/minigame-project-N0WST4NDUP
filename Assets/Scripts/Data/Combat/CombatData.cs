using UnityEngine;

[CreateAssetMenu(
    fileName = "CombatData",
    menuName = "Combat/CombatData")]
public class CombatData : ScriptableObject
{
    [Header("Default Config")]
    [Tooltip("데미지")]
    public float Damage = 10f;

    [Tooltip("최소 범위")]
    public float MinRange = 1f;
    [Tooltip("최대 범위")]
    public float MaxRange = 10f;

    [Tooltip("쿨다운(초)")]
    public float Cooldown = 1f;

    [Header("Area Config")]
    [Tooltip("광역 공격 여부")]
    public bool IsAreaAttack = false;

    [Tooltip("광역 공격 반경")]
    public float AreaRadius = 1f;
}