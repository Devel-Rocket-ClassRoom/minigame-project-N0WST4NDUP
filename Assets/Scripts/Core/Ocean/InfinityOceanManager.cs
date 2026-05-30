using UnityEngine;

public class InfinityOceanManager : MonoBehaviour
{
    [Header("Ocean Settings")]
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private float _tileSize = 20f * 3.5f; // 타일 기본 크기 (20)
    [SerializeField] private Vector3 _tileOffset = Vector3.zero;
    [SerializeField] private int _radius = 1;

    private Transform[] _tiles;

    [Header("Target Settings")]
    [SerializeField] private Transform _target;

    private Vector2Int _centerCell;

    public Vector3 TileOffset => new Vector3(_tileSize / 2, 0, _tileSize / 2) + _tileOffset;

    private void Awake()
    {
        _tiles = new Transform[(_radius * 2 + 1) * (_radius * 2 + 1)];
    }

    private void Start()
    {
        if (_target == null || _tilePrefab == null) return;

        int side = 2 * _radius + 1;
        _tiles = new Transform[side * side];
        for (int i = 0; i < _tiles.Length; i++)
        {
            var tile = Instantiate(_tilePrefab, Vector3.zero, Quaternion.identity, transform);
            _tiles[i] = tile.transform;
        }

        _centerCell = GetCell(_target.position);
        Vector2Int farAway = _centerCell + new Vector2Int(side + 1, side + 1);
        TileUpdate(farAway);
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector2Int cell = GetCell(_target.position);
        if (cell == _centerCell) return;

        Vector2Int oldCenter = _centerCell;
        _centerCell = cell;
        TileUpdate(oldCenter);
    }

    private void TileUpdate(Vector2Int oldCenter)
    {
        int side = 2 * _radius + 1;
        for (int dz = -_radius; dz <= _radius; dz++)
            for (int dx = -_radius; dx <= _radius; dx++)
            {
                int cx = _centerCell.x + dx;
                int cz = _centerCell.y + dz;

                // 이 셀이 이전 가시 범위에도 들어있었으면 이미 자리에 있음 → 스킵
                if (Mathf.Abs(cx - oldCenter.x) <= _radius &&
                    Mathf.Abs(cz - oldCenter.y) <= _radius)
                    continue;

                // 새 셀 — 모듈로로 슬롯 찾고 좌표 갱신
                int sx = ((cx % side) + side) % side;
                int sz = ((cz % side) + side) % side;
                _tiles[sx * side + sz].position = new Vector3(
                    cx * _tileSize, 0, cz * _tileSize) + TileOffset;
            }
    }

    private Vector2Int GetCell(Vector3 position) => GetCell(position.x, position.z);
    private Vector2Int GetCell(float x, float z)
    {
        return new Vector2Int(
            Mathf.RoundToInt(x / _tileSize),
            Mathf.RoundToInt(z / _tileSize)
        );
    }
}