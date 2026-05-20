using System.Collections.Generic;
using UnityEngine;

public class CommonSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _anchor;
    [SerializeField] private CommonPool[] _pools;

    [Header("Spawn Config")]
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private int _maxAlive = 100;

    [Header("Cull Config")]
    [SerializeField] private float _cullRadius = 60f;
    [SerializeField] private float _cullCheckInterval = 1f;

    private float _spawnTimer;

    private readonly List<CommonEnemyBase> _alive = new();
    private float _cullTimer;

    private void Update()
    {
        if (_anchor == null) return;

        Spawn();
        CheckCull();
    }

    private void Spawn()
    {
        if (Time.time < _spawnTimer) return;
        if (_alive.Count >= _maxAlive) return;

        var dir = Random.insideUnitCircle.normalized;
        var distance = Random.Range(20f, 40f); // TODO: 나중에 min, max 따로 빼서 작업
        var spawnPoint = _anchor.position + new Vector3(dir.x, 0f, dir.y) * distance;

        var pool = _pools[Random.Range(0, _pools.Length)]; // TODO: 밸런싱 단계에서 가중치 추가
        var enemy = pool.Get();
        enemy.transform.position = spawnPoint;
        enemy.transform.rotation = Quaternion.LookRotation(_anchor.position - enemy.transform.position);
        enemy.Init();
        _alive.Add(enemy);

        _spawnTimer = Time.time + _spawnInterval;
    }

    private void CheckCull()
    {
        if (Time.time < _cullTimer) return;

        float sqrCull = _cullRadius * _cullRadius;
        Vector3 anchorPos = _anchor.position;

        for (int i = _alive.Count - 1; i >= 0; i--)
        {
            var enemy = _alive[i];

            if (enemy == null || !enemy.gameObject.activeSelf)
            {
                _alive.RemoveAt(i);
                continue;
            }

            if ((enemy.transform.position - anchorPos).sqrMagnitude > sqrCull)
            {
                _alive.RemoveAt(i);
                enemy.ReturnToPool();
            }
        }
        _cullTimer = Time.time + _cullCheckInterval;
    }
}