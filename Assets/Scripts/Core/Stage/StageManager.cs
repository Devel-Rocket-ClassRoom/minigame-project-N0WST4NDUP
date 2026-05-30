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
    public static float Elapsed { get; private set; }
    public static float BossSpawnAfterSec => CurrentStage != null ? CurrentStage.BossSpawnAfterSec : 0f;
    public static bool BossSpawned { get; private set; }

    public static event Action<StageData> OnStageStarted;
    public static event Action OnGameClear;

    private GameObject _currentBoss;

    private void OnDestroy()
    {
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
    }

    private void Start()
    {
        if (_stages == null || _stages.Length == 0) return;

        StageCount = _stages.Length;
        StartStage(0);
    }

    private void Update()
    {
        if (BossSpawned || CurrentStage == null) return;

        Elapsed += Time.deltaTime;
        if (Elapsed >= CurrentStage.BossSpawnAfterSec)
        {
            SpawnBoss();
        }
    }

    private void StartStage(int index)
    {
        CurrentStageIndex = index;
        CurrentStage = _stages[index];
        Elapsed = 0f;
        BossSpawned = false;
        _currentBoss = null;

        OnStageStarted?.Invoke(CurrentStage);
    }

    private void SpawnBoss()
    {
        if (CurrentStage.BossPrefab == null) return;
        if (_player == null) return;

        Vector3 spawnPos = _player.position + _bossSpawnOffset;
        Quaternion spawnRot = Quaternion.LookRotation(_player.position - spawnPos);
        _currentBoss = Instantiate(CurrentStage.BossPrefab, spawnPos, spawnRot);
        _currentBoss.GetComponent<PirateLord>().Init(_player); // 임시, 나중에 보스 상위 추상화로 Init
        BossSpawned = true;

        PirateLord.OnBossDeathEvent -= HandleBossDeath;
        PirateLord.OnBossDeathEvent += HandleBossDeath;
    }

    private void HandleBossDeath(Vector3 _)
    {
        PirateLord.OnBossDeathEvent -= HandleBossDeath;

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
            OnGameClear?.Invoke();
        }
    }
}
