using System.Linq;
using UnityEngine;

public class CommonSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _anchor;
    [SerializeField] private CommonPool[] _pools;

    [Header("Spawn Config")]
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private int _maxAlive = 20;

    private float _spawnTimer;

    public int AliveCount => _pools.Sum(p => p.Active);

    private void Update()
    {
        if (Time.time < _spawnTimer) return;
        if (AliveCount >= _maxAlive) return;

        var dir = Random.insideUnitCircle.normalized;
        var distance = Random.Range(20f, 40f); // TODO: 나중에 min, max 따로 빼서 작업
        var spawnPoint = _anchor.position + new Vector3(dir.x, 0f, dir.y) * distance;

        var pool = _pools[Random.Range(0, _pools.Length)]; // TODO: 밸런싱 단계에서 가중치 추가
        var enemy = pool.Get();
        enemy.transform.position = spawnPoint;
        enemy.transform.rotation = Quaternion.LookRotation(_anchor.position - enemy.transform.position);
        enemy.Init();

        _spawnTimer = Time.time + _spawnInterval;
    }
}