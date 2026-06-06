using UnityEngine;

// 두 점을 잇는 지글거리는 번개 한 줄기. LineRenderer로 점-대-점을 그리고 짧은 수명 뒤 스스로 소멸.
// TODO(§3): 현재 풀링 없이 Instantiate/Destroy — 머지 전 ParticlePool 류 풀링으로 전환 필요(다량 생성 VFX).
[RequireComponent(typeof(LineRenderer))]
public class LightningBolt : MonoBehaviour
{
    [Header("Shape")]
    [Tooltip("선분 분할 수 — 클수록 더 잘게 지글거림")]
    [SerializeField] private int _segments = 6;
    [Tooltip("진행 방향 수직으로 흔들리는 폭(유닛)")]
    [SerializeField] private float _jitter = 0.3f;

    [Header("Lifetime")]
    [Tooltip("표시 시간(초) 후 자동 소멸")]
    [SerializeField] private float _lifetime = 0.08f;

    private LineRenderer _line;
    private float _timer;

    private void Awake() => _line = GetComponent<LineRenderer>();

    // from→to를 _segments로 나누고, 중간점만 진행 방향의 수직(수평면)으로 무작위 변위시켜 번개 모양을 만든다.
    public void Draw(Vector3 from, Vector3 to)
    {
        _line.positionCount = _segments + 1;

        Vector3 dir = to - from;
        Vector3 perp = Vector3.Cross(dir.normalized, Vector3.up).normalized;

        for (int i = 0; i <= _segments; i++)
        {
            float t = i / (float)_segments;
            Vector3 p = Vector3.Lerp(from, to, t);
            if (i != 0 && i != _segments)
                p += perp * Random.Range(-_jitter, _jitter);
            _line.SetPosition(i, p);
        }

        _timer = _lifetime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f) Destroy(gameObject);
    }
}
