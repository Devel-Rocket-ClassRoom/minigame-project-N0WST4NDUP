using UnityEngine;

public enum RadarBlipKind
{
    Enemy, // 일반 몹 (보스·네임드 외 모든 태그)
    Named, // 네임드 (태그: Named)
    Boss,  // 보스 (이벤트로 별도 추적)
    // Drop — 드롭 표시 추가 시
}

public struct RadarBlip
{
    public Vector2 NormalizedPos; // 단위원 기준 (-1~1). Clamped면 가장자리(크기 1)로 보정됨
    public RadarBlipKind Kind;
    public bool Clamped;          // true = 레이더 범위 밖 → 가장자리 클램프(방향 표시)
}
