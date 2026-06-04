using System.Collections.Generic;
using UnityEngine;

public class NamedSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _anchor;
    [SerializeField] private Named _namedPrefab; // TODO: 종이 늘면 Named[] + 가중치로 확장

    [Header("Spawn Config")]
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxAlive = 3;
    [SerializeField] private float _spawnMinDistance = 20f;
    [SerializeField] private float _spawnMaxDistance = 40f;

    private float _spawnTimer;

    private readonly List<Named> _alive = new();

    private void Update()
    {
        if (_anchor == null) return;

        Spawn();
    }

    private void Spawn()
    {
        if (Time.time < _spawnTimer) return;

        Prune();
        if (_alive.Count >= _maxAlive) return;

        var dir = Random.insideUnitCircle.normalized;
        var distance = Random.Range(_spawnMinDistance, _spawnMaxDistance);
        var spawnPoint = _anchor.position + new Vector3(dir.x, 0f, dir.y) * distance;
        var rot = Quaternion.LookRotation(_anchor.position - spawnPoint);

        // Named는 Awake/Start에서 _shipData init + EquipRandomLoadout을 스스로 수행.
        var named = Instantiate(_namedPrefab, spawnPoint, rot);
        _alive.Add(named);

        _spawnTimer = Time.time + _spawnInterval;
    }

    // 사망(Destroy)으로 비워진 항목 정리 — leash 디스폰은 이동 구현 후 추가 예정.
    private void Prune()
    {
        for (int i = _alive.Count - 1; i >= 0; i--)
        {
            if (_alive[i] == null) _alive.RemoveAt(i);
        }
    }
}
