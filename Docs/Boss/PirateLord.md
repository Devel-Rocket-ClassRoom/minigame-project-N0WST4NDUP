# Pirate Lord (해적왕)

> 본 문서는 [GDD §5.6](../GDD.md) 보스 사양을 Pirate Lord 1체에 맞춰 구체화한다.
> 수치는 [Balancing.md §3.11](../Balancing.md)에 확정값으로 등재되어 있으며, 본 문서는 사양/구현 세부에 집중한다.

---

## 1. 개요

- **이름**: Pirate Lord (해적왕)
- **장르 내 역할**: v1.0 유일 보스. 스테이지 클리어 게이트
- **등장 트리거** ([GDD §5.6](../GDD.md))
    - **현재 구현**: 경과시간 단일 조건 (`StageData.BossSpawnAfterSec`).
    - **본래 사양**: `(경과시간 ≥ X분) AND (네임드 처치 수 ≥ Y)` — 네임드 시스템 미구현으로 시간 트리거만 사용. 네임드 시스템 도입 시 `StageData`에 `NamedKillsRequired` 필드 추가 예정.
- **이탈 불가**: leash 없음. 캐치업 추격 ([GDD §5.6](../GDD.md))
- **컨셉**: 해적선. 페이즈 3에서 유령선으로 변신하여 무적 상태로 추격 → "도망만 쳐도 승리"하는 호러 클라이맥스

---

## 2. 페이즈

| 페이즈 | HP 구간 | 무적 | 콜라이더 | 핵심 행동 |
|---|---|---|---|---|
| **P1** | 100% → 50% | X | ON | 사격 위주 (Radial Sweep + Mortar Rain) |
| **P2** | 50% → 0 | X | ON | 사격 위주 (P1과 동일 패턴, movement 속도만 ↑). Whirlpool / Ramming Charge 콤보는 **주말 후속** |
| **P3** | 100% → 0 (자연 감소) | O | OFF | 빠른 추격 + 근접 채널링 DoT + 공포 효과. 공격 패턴 없음 |

**전환 메커니즘** — BT가 결정, `PirateLord` MonoBehaviour가 부수효과 적용.

- **P1 → P2**: BT의 `HpThresholdWatchAction`이 HP 임계(0.5) 도달 감지 → Blackboard `Phase = P2` 갱신 → `PirateLord.Update`가 변수 변화 폴링 → `ApplyPhaseTransition(P2)` 호출 → movement 데이터 P2로 교체.
- **P2 → P3**: BT의 `OnDeadWatchAction`이 `ShipBody.OnDeadEvent` 구독 → 트리거 시 Phase 갱신 → `ApplyPhaseTransition(P3)` → Repair(MaxHP) + 콜라이더 OFF + decay 시작 + ghost ship 스왑 + `ShipBody.OnDeadEvent` 재구독(HandleBossDeath).
- **P3 종료**: HP decay가 0 도달 → `ShipBody.OnDeadEvent` 두 번째 발화 → `HandleBossDeath()` → `OnBossDeathEvent(position)` 발화 → `StageManager`가 다음 스테이지 진행 또는 `OnGameClear`.

---

## 3. 공격 패턴

전부 [GDD §5.3](../GDD.md) "투사체 패턴은 네임드/보스에만 적용" 예외 조항 사용.
수치는 [Balancing.md §3.11](../Balancing.md)에 확정 등재.

| 패턴 | 페이즈 | 압박 의도 | 구현 |
|---|---|---|---|
| **Radial Sweep** | P1, P2 | 회전 역방향 무빙 강제 (제자리 금지) | `RadialSweepAction` (BT). 보스 주위 360° 순차 발사 — 12발 × 0.3s = 3.6s 휘두름. CannonBall 풀 재사용. |
| **Mortar Rain** | P1, P2 | 안전지대 지속 갱신 (정지 금지) | `MortarRainAction` (BT). 플레이어 주변 8m 영역에 3발 흩뿌리기 → 1.5s 텔레그래프 → 0.6s 낙하. CannonBall(ArcHeight=0 직선 낙하) + `MortarTelegraph` 파티클. |
| **Whirlpool** | P2 | 같은 자리 못 있게 강제 (슬로우+풀링) | **주말 후속** — 미구현 |
| **Ramming Charge** | P2 | 횡축 무빙 강제 | **주말 후속** — 미구현 |
| **Proximity Channel** | P3 | 거리 유지 강제 | `ProximityChannelAction` (BT). `Physics.OverlapSphereNonAlloc`로 12m 반경 내 ShipBody에 0.5s마다 8 데미지(DPS 16). 본체 콜라이더와 분리. |
| **Horror FX** | P3 | 시각·이동 압박 | `HorrorFXAction` (BT) + `HorrorFXController` (MonoBehaviour). URP Volume weight 0→1 페이드 + Cinemachine Perlin + 플레이어 3s마다 -25% MoveSpeed 1.5s 슬로우. |

---

## 4. 페이즈 3 상세 (유령선)

`PirateLord.ApplyPhaseTransition(P3)`이 일괄 처리하는 부수효과:

- **무적**: `TryGetComponent<Collider>` → `enabled = false`. `OnDamaged` 호출 자체가 안 들어옴.
- **HP 자연 감소**: `PirateLord.Update`가 매 프레임 `_body.OnDamaged(Phase3DecayPerSecond * Time.deltaTime)` 호출. `PirateLordData.InvincibleTime=0`으로 i-frame 가드가 비활성이라 매 프레임 차감이 그대로 적용됨.
- **부활 처리**: `_body.Repair(_body.MaxHealth)` 호출로 HP를 즉시 MaxHealth로 복원. `ShipBody.OnDamaged`의 `IsDestroyed` 조기 종료가 해제됨. 동시에 `OnDeadEvent` 핸들러를 `HandleBossDeath`로 추가 구독 (P3 진짜 사망용).
- **모델 스왑**: `_defaultShip.SetActive(false)` + `_ghostShip.SetActive(true)`. 인스펙터에서 두 자식 GameObject를 wiring.
- **근접 채널링**: `ProximityChannelAction`이 매 0.5s `Physics.OverlapSphereNonAlloc(transform.position, 12m, ...)` 폴링 → 자기 페이즈가 아니면 즉시 `Status.Success`로 종료(페이즈 가드).
- **공포 효과** (`HorrorFXController`)
    - URP Volume(Lens Distortion + Chromatic Aberration + Vignette 등 사용자 wiring) weight 0→1 lerp (2s)
    - `CinemachineBasicMultiChannelPerlin` amplitude 0.8 / frequency 1.0
    - 플레이어 `ShipStats`에 3s 주기로 `MoveSpeed × PercentAdd(-0.25)` 모디파이어 추가, 1.5s 뒤 코루틴으로 제거(`ShipStats.RemoveModifier`).
- **추격 속도**: `PirateLordData.PhaseMovements[P3]` — MaxSpeed 14 (플레이어 10보다 빠름, 캐치업 추격).
- **승리 조건**: 플레이어가 도망·생존만 해도 P3 HP가 자연 감소로 0 도달 → `HandleBossDeath` → `OnBossDeathEvent` → 자동 승리.

---

## 5. 코드 구조 — 책임 분담

> v1.0 단일 보스지만, BT 노드는 다른 보스에서도 재사용 가능하도록 설계.
> Pirate Lord 고유 로직(유령선화)만 MonoBehaviour에 응집.

### 5.1 BT 액션 노드 (재사용 가능)

위치: `Assets/Scripts/Enemies/Boss/Actions/`

| 노드 | 역할 | 노드 인스펙터 필드 |
|---|---|---|
| `PursueAction` | 타겟 방향 추격 | `Self`, `Target` (둘 다 `BlackboardVariable<GameObject>`) |
| `HpThresholdWatchAction` | Self의 ShipBody HP가 임계 이하면 `Success` | `Self`, `ThresholdRatio` (`BlackboardVariable<float>`) |
| `OnDeadWatchAction` | Self의 ShipBody.OnDeadEvent 구독 → 발화 시 `Success` | `Self` |
| `RadialSweepAction` | 360° 순차 발사 (자체 쿨다운/상태 보유) | `Self`, `Config: RadialSweepConfig`, `RunOn: Phase` |
| `MortarRainAction` | 텔레그래프 → 지연 낙하 (자체 상태머신: Cooldown ↔ Telegraph) | `Self`, `Target`, `Config`, `RunOn` |
| `ProximityChannelAction` | OverlapSphere 폴링으로 주기적 DPS 틱 | `Self`, `Config`, `RunOn` |
| `HorrorFXAction` | `HorrorFXController.enabled` 토글 (FX 위주라 토글만) | `Self`, `RunOn` |

### 5.2 페이즈 가드

Unity.Behavior의 `SwitchComposite`가 자식 `Running` 상태에 갇혀 Phase 변수를 재평가하지 않는 BT 표준 동작 회피용.

각 패턴 액션의 `OnUpdate` 첫 줄:

```csharp
if (RunOn != null && _agent != null
    && _agent.GetVariable<Phase>("Phase", out var phaseVar)
    && phaseVar.Value != RunOn.Value)
{
    return Status.Success;
}
```

- 자기 페이즈가 아니면 즉시 `Success` → Switch가 새 케이스 평가 → 다음 페이즈 그룹 활성화.
- `RunOn`은 노드 인스펙터에서 P1/P2/P3 상수 입력. `ActivePhase`는 `Self.GetComponent<BehaviorGraphAgent>()`로 자동 조회 (wiring 부담 ↓).

### 5.3 MonoBehaviour (Pirate Lord 고유)

- `PirateLord.cs` — 페이즈 전환 부수효과 응집 (`ApplyPhaseTransition`), 사망 이벤트(`OnBossSpawned`/`OnPhaseChanged`/`OnBossDeathEvent`) 발화, BT의 `Phase`/`Target` Blackboard 변수 주입.
  - `Init(Transform target)` public — `StageManager`가 동적 Instantiate 직후 호출.
- `HorrorFXController.cs` — URP Volume·Cinemachine Perlin·Player ShipStats 등 **인스펙터 wiring 외부 참조**가 필요해 BT 노드로 흡수하지 않고 MonoBehaviour 유지. BT 액션은 `enabled` 토글만 담당. 슬로우 모디파이어 만료는 코루틴 + `ShipStats.RemoveModifier`.

### 5.4 새 보스 추가 가이드

1. BT 그래프는 `PirateLordBT.asset` 복제 후 패턴 노드만 교체.
2. 보스 고유 transition(유령선화 같은 부수효과)이 있으면 새 MonoBehaviour 작성 (`PirateLord.cs` 패턴).
3. `PirateLord.OnBossDeathEvent` 대신 보스별 정적 이벤트 또는 공용 인터페이스(`IBossEvents`) 도입 검토.

---

## 6. 데이터 자산 (ScriptableObject)

### 스크립트 위치

| 클래스 | 경로 |
|---|---|
| `PirateLordData` (HP·페이즈 임계·decay·PhaseMovements[3]) | `Assets/Scripts/Data/Boss/PirateLordData.cs` |
| `RadialSweepConfig` | `Assets/Scripts/Data/Boss/Patterns/RadialSweepConfig.cs` |
| `MortarRainConfig` | `Assets/Scripts/Data/Boss/Patterns/MortarRainConfig.cs` |
| `ProximityChannelConfig` | `Assets/Scripts/Data/Boss/Patterns/ProximityChannelConfig.cs` |
| `HorrorFXConfig` | `Assets/Scripts/Data/Boss/Patterns/HorrorFXConfig.cs` |
| `Phase` enum (`[BlackboardEnum]`) | `Assets/Scripts/Enemies/Boss/Phase.cs` |

> `PirateLordData`는 **페이즈 임계·decay·PhaseMovements만** 보유. Config 참조는 보유하지 않음 — 각 BT 노드 인스펙터에서 직접 wiring하는 게 일반화에 유리해 분리.

### 에셋 인스턴스

`Assets/ScriptableObjects/Boss/`:
- `PirateLord/PirateLordData.asset`
- `PirateLord/PirateLordBT.asset` (BehaviorGraph)
- `Patterns/RadialSweepConfig (PirateLord).asset`
- `Patterns/MortarRainConfig (PirateLord).asset`
- `Patterns/ProximityChannelConfig (PirateLord).asset`
- `Patterns/HorrorFXConfig (PirateLord).asset`

`Assets/ScriptableObjects/Movement/PirateLord/`:
- `P1MovementData.asset` / `P2MovementData.asset` / `P3MovementData.asset`

---

## 7. 이벤트 hook

```csharp
public static event Action<PirateLord> OnBossSpawned;    // 등장 연출 + HUD 전환 (.Body, .transform.position 등 접근)
public static event Action<Phase>      OnPhaseChanged;   // HUD HP바 페이즈 라벨 갱신
public static event Action<Vector3>    OnBossDeathEvent; // 스테이지 클리어 트리거 (StageManager 구독)
```

현재 소비처:
- `StageManager`가 `OnBossDeathEvent` 구독 → 다음 스테이지 진행 또는 `OnGameClear` 발화.
- `BossHPUI`가 `OnBossSpawned`/`OnPhaseChanged`/`OnBossDeathEvent` 구독 → 대기 게이지를 보스 HP 게이지로 전환, 페이즈 라벨/HP 수치 갱신, 사망 시 숨김.

미사용 (후속):
- `OnBossSpawned` — 등장 컷인/사운드 큐

---

## 8. 풀 의존성

- **CombatPool**: `CannonBall` — Radial Sweep + Mortar Rain 둘 다 동일 풀 재사용
- **ParticlePool**: `MortarTelegraph` (신규 추가됨, `ParticleKind.MortarTelegraph`)
- 보스 본체는 `StageManager.Instantiate`로 1체 생성 → 단일 인스턴스이므로 풀링 제외

---

## 9. 수치 (Balancing.md 참조)

전 수치 [Balancing.md §3.11](../Balancing.md)에 `구현 기본값`으로 등재 완료.
GDD `❓W2 확정` 마커는 본 작업에서 해소 — Whirlpool/Ramming(P2 콤보)만 주말 후속으로 남음.

---

## 10. 관련 문서

- [GDD.md §5.6](../GDD.md) — 보스 사양 원본
- [GDD.md §5.3](../GDD.md) — 투사체 도배 회피 원칙 (보스 예외)
- [Balancing.md §3.11](../Balancing.md) — 보스 수치 등재
