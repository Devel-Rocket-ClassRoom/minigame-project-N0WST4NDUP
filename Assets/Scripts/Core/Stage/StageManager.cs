using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData[] _stages;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _bossSpawnOffset = new(100, 0, 100);

    public static StageData CurrentStage { get; private set; }
    public static int CurrentStageIndex { get; private set; } = -1;
    public static int StageCount { get; private set; }

    public static event Action<StageData> OnStageStarted;
    public static event Action OnGameClear;

    private float _elapsed;
    private bool _bossSpawned;
    private GameObject _currentBoss;

    private void OnDestroy()
    {
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
    }

    private void Start()
    {
        if (_stages == null || _stages.Length == 0)
        {
            Debug.LogError("[StageManager] No stages configured");
            return;
        }

        StageCount = _stages.Length;
        StartStage(0);
    }

    private void Update()
    {
        if (_bossSpawned || CurrentStage == null) return;

        _elapsed += Time.deltaTime;
        Debug.Log($"[StageManager] Elapsed time: {_elapsed:F1}s / {CurrentStage.BossSpawnAfterSec}s");
        if (_elapsed >= CurrentStage.BossSpawnAfterSec)
        {
            SpawnBoss();
        }
    }

    private void StartStage(int index)
    {
        CurrentStageIndex = index;
        CurrentStage = _stages[index];
        _elapsed = 0f;
        _bossSpawned = false;
        _currentBoss = null;

        Debug.Log($"[StageManager] Stage {index + 1}/{StageCount} started: {CurrentStage.StageName} (boss in {CurrentStage.BossSpawnAfterSec}s)");
        OnStageStarted?.Invoke(CurrentStage);
    }

    private void SpawnBoss()
    {
        if (CurrentStage.BossPrefab == null)
        {
            Debug.LogError($"[StageManager] Boss prefab missing for stage {CurrentStage.StageName}");
            return;
        }

        Vector3 spawnPos = _player.position + _bossSpawnOffset;
        Quaternion spawnRot = Quaternion.LookRotation(_player.position - spawnPos);
        _currentBoss = Instantiate(CurrentStage.BossPrefab, spawnPos, spawnRot);
        _currentBoss.GetComponent<PirateLord>().Init(_player); // 임시, 나중에 보스 상위 추상화로 Init
        _bossSpawned = true;

        PirateLord.OnBossDeathEvent -= HandleBossDeath;
        PirateLord.OnBossDeathEvent += HandleBossDeath;

        Debug.Log($"[StageManager] Boss spawned at {_elapsed:F1}s elapsed");
    }

    private void HandleBossDeath(Vector3 _)
    {
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
        Debug.Log($"[StageManager] Boss defeated — stage {CurrentStageIndex + 1}/{StageCount}");

        if (_currentBoss != null)
        {
            Destroy(_currentBoss);
            _currentBoss = null;
        }

        if (CurrentStageIndex + 1 < _stages.Length)
        {
            StartStage(CurrentStageIndex + 1);
        }
        else
        {
            Debug.Log("[StageManager] All stages cleared — game over");
            OnGameClear?.Invoke();
        }
    }
}
