using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 표현 레이어(프로토). RadarModel이 준 blip을 풀링된 Image로 그린다.
/// 스윕은 연출용 회전, blip은 항상 표시(추적용).
/// </summary>
public class MinimapUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _radarArea; // 원형 영역 (반경 = width / 2)
    [SerializeField] private RectTransform _sweep;      // 회전 스윕 라인 (연출, 옵션)
    [SerializeField] private Image _blipPrefab;         // 점 스프라이트(Knob 등)
    [SerializeField] private Transform _blipParent;

    [Header("Config")]
    [SerializeField] private float _worldRange = 60f;
    [SerializeField] private LayerMask _enemyMask;      // 일반 적 레이어만 (보스/플레이어 제외)
    [SerializeField] private float _sweepDegPerSec = 120f;

    [Header("Blip Colors")]
    [SerializeField] private Color _enemyColor = Color.white;
    [SerializeField] private Color _namedColor = Color.white;
    [SerializeField] private Color _bossColor = Color.red;

    private RadarModel _model;
    private Vector3 _groundRight;
    private Vector3 _groundUp;
    private float _sweepAngle;

    private Transform _bossTransform;
    private readonly List<Image> _pool = new();
    private int _activeCount;

    private void Awake()
    {
        if (_camera == null) _camera = Camera.main;
        _model = new RadarModel(_worldRange, _enemyMask);

        // 카메라 고정 → 화면 축을 바닥면에 투영한 값은 상수
        _groundRight = Flat(_camera.transform.right);
        _groundUp = Flat(_camera.transform.up); // 화면 위 = 바닥면 '안쪽' 방향
    }

    private void OnEnable()
    {
        PirateLord.OnBossSpawned += HandleBossSpawned;
        PirateLord.OnBossDeathEvent += HandleBossDeath;
    }

    private void OnDisable()
    {
        PirateLord.OnBossSpawned -= HandleBossSpawned;
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
    }

    private void HandleBossSpawned(PirateLord boss) => _bossTransform = boss.transform;
    private void HandleBossDeath(Vector3 _) => _bossTransform = null;

    private void LateUpdate()
    {
        if (_player == null) return;

        if (_sweep != null)
        {
            _sweepAngle -= _sweepDegPerSec * Time.deltaTime;
            _sweep.localRotation = Quaternion.Euler(0f, 0f, _sweepAngle);
        }

        IReadOnlyList<RadarBlip> blips = _model.Sample(_player.position, _groundRight, _groundUp, _bossTransform);
        Draw(blips);
    }

    private void Draw(IReadOnlyList<RadarBlip> blips)
    {
        float uiRadius = _radarArea.rect.width * 0.5f;
        EnsurePool(blips.Count);

        for (int i = 0; i < blips.Count; i++)
        {
            RadarBlip blip = blips[i];
            Image img = _pool[i];
            img.gameObject.SetActive(true);
            img.color = blip.Kind switch
            {
                RadarBlipKind.Boss => _bossColor,
                RadarBlipKind.Named => _namedColor,
                _ => _enemyColor,
            };
            img.rectTransform.localScale = blip.Kind switch
            {
                RadarBlipKind.Boss => Vector3.one * 1.5f,
                RadarBlipKind.Named => Vector3.one * 1.2f,
                _ => Vector3.one,
            };
            img.rectTransform.anchoredPosition = blip.NormalizedPos * uiRadius;

            // 범위 밖 보스: 바깥쪽을 향해 회전 (화살표 스프라이트 교체 대비)
            img.rectTransform.localRotation = blip.Clamped
                ? Quaternion.FromToRotation(Vector3.up, new(blip.NormalizedPos.x, blip.NormalizedPos.y, 0f))
                : Quaternion.identity;
        }

        for (int i = blips.Count; i < _activeCount; i++)
            _pool[i].gameObject.SetActive(false);

        _activeCount = blips.Count;
    }

    private void EnsurePool(int needed)
    {
        while (_pool.Count < needed)
        {
            _pool.Add(Instantiate(_blipPrefab, _blipParent));
        }
    }

    private static Vector3 Flat(Vector3 v)
    {
        v.y = 0f;
        return v.normalized;
    }
}
