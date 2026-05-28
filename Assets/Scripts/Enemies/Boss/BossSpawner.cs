using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private PirateLord _boss;
    [SerializeField] private float _spawnAfterSec = 60f;

    private float _elapsed;
    private bool _spawned;

    private void Awake()
    {
        if (_boss != null)
        {
            _boss.gameObject.SetActive(false);
            Debug.Log($"[BossSpawner] Boss disabled, will spawn after {_spawnAfterSec}s");
        }
    }

    private void Update()
    {
        if (_spawned) return;
        _elapsed += Time.deltaTime;
        if (_elapsed >= _spawnAfterSec)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (_boss == null) return;
        Debug.Log($"[BossSpawner] Spawning boss at {_elapsed:F1}s elapsed");
        _boss.gameObject.SetActive(true);
        _spawned = true;
    }
}
