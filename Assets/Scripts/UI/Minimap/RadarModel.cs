using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 순수 로직: 플레이어 기준 추적 대상을 레이더 정규화 좌표(-1~1)로 변환한다.
/// UI 비의존 — MinimapUI가 매 프레임 Sample()을 호출한다.
/// </summary>
public class RadarModel
{
    private const int k_BufferSize = 128;
    private const string k_BossTag = "Boss";
    private const string k_NamedTag = "Named";

    private readonly Collider[] _buffer = new Collider[k_BufferSize];
    private readonly List<RadarBlip> _blips = new();

    private readonly float _worldRange;
    private readonly LayerMask _enemyMask;

    public RadarModel(float worldRange, LayerMask enemyMask)
    {
        _worldRange = worldRange;
        _enemyMask = enemyMask;
    }

    /// <param name="groundRight">카메라(화면) 우측 축을 바닥면에 투영·정규화한 값. 카메라 고정이라 상수.</param>
    /// <param name="groundUp">카메라(화면) 위 축을 바닥면에 투영·정규화한 값.</param>
    /// <param name="boss">보스 미존재면 null. 보스는 Enemy 레이어를 공유하지만 이벤트로 별도 추적하므로
    /// OverlapSphere 결과에서는 태그로 제외하고, 범위 밖이어도 가장자리 클램프로 항상 표시한다.</param>
    public IReadOnlyList<RadarBlip> Sample(Vector3 playerPos, Vector3 groundRight, Vector3 groundUp, Transform boss)
    {
        _blips.Clear();

        // 일반 적/네임드: 범위 안만 표시. 태그로 종류 분류, 보스 태그는 제외(아래에서 별도 처리)
        int count = Physics.OverlapSphereNonAlloc(playerPos, _worldRange, _buffer, _enemyMask);
        for (int i = 0; i < count; i++)
        {
            Collider col = _buffer[i];
            if (col.CompareTag(k_BossTag)) continue;

            _blips.Add(new RadarBlip
            {
                NormalizedPos = ToRadar(col.transform.position - playerPos, groundRight, groundUp),
                Kind = col.CompareTag(k_NamedTag) ? RadarBlipKind.Named : RadarBlipKind.Enemy,
                Clamped = false,
            });
        }

        // 보스: 범위 밖이면 가장자리 클램프(방향 화살표)
        if (boss != null)
        {
            Vector2 norm = ToRadar(boss.position - playerPos, groundRight, groundUp);
            bool clamped = norm.sqrMagnitude > 1f;
            if (clamped) norm = norm.normalized;

            _blips.Add(new RadarBlip
            {
                NormalizedPos = norm,
                Kind = RadarBlipKind.Boss,
                Clamped = clamped,
            });
        }

        return _blips;
    }

    private Vector2 ToRadar(Vector3 delta, Vector3 groundRight, Vector3 groundUp)
    {
        return new Vector2(
            Vector3.Dot(delta, groundRight),
            Vector3.Dot(delta, groundUp)) / _worldRange;
    }
}
