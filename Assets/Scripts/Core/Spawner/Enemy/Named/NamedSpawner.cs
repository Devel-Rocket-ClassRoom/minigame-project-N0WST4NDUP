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

    [Header("Leash Config")]
    // leash 반경은 미니맵 반경보다 바깥(§5.5). 스폰 거리보다 커야 갓 스폰된 네임드가 즉시 디스폰되지 않음.
    [SerializeField] private float _leashRadius = 70f;
    [SerializeField] private float _leashCheckInterval = 1f;

    private float _spawnTimer;
    private float _leashTimer;

    private readonly List<Named> _alive = new();

    private void Update()
    {
        if (_anchor == null) return;

        Spawn();
        CheckLeash();
    }

    private void Spawn()
    {
        if (Time.time < _spawnTimer) return;
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

    // null(사망 Destroy) 정리 + leash 반경 밖 디스폰.
    // leash 디스폰은 OnDeadEvent를 거치지 않으므로 드롭이 없다 = 회피 성공(§5.5).
    private void CheckLeash()
    {
        if (Time.time < _leashTimer) return;

        float sqrLeash = _leashRadius * _leashRadius;
        Vector3 anchorPos = _anchor.position;

        for (int i = _alive.Count - 1; i >= 0; i--)
        {
            var named = _alive[i];

            if (named == null)
            {
                _alive.RemoveAt(i);
                continue;
            }

            if ((named.transform.position - anchorPos).sqrMagnitude > sqrLeash)
            {
                _alive.RemoveAt(i);
                Destroy(named.gameObject);
            }
        }
        _leashTimer = Time.time + _leashCheckInterval;
    }
}
