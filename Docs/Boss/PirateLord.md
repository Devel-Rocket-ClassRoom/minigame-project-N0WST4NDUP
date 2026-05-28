# Pirate Lord (해적왕)

> 본 문서는 [GDD §5.6](../GDD.md) 보스 사양을 Pirate Lord 1체에 맞춰 구체화한다.
> 수치는 작업 종료 시점에 확정 후 [Balancing.md §2.1](../Balancing.md)로 이동한다.

---

## 1. 개요

- **이름**: Pirate Lord (해적왕)
- **장르 내 역할**: v1.0 유일 보스. 스테이지 클리어 게이트
- **등장 트리거** ([GDD §5.6](../GDD.md))
    - `(경과시간 ≥ X분) AND (네임드 처치 수 ≥ Y)` — X/Y는 `❓플레이테스트 확정` (보스 스폰 이슈에서 결정)
- **이탈 불가**: leash 없음. 캐치업 추격 ([GDD §5.6](../GDD.md))
- **컨셉**: 해적선. 페이즈 3에서 유령선으로 변신하여 무적 상태로 추격 → "도망만 쳐도 승리"하는 호러 클라이맥스

---

## 2. 페이즈

| 페이즈 | HP 구간 | 무적 | 콜라이더 | 핵심 행동 |
|---|---|---|---|---|
| **P1** | 100% → 50% | X | ON | 사격 위주 (Broadside + Mortar) |
| **P2** | 50% → 0 | X | ON | Whirlpool→Ramming 콤보 추가, 사격 빈도 ↓ |
| **P3** | 100% → 0 (자연 감소) | O | OFF | 빠른 추격 + 근접 채널링 DoT + 공포 효과. 공격 패턴 없음 |

**전환 메커니즘**

- P1 → P2: HP 임계(50%) 교차 시 — `PhaseController.Update()` 폴링
- P2 → P3: HP 0 도달 → 즉시 부활(MaxHP) + 무적·콜라이더OFF + HP decay 시작
- P3 종료: HP decay가 0 도달 → 진짜 사망 → `OnBossDeathEvent` 발화

---

## 3. 공격 패턴

| 패턴 | 페이즈 | 압박 의도 | 데미지 | 쿨다운 | 비고 |
|---|---|---|---|---|---|
| **Broadside Volley** | P1, P2(빈도↓) | 보스 축선 회피 강제 (정면·후면이 안전지대) | `❓W2 확정` | `❓W2 확정` | 측면 부채꼴 다탄 동시 발사 |
| **Mortar Rain** | P1, P2(빈도↓) | 안전지대 지속 갱신 (정지 금지) | `❓W2 확정` | `❓W2 확정` | 화면 원형 텔레그래프 → 지연 폭발 |
| **Whirlpool** | P2 | 같은 자리 못 있게 강제 (슬로우+풀링) | 즉발 데미지 없음 | `❓W2 확정` | 플레이어 위치 추적, 일정 시간 후 소멸 |
| **Ramming Charge** | P2 | 횡축 무빙 강제 | `❓W2 확정` | `❓W2 확정` | 텔레그래프 라인 → 보스 본체 돌진 |
| **Proximity Channel** | P3 | 거리 유지 강제 | DPS `❓W2 확정` | 연속 | 자식 트리거 존, 본체 콜라이더와 분리 |

전부 [GDD §5.3](../GDD.md) "투사체 패턴은 네임드/보스에만 적용" 예외 조항 사용.

---

## 4. 페이즈 3 상세 (유령선)

- **무적**: 본체 콜라이더 OFF로 `OnDamaged` 호출 자체가 안 들어옴
- **콜라이더**: 본체 OFF. 자식 `ProximityDamageZone`만 ON (트리거)
- **HP 자연 감소**: `PirateLord`가 매 프레임 `ShipBody.OnDamaged(decayPerSec * dt)` 호출. **PirateLordData.InvincibleTime=0**으로 두면 i-frame 가드가 비활성이라 매 프레임 차감이 그대로 적용됨 (ShipBody 표면 변경 없음)
- **부활 처리**: `PirateLord`가 `ShipBody.OnDeadEvent`를 통해 구독 교체. P2 사망 시 첫 핸들러가 `ShipBody.Repair(MaxHealth)` 호출 후 자신을 unsubscribe하고 "진짜 사망" 핸들러로 교체. `Repair`는 공용 메서드 (플레이어·네임드 회복에도 재사용)
- **근접 채널링**: 자식 트리거 영역 진입 시 주기적 `IDamageable.OnDamaged(channelDps * interval)`
- **공포 효과** (HorrorFXController)
    - URP Volume(Lens Distortion + Chromatic Aberration + Vignette) weight 0→1 lerp
    - Cinemachine Basic Multi Channel Perlin amplitude/frequency 활성
    - 플레이어 ShipStats에 시간제한 MoveSpeed 음수 모디파이어 주기적 적용
- **추격 속도**: P1/P2보다 빠름 (`PirateLordData.PhaseMovement[2]`의 `MaxSpeed` — P3용 `ShipMovementData` 에셋에서 결정)
- **승리 조건**: 플레이어가 도망·생존만 해도 P3 HP가 자연 감소로 0 도달 → 자동 승리

---

## 5. 데이터 자산 (ScriptableObject)

스크립트 위치: `Assets/Scripts/Data/Boss/`

- `PirateLordData.cs` — Pirate Lord 통합 데이터 (`ShipData` 상속 — HP/InvincibleTime은 base, 페이즈 임계·페이즈별 `ShipMovementData[3]` 참조·P3 decay rate·각 패턴 Config 참조는 본 클래스)
- `BroadsideVolleyConfig.cs` — ProjectileCount, ArcAngle, Damage, Cooldown, TelegraphDuration
- `MortarRainConfig.cs` — ShellCount, AreaRadius, Damage, Cooldown, TelegraphDuration, ScatterRadius
- `WhirlpoolConfig.cs` — ZoneRadius, PullStrength, PlayerSlowPercent, Duration, Cooldown
- `RammingConfig.cs` — ChargeSpeed, ChargeDistance, Damage, TelegraphDuration, Cooldown
- `ProximityChannelConfig.cs` — ZoneRadius, DpsTickInterval, DpsPerTick
- `HorrorFXConfig.cs` — VolumeWeightLerpSec, PerlinAmplitude, PerlinFrequency, PlayerSlowPercent, PlayerSlowDuration, PlayerSlowInterval

에셋 인스턴스 1세트: `Assets/Data/Boss/PirateLord/` (작업 중 경로 확정).

---

## 6. 이벤트 hook

후속 UI/연출/클리어 이슈가 구독할 인터페이스 경계:

```csharp
public static event Action<Vector3> OnBossSpawned;    // 등장 연출
public static event Action<int>     OnPhaseChanged;   // HUD HP바 (페이즈 1/2/3)
public static event Action<Vector3> OnBossDeathEvent; // 스테이지 클리어 트리거
```

본 이슈에서는 이벤트 정의·발화만. 소비처는 후속 이슈.

---

## 7. 풀 의존성

- **CombatPool**: CannonBall (기존), MortarShell (신규)
- **ParticlePool**: MortarTelegraph (신규), Whirlpool (신규), Horror (신규)
- 보스 본체는 단일 인스턴스이므로 풀링 제외

---

## 8. 미정 수치 (`❓W2 확정`)

작업 종료 시 본 표를 확정값으로 갱신 + [Balancing.md §2.1](../Balancing.md) 이동.

| 항목 | 위치 | 비고 |
|---|---|---|
| 트리거 X분 / Y킬 | `❓플레이테스트 확정` | 보스 스폰 이슈로 이관 |
| 페이즈별 movement (3개) | `PirateLordData.PhaseMovement[]` (`ShipMovementData[3]`) | 플레이어 최대 속도 기반. 페이즈마다 MaxSpeed·TurnSpeed·Acceleration 자유 튜닝 |
| P1→P2 HP 임계 | `PirateLordData.Phase1ToPhase2Hp` | 초안 50% |
| P3 HP decay rate | `PirateLordData.Phase3DecayPerSec` | "도망 승리" 체감 시간 결정 |
| 패턴별 데미지·쿨다운·사거리 | 각 `*Config` SO | 5종 |
| 공포 효과 강도·슬로우 비율 | `HorrorFXConfig` | 시각·체감 조정 |
| 채널링 DPS·반경 | `ProximityChannelConfig` | P3 거리 압박 강도 |

---

## 9. 관련 문서

- [GDD.md §5.6](../GDD.md) — 보스 사양 원본
- [GDD.md §5.3](../GDD.md) — 투사체 도배 회피 원칙 (보스 예외)
- [Balancing.md §2.1](../Balancing.md) — 수치 확정 대기 영역
