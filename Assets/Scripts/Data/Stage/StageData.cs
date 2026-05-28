using UnityEngine;

[CreateAssetMenu(
    fileName = "StageData",
    menuName = "Stage/StageData")]
public class StageData : ScriptableObject
{
    [Header("Info")]
    public string StageName = "Stage 1";

    [Header("Boss")]
    public GameObject BossPrefab;
    public float BossSpawnAfterSec = 60f;

    // TODO: 네임드 처치 수 트리거 추후 추가 — public int NamedKillsRequired;
}
