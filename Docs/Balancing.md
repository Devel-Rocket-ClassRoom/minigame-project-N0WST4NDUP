# Balancing — ShipSurivor

> 본 문서는 게임 밸런싱 수치를 **"어떤 영역인지 / 값은 얼마인지 / 왜 그렇게 정했는지"** 세 가지를
> 항상 함께 기록한다. [Docs/GDD.md](GDD.md) §11의 `❓` 항목이 확정되면 이 문서로 이동한다.
>
> 작성 전 반드시 §0(문서의 목적과 의의)을 읽고, 모든 수치의 근거가 §0과 충돌하지 않는지 확인한다.

---

## 0. 문서의 목적과 의의

이 섹션은 문서 전체의 작성 기준이다. **§3에 등재되는 모든 수치의 `근거`는 이 섹션을 위배해서는 안 된다.**

### 0.1 목적

- 밸런싱 수치를 **영역 / 값 / 근거** 3요소로 기록하는 단일 장소(SSoT의 수치 레이어).
- GDD의 `❓` 마커 값이 플레이테스트로 확정되면 이 문서로 이동한다.
- 근거를 함께 남겨, 플레이테스트 중 수치가 일관성 없이 흔들리는 것을 막는다.

### 0.2 타겟 (GDD §1·§3)

- **캐주얼 코어** — 짧은 시간 안에 빌드 다양성을 체험하고 싶은 플레이어.
- **1런 약 10분** — 모든 수치는 10분 세션 안에서 의미가 완결되어야 한다.
- **"정밀 회피"가 아니라 "궤적 예측"** 으로 살아남는 감각. 트위치성 반응 요구를 밸런싱으로 강제하지 않는다.

### 0.3 이루고자 하는 목표 (GDD §2·§4)

- **세 가지 감각의 결합** — 자동 사격(Vampire Survivors) + 컴포넌트 빌드(Time Wasters) + 선박 조작감(Battleship).
- **다층 루프의 리듬** — 30초 루프(즉각 체감) / 5분 루프(빌드 의사결정) / 10분 루프(세션 마무리).
- **드롭 경쟁의 긴장감** — 핵심 차별점. 네임드 드롭을 두고 플레이어와 적이 동일 규칙으로 흡수 경쟁한다.
  늦으면 적이 강해진다. 수치는 이 **시간/공간/우선순위 경쟁**의 긴장을 유지·강화하는 방향으로 정한다.

### 0.4 하지 말아야 할 것 (밸런싱 안티패턴)

- **`❓` 값 임의 확정 금지** — 플레이테스트 없이 미확정 수치를 추정값으로 채우지 않는다 (GDD §0 · CLAUDE.md §7).
- **OUT OF SCOPE 시스템 밸런싱 금지** — GDD §9의 멀티플레이 / 메타 진행 / 스토리 / RL·ML AI / 부활 아이템
  관련 수치는 이 문서에 등재하지 않는다.
- **근거 없는 수치 금지** — 모든 수치는 §0.2 타겟 또는 §0.3 목표에 연결된 근거를 가져야 한다.
- **수치 코드 박기 금지** — 밸런싱 수치는 ScriptableObject 또는 별도 데이터로 분리해 핫스왑 가능하게 한다 (CLAUDE.md §3).

### 0.5 SSoT 관계

- **GDD가 진실의 원천이다.** GDD와 이 문서가 충돌하면 GDD를 우선한다.
- 수치를 확정해 이 문서에 등재할 때는 **GDD의 해당 `❓` 마커도 함께 갱신**한다.

---

## 1. 항목 작성 규칙

§3에 등재하는 모든 밸런싱 항목은 아래 **구조화 블록** 형식을 따른다.

### 1.1 구조화 블록 템플릿

```
### N.M 항목명 (`SourceClass` 또는 데이터 소스)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| FieldName | 10 | m/s | ... |

- **근거**: 왜 이 값인가. §0.2 타겟 또는 §0.3 목표 중 최소 하나를 명시적으로 인용한다.
- **상태**: 아래 상태 라벨 중 하나.
```

### 1.2 근거 작성 규칙 (필수)

- 모든 `근거` 문단은 **§0.2 타겟 또는 §0.3 목표 중 최소 하나를 명시적으로 인용**해야 한다.
- §0과 충돌하는 근거는 등재 금지다. 예:
  - 1런 10분 세션을 깨뜨리는 수치 (§0.2 위배).
  - OUT OF SCOPE 시스템을 전제로 한 수치 (§0.4 위배).
  - 트위치성 정밀 회피를 강제하는 수치 (§0.2 "궤적 예측" 위배).

### 1.3 상태 라벨

GDD 마일스톤(§10)과 1:1로 대응한다.

| 라벨 | 의미 |
|---|---|
| `확정` | 플레이테스트로 검증·확정됨 |
| `구현 기본값` | 코드/에셋에 들어가 있으나 플레이테스트 미검증 — 출발점 값 |
| `W1 확정 예정` | W1(05/18~05/24) 게이트에서 확정 |
| `W2 확정 예정` | W2(05/25~05/31)에서 확정 |
| `미확정(❓)` | 확정 마일스톤 미정 — GDD `❓`와 동기화 |

> `❓`·`W1/W2 확정 예정` 항목은 §0.4에 따라 임의 수치를 미리 채우지 않는다.

---

## 2. 향후 추가 영역 & 추가 절차

현재 확정된 영역은 §3.1(선박 이동)뿐이다. 나머지 영역은 GDD에서 대부분 `❓` 상태이며,
아래 표의 마일스톤에 도달할 때 §2.2 절차에 따라 §3에 블록으로 추가된다.

### 2.1 추가 예정 영역

| 영역 | GDD 참조 | 확정 마일스톤 | 현재 상태 |
|---|---|---|---|
| 자동 사격 (타입별 발사 규칙·사거리) | §5.2 | W1 | **부분 등재 (§3.6)** — 주포(캐넌) 1차 구현. 부포·어뢰 등 타 타입은 여전히 `미확정(❓)` |
| 적 스폰·일반 몹 스탯 (HP/데미지/EXP/등장률) | §5.3.1 | 플레이테스트 | **부분 등재 (§3.2~3.5)** — 일반 몹 3종 `구현 기본값`. EXP·등장률·스폰 곡선은 여전히 `미확정(❓)` |
| **부하 테스트 임계** (동시 적/투사체/드롭/VFX @60fps) | §5.3.2 | W1 (P0) | 미확정(❓) — W1 종료 전 도출 |
| EXP / 레벨업 곡선 | §5.4 | W1 | **부분 등재 (§3.7)** — `PlayerXP` 선형 곡선 `구현 기본값`. 플레이테스트로 곡선 형태(선형 vs 지수) 재검토 필요. |
| EXP 드롭·자석·젬 이동 | §5.4 | W1 | **부분 등재 (§3.8)** — 자석 반경 5m, 젬 이동 10m/s `구현 기본값`. 드롭당 EXP는 `XPDropper` 인스펙터 (현재 기본 1). |
| ShipStats / Modifier 시스템 | §5.4 / §6.1 | — | **신규 등재 (§3.9)** — 스탯 정의·계산식. 시스템 자체는 `구현 완성`, 풀에 들어갈 모디파이어 값은 플레이테스트 미확정 |
| 강화 카드 풀 (3택 레벨업) | §5.4 | W1~W2 | **부분 등재 (§3.10)** — 카드 4종 SO 작성, 풀에 스탯 모디파이어 3종만 연결. 컴포넌트 카드(Cannon/Double/Triple)는 SO 존재하나 풀 미연결. |
| 네임드 (드롭 타이머·흡수 우선순위·leash 반경) | §5.5 | W1 / 플레이테스트 | 미확정(❓) |
| 보스 (트리거·캐치업 속도·패턴 단계) | §5.6 | 플레이테스트 / W2 | **부분 등재 (§3.11)** — Pirate Lord 1체의 HP·페이즈 임계·decay·P1/P2 사격 패턴 2종·P3 채널링+FX `구현 기본값`. P2 콤보(Whirlpool/Ramming)는 **주말 후속**, 보스 등장 트리거 X분/Y킬은 네임드 시스템 미구현으로 시간 단일 조건만. |
| 컴포넌트 슬롯 (카테고리·개수·Lv 상한·레벨업 효과) | §6.1 | W1 / W2 | 미확정(❓) — Main/Sub/Rear 골격만 구현. 카테고리 확정/슬롯 Lv 상한 미정. |
| 피해 / 사망 (i-frame 무적시간) | §5.7 | W1 | 미확정(❓) |

> **부하 테스트 임계**(§5.3.2)는 스폰 매니저 상한·풀 사이즈의 기준이 되는 P0 항목이다. 우선 확정한다.

### 2.2 수치 확정 시 추가 절차

수치가 플레이테스트/부하 테스트로 확정되면:

1. 플레이테스트 또는 부하 테스트로 수치를 도출한다.
2. 해당 영역의 §1.1 구조화 블록을 §3에 신규 추가한다 (기존 블록이면 값·상태를 갱신).
3. `근거`에 §0.2 타겟 / §0.3 목표를 인용하고, `상태` 라벨을 갱신한다.
4. **GDD의 해당 `❓` 마커를 제거하고 수치를 반영**한다 (SSoT 동기화 — §0.5).
5. 수치 자체는 ScriptableObject / 데이터 에셋에 반영하고, 이 문서에는 값과 근거를 기록한다.

---

## 3. 현재 밸런싱 수치

### 3.0.1 인플레이 카메라 (`CinemachineCamera` + `CinemachineFollow`)

소스: [Assets/Scenes/MovementTest.unity](../Assets/Scenes/MovementTest.unity) `InPlay Camera` 게임오브젝트
(Cinemachine 컴포넌트 직배치). Main Camera에는 `CinemachineBrain` 부착.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `FieldOfView` | 60 | deg | 시야각 |
| `NearClipPlane` | 10 | m | 근거리 컬링 |
| `FarClipPlane` | 70 | m | 원거리 가시 한계 — 화면뷰에서 네임드/드롭을 인지할 수 있는 거리의 상한 |
| `FollowOffset` (월드) | (-10, 18, -10) | m | 선박 기준 카메라 위치 오프셋 |
| Pitch (X) | 45 | deg | GDD §0·§8 "쿼터뷰 45°" 충족 |
| Yaw (Y) | 45 | deg | 카메라 방위 (대각선 isometric) |
| `PositionDamping` | (6, 2, 6) | — | 추적 응답 지연. Y는 빠르게(2), XZ는 부드럽게(6) |
| `RotationDamping` | (1, 1, 1) | — | (BindingMode=WorldSpace라 실질 영향 없음) |
| `BindingMode` | 4 (WorldSpace) | — | 선박 회전을 무시하고 월드축 기준 추적 → GDD §8 "회전 없음" 구조적 보장 |
| `DefaultBlend` (Brain) | EaseInOut · 2s | — | 가상 카메라 간 블렌드 — 향후 줌·컷씬용 |

### 3.1 선박 이동 (`ShipMovementData`)

소스: [Assets/Scripts/Data/Ship/ShipMovementData.cs](../Assets/Scripts/Data/Ship/ShipMovementData.cs),
에셋 `Assets/ScriptableObjects/DefaultShip.asset`.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `MaxSpeed` | 10 | m/s | 최대 전진 속도 |
| `Acceleration` | 2 | m/s² | 전진 가속도 |
| `BrakeStrength` | 2 | — | 브레이크 강도 |
| `TurnSpeed` | 60 | deg/s | 최대 선회 각속도 |
| `LateralGrip` | 0.5 | 0–1 | 측면 미끄러짐 저항 |

- **근거**: 현재 값은 PR #44에서 설정한 **구현 기본값**으로, 관성 이동·선회·브레이크 감각의 출발점이다.
  §0.3 목표의 "선박 조작감" 결합과 §0.2 타겟의 "정밀 회피가 아닌 궤적 예측으로 살아남는 감각"을 위한 초기
  셋업이다. 다만 GDD §5.1은 가속/감속 곡선·최대 속도·회전 반경·브레이크 강도를 모두 `❓`로 두고 있어,
  현재 값은 검증 전 출발점일 뿐이다.
- **상태**: `구현 기본값` — W1 카메라/조작감 튜닝 게이트(GDD §10)에서 `확정`으로 승격 예정.

### 3.2 일반 몹 공통 감지·이동 (`CommonEnemyBase`)

소스: [Assets/Scripts/Enemies/Common/CommonEnemyBase.cs](../Assets/Scripts/Enemies/Common/CommonEnemyBase.cs).
`[SerializeField]` 필드라 프리팹/인스턴스마다 인스펙터에서 오버라이드 가능. 아래는 **클래스 기본값**이며,
실제 적용값은 각 적 프리팹에서 인스펙터로 확인한다.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `_shipLayerMask` | (인스펙터 지정) | LayerMask | 감지 대상 — 플레이어 함선 레이어 |
| `_detectRange` | 10 | m | 감지 반경 (OverlapSphere) |
| `_detectInterval` | 2.5 | s | 감지 갱신 주기 (스로틀) |
| `_moveSpeed` | 6 | m/s | 이동 속도 |
| `k_DetectBufferSize` | 8 | — | `OverlapSphereNonAlloc` 결과 버퍼 크기 (`const`, 풀링 시에도 GC-free) |

- **근거**: §0.2 "1런 10분"과 §0.3 "다층 루프의 30초 리듬"을 만족하기 위한 출발점.
  감지 반경 10m는 카메라 가시 영역(`FarClipPlane` 70m, §3.0.1)보다 작아 "적이 시야 안에서 행동을 시작하는"
  체감 확보. 이동 속도 6m/s는 플레이어 `MaxSpeed` 10m/s(§3.1)보다 느려 도주 여지를 남기되 압박은 유지.
  감지 간격 2.5s는 매 프레임 `Physics.OverlapSphere` 호출을 피해 §5.3.2 부하 테스트 임계 부담을 줄인다.
- **상태**: `구현 기본값` — W1 부하 테스트 + 플레이테스트 후 적별 오버라이드 확정 예정.

### 3.3 일반 몹 — 나룻배 (`SailBoat` / `SailBoat_Data`)

소스: [Assets/Scripts/Enemies/Common/SailBoat/SailBoat.cs](../Assets/Scripts/Enemies/Common/SailBoat/SailBoat.cs),
에셋 [Assets/ScriptableObjects/Ship/Common/SailBoat_Data.asset](../Assets/ScriptableObjects/Ship/Common/SailBoat_Data.asset).
**GDD §5.3.1 #1** 매핑.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Health` | 10 | HP | 잡몹답게 1~2발에 처치되는 출발점 |
| `InvincibleTime` | 1 | s | 피격 i-frame |
| 공격 데미지 | `_body.CurrentHealth` | HP | **자살 충돌** — 충돌 시 자기 잔여 HP만큼 데미지 후 `Destroy` |
| 행동 | 매 프레임 가장 가까운 함선 직진 추격 | — | FSM 없이 `Update`에서 직접 처리 |

- **근거**: GDD §5.3.1 #1 "가장 흔한 잡몹, 밀집 압박용". §0.3 30초 루프의 자주 등장하는 잡몹.
  HP 10은 출발점 — 빠른 처치감으로 도파민 루프 유지.
- **상태**: `구현 기본값`.
- **✅ 의도된 디자인**: 충돌 데미지가 `_body.CurrentHealth`로 잔여 HP에 연동된다. 향후 스테이지 난이도
  상승으로 나룻배 HP가 스케일링되면 데미지도 **선형으로 증가**한다. 별도 데미지 상수를 두지 않고 HP 한 축으로
  난이도/위협을 동시에 조정하는 단순 스케일링 패턴. GDD §5.3.1 #1의 `❓` 데미지 값은 이 규칙으로 해소됨.

### 3.4 일반 몹 — 소형 함선 (`GunBoat` / `GunBoat_Data` / `CombatData`)

소스: [Assets/Scripts/Enemies/Common/GunBoat/](../Assets/Scripts/Enemies/Common/GunBoat/) 일괄,
에셋 [GunBoat_Data.asset](../Assets/ScriptableObjects/Ship/Common/GunBoat_Data.asset),
[CombatData.asset](../Assets/ScriptableObjects/Combat/CombatData.asset).
**GDD §5.3.1 #2** 매핑. FSM 4상태: `Idle → Patrol → Chase → Attack`.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Health` | 15 | HP | 나룻배보다 약간 단단 |
| `InvincibleTime` | 1 | s | 피격 i-frame |
| `_idlingInterval` | 3 | s | Idle 대기 시간 — 끝나면 Patrol로 |
| `_patrolPointCount` | 7 | 개 | 스폰 위치 주변 패트롤 포인트 수 |
| `_patrolPointRadius` | 5 | m | 패트롤 포인트 분포 반경 (insideUnitCircle) |
| `CombatData.Damage` | 10 | HP | 즉발 데미지 |
| `CombatData.MinRange` | 1 | m | (GunBoat 미사용) — 캐넌 랜덤 발사 거리 하한용 필드. 클래스 기본값. |
| `CombatData.MaxRange` | 10 | m | Chase→Attack 전이 거리. **`_detectRange`(10) 이하여야 함**. 클래스 기본값. |
| `CombatData.Cooldown` | 1 | s | Attack 재발사 쿨다운 |
| `CombatData.IsAreaAttack` | false | — | 광역 공격 여부 |
| `CombatData.AreaRadius` | 1 | m | 광역 공격 반경 (현재 사용 안 함) |

- **근거**: GDD §5.3.1 #2 "사거리 유지가 회피의 핵심, 거리 의사결정 유도".
  §0.2 "궤적 예측" — MaxRange 10m와 MoveSpeed 6m/s 조합으로 플레이어가 "다가오는 사거리"를 예측하고
  피할 수 있는 출발점. 패트롤은 GDD 표 본문에 없는 코드 측 추가로, "정찰 중인 적" 느낌을 주어
  스폰 직후 어색함을 줄이는 보조 행동.
- **상태**: `구현 기본값`.
- **⚠️ 자산 마이그레이션 잔재**: `CombatData.asset` YAML에는 구 필드 `Range: 9`가 남아있으나, 코드
  필드명이 `MinRange`/`MaxRange`로 분리되며(`[FormerlySerializedAs]` 미부여) 해당 값은 직렬화에서
  끊겨 클래스 기본값(`MaxRange = 10`)이 적용된다. 즉 Chase→Attack 전이 거리는 사실상 1m 증가(9 → 10).
  W1 부하/플레이테스트 시 자산을 재저장하면 표기/실제가 다시 일치할 예정.
- **✅ GDD 갱신 반영**: 일반 몹은 Rigidbody 미사용·`transform` 직접 조작 방식으로 통일됨에 따라 물리 기반
  자연 감속이 불가능하다. GDD §5.3.1 #2 본문도 "추격 → 사거리 진입 시 **정지** → 공격"으로 의식적으로
  갱신되어 코드와 정렬됨 (CLAUDE.md §3 SSoT 원칙).

### 3.5 일반 몹 — 잠수함 (`Submarine` / `Submarine_Data`) — 부분 구현

소스: [Assets/Scripts/Enemies/Common/Submarine/](../Assets/Scripts/Enemies/Common/Submarine/) 일괄,
에셋 [Submarine_Data.asset](../Assets/ScriptableObjects/Ship/Common/Submarine_Data.asset).
**GDD §5.3.1 #3** 매핑. FSM 4상태: `Idle → Diving → Flee → Surfacing`.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Health` | 50 | HP | 다른 일반 몹 대비 가장 단단 (잠수 무적 시간 고려) |
| `InvincibleTime` | 1 | s | 피격 i-frame (수면 노출 시) |
| `_divingOffset` | (인스펙터) | m | 자식 모델이 잠수 시 내려갈 오프셋 (기본 `Vector3.down`) |
| `_divingDuration` | 1 | s | 잠수/부상 보간 시간 (smoothstep ease-in-out) |
| 공격 데미지 | — | — | **🚧 미구현** (`SetMine()` 메서드만 존재, 호출처 없음) |

- **근거**: GDD §5.3.1 #3 "잠수 → 이동 → 출수 → 1회 공격 → 재잠수". HP 50은 잠수 중 콜라이더 비활성(무적)을
  감안한 보정 — 수면 노출 시간이 짧기에 그 짧은 윈도우에 처치 가능한 수준.
- **상태**: `구현 기본값` (HP/보간) · `🚧 부분 구현` (공격 사이클 전체 미구현).
- **🚧 디자인 미확정 — 추후 구현 예정**:
  - **지뢰(Mine) 공격 사이클**: 지뢰 프리팹/디자인이 아직 확정되지 않아 의식적으로 보류 중. 잠수함 밸런싱
    (잠수 주기·부상 주기·지뢰 설치 타이밍 등)이 결정된 시점에 `SetMine()` 트리거와 함께 일괄 구현 예정.
    현재는 `Flee` 상태에서 타겟 잃을 때까지 잠수만 하는 골격 상태로, GDD §5.3.1 #3의 "잠수 → 이동 → 출수
    → 1회 공격 → 재잠수" 사이클은 아직 충족하지 않는다.
  - **잠수 중 시인성 보조**: GDD §5.3.1 #3 비고 + §11 리스크 — 그림자/물결/미니맵 마커 중 1개 W1에
    프로토 후 채택 필요. 현재 잠수 시 시각 단서 없음.
  - **잠수 중 무적**: 잠수 진입 시 `SetCollider(false)` 호출은 되어 있으나, 프리팹의 `_collider` 인스펙터
    참조 연결 검증 필요.

### 3.6 주포 — 캐넌 (`CannonBase` 계열 / `CannonData` / `CannonBall`)

소스: [Assets/Scripts/Components/MainSlot/Cannon/](../Assets/Scripts/Components/MainSlot/Cannon/),
[Assets/Scripts/Combat/CannonBall/CannonBall.cs](../Assets/Scripts/Combat/CannonBall/CannonBall.cs),
에셋 [CannonData.asset](../Assets/ScriptableObjects/Combat/CannonData.asset).
**GDD §5.2 자동 사격 / §6.1 슬롯 & 컴포넌트** 매핑 — 주포 슬롯 1차 구현.

#### CannonData (SO)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Damage` | 50 | HP | 피격 대상 데미지 (적용 로직은 충돌 처리 미구현) |
| `MinRange` | 1 | m | 랜덤 발사 거리 하한. 클래스 기본값 (자산 YAML은 구 `Range`만 잔존). |
| `MaxRange` | 10 | m | 랜덤 발사 거리 상한. 클래스 기본값. |
| `Cooldown` | 2 | s | 발사 간격 |
| `IsAreaAttack` | true | — | 광역 공격 여부 (적용 로직 미구현) |
| `AreaRadius` | 1 | m | 광역 반경 (사용 안 함) |

#### 캐넌 본체 — 발사 파라미터

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Lv1_Cannon.Upward` | 5 | — | 수직 임펄스 크기 (`const`, Lv1 전용). 수평 = 랜덤 단위벡터 × Range, 합성하여 포물선 궤적. |
| 발사 방향 | XZ 평면 랜덤 360° | — | `Random.Range(0, 2π)`로 수평 방향 결정 (타게팅 미구현, 무지향). |

#### 업그레이드 사슬 (데코레이터 패턴)

| 단계 | 클래스 | 역할 |
|---|---|---|
| 1 | `Lv1_Cannon` | 단일 발사 (수직 5 임펄스) |
| 2 | `Lv2_Cannon` | 단일 발사, Lv1 대체 (스탯 강화 자리표) |
| 3 | `DoubleCannon` | 데코레이터 — 내부 캐넌의 `FireProcess`를 1 Tick에 2회 실행 |
| 4 | `TripleCannon` | 데코레이터 — 1 Tick에 3회 실행 |

**데코레이터 cooldown 위임 규약 (중요)**: `DoubleCannon`/`TripleCannon`은 `CannonBase`를 상속하지만
자기 자신의 `_cooldownTimer`를 **절대 세팅하지 않는다**. 대신 `CanFire`와 `TickCooldown`을 `override`해
inner `_cannon`으로 위임 — 가장 안쪽 leaf cannon(Lv1/Lv2)의 타이머 하나로 전체 체인이 발사 주기를 결정한다.
이로써 `TripleCannon(DoubleCannon(Lv1))` 스택 시 **1 cooldown 주기에 2×3=6발이 동시 발사**된다.
초기 구현에서는 wrapper의 자체 `_cooldownTimer`가 0으로 방치돼 항상 `CanFire=true`가 되어 무한 발사
버그가 있었으며, [CannonBase.cs:9, :60](../Assets/Scripts/Components/MainSlot/Cannon/CannonBase.cs)의
`virtual` 승격 + 두 데코레이터의 위임 override로 수정됨.

업그레이드 트리거는 현재 두 경로:
- **레벨업 카드** (`UpgradeUI` → `UpgradePool.Pick`) — 정상 경로. 단, 현재 `UpgradePool.asset`에는 캐넌 계열
  SO(`Cannon.asset / MainDoubleWrapper.asset / MainTripleWrapper.asset`)가 **풀에 미연결**이라 카드로 등장하지 않음.
- 테스트용 키 입력은 `CannonAttachable`에서 제거됨 (이전 `Alpha1` 핫키 → 카드 시스템으로 일원화).

#### CannonBall (포탄, `CombatItemBase` 풀링 대상)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `SplashDuration` | 3.5 | s | 수면 닿은 뒤 splash 표시 유지 시간. `const`. |
| 수면 충돌 처리 | `y < 0` 시 `isKinematic = true` + y → 0 스냅 | — | 가라앉음 방지. Splash 종료 시 `ReturnToPool()`. |

- **근거**: GDD §5.2 "슬롯에 장착된 컴포넌트가 각자의 쿨다운/타게팅 규칙으로 자동 발사" 충족을 위한
  첫 슬롯 구현. §0.3 "자동 사격(Vampire Survivors)" 결합의 출발점. Damage 50은 GunBoat HP 15(§3.4)·
  나룻배 HP 10(§3.3) 기준 1~2발 처치 — §0.3 30초 루프의 즉시 체감 확보. Cooldown 2s는 발사가 시각적으로
  드물어 보이지 않는 출발점이며 더블/트리플 데코레이터로 체감 DPS를 키우는 설계 여지를 남긴다.
  데미지·궤적·랜덤 방향 모두 **타게팅 미구현**으로, 적 명중을 전제로 한 밸런싱은 W1 부하/플레이테스트 후
  본격 등재한다.
- **상태**: `구현 기본값` — W1 게이트에서 타게팅·데미지 적용 로직 추가 후 재평가.
- **🚧 미구현 / 추후**:
  - **타게팅**: 현재 무지향(랜덤). GDD §5.2 "타입별 발사 규칙"의 주포=전방 규약은 미적용.
  - **데미지 적용**: `Damage 50`은 SO에 박혀 있으나 충돌 시 `IDamageable` 호출 경로 없음.
  - **광역 공격**: `IsAreaAttack=true`이지만 실제 splash 반경 데미지 처리 없음.
  - **수명/맥스 라이프**: 수평 발사로 수면을 못 만나면 영구 부유 가능. 타이머 컷오프 미구현.
  - **`CannonData.asset` 마이그레이션**: `Range: 10` 잔존 → 인스펙터 재저장 시 `MinRange`/`MaxRange`로 YAML 갱신 예정.

### 3.7 EXP 곡선 (`PlayerXP`)

소스: [Assets/Scripts/Player/PlayerXP.cs](../Assets/Scripts/Player/PlayerXP.cs).
**GDD §5.4 EXP / 레벨업** 매핑.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `_baseXp` | 10 | XP | Lv1 → Lv2 필요량 |
| `_stepXp` | 3 | XP | 레벨당 필요량 증가폭 |
| 곡선 식 | `MaxXpForLevel(lv) = baseXp + (lv-1) × stepXp` | — | **선형** 증가 |
| 예시 (Lv1→Lv10) | 10 / 13 / 16 / 19 / 22 / 25 / 28 / 31 / 34 / 37 | XP | 누적 233 XP |

- **근거**: §0.2 "1런 10분" 안에서 5분 루프(빌드 의사결정)를 발생시킬 만큼의 레벨업 빈도가 필요.
  선형 곡선은 후반에도 레벨업이 자주 발생해 카드 선택 의사결정을 지속적으로 던지는 출발점.
  지수 곡선 채택 시 후반 정체로 30초 루프(§0.3) 신선도가 떨어질 수 있어 의식적으로 선형 선택.
  드롭당 XP가 1(§3.8)이므로 Lv2 도달은 일반 몹 10마리 처치에 해당 — 30초 루프와 정합.
- **상태**: `구현 기본값` — W1 플레이테스트에서 곡선 형태(선형 vs 완만한 지수)·계수 재검토.
- **🚧 미구현 / 추후**:
  - **레벨업 시 보상 변화**: 현재 모든 레벨이 동일하게 3택 카드 1회 제공. 특정 레벨에서 추가 보상(슬롯 해금 등) 검토 가능.
  - **HP 재생·EXP 흡수 반경 확장 등 패시브 스탯** (GDD §5.4 후보): 현재 `StatType` enum에 미정의.

### 3.8 EXP 드롭·자석·젬 이동 (`XPDropper` / `XPMagnet` / `XPGem`)

소스: [Assets/Scripts/Shared/ItemDrop/XPGem.cs](../Assets/Scripts/Shared/ItemDrop/XPGem.cs),
[XPDropper.cs](../Assets/Scripts/Shared/ItemDrop/XPDropper.cs),
[Assets/Scripts/Player/XPMagenet.cs](../Assets/Scripts/Player/XPMagenet.cs) (파일명 typo: `XPMagenet`).
**GDD §5.4** 매핑. 풀링: [XPGemPool.cs](../Assets/Scripts/Core/Pool/ItemDrop/XPGemPool.cs) (Singleton).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `XPDropper._xp` | 1 | XP | 적 사망 시 젬 1개에 부여되는 XP량. 인스펙터 오버라이드. |
| `XPDropper._active` | (인스펙터) | bool | false면 드롭 무시. 적별로 ON/OFF 가능. |
| `XPMagnet._radius` | 5 | m | 자석 흡인 반경. `SphereCollider isTrigger=true`로 진입 감지 → `OnPick(target)` 호출. |
| `XPGem._moveSpeed` | 10 | m/s | 흡인 시작 후 타겟을 향한 직선 이동 속도. |
| 흡인 동작 | 직선 추적 (관성·곡선 없음) | — | `dir = (target - pos).normalized; pos += dir × speed × dt` |
| 흡수 조건 | 트리거 충돌 + `gameObject.layer == _targetLayer` | — | 자석 진입 시 캐싱한 레이어와 일치해야 `PlayerXP.AddXp` 호출 후 풀로 반환 |

- **근거**: §0.3 "30초 루프 즉시 체감" — 자석 반경 5m는 플레이어 시야(카메라 가시 영역 §3.0.1) 안에서
  처치 직후 흡인이 시작되는 거리. 젬 속도 10m/s는 플레이어 `MaxSpeed`(§3.1)와 동일해 정지 상태일 때
  ≈0.5초 안에 도달해 도파민 루프 유지, 이동 중에는 따라잡는 텐션도 발생.
  드롭당 1 XP는 §3.7 곡선과 직결 — Lv2 진입에 10마리 처치 필요.
- **상태**: `구현 기본값` — W1 부하/플레이테스트로 자석 반경·젬 속도 튜닝.
- **⚠️ 코드 품질 노트**: `XPMagenet.cs` 파일/클래스명에 typo (`Magenet` → 정확히는 `Magnet`).
  리네이밍은 별도 chore 이슈로 분리 권장.
- **🚧 미구현 / 추후**:
  - **드롭 타이머·소멸 연출**: EXP 젬은 시간 만료 없이 영구 상주 (GDD §5.5 네임드 드롭과 달리 일반 EXP 젬은 타이머 불필요로 보임 — 디자인 확정 필요).
  - **드롭 경쟁** (GDD §2·§5.5): 네임드 시스템 자체가 미구현이라 적의 EXP 흡수 흐름 없음.

### 3.9 ShipStats / Modifier 시스템 (`ShipStats` / `Modifier` / `StatType`)

소스: [Assets/Scripts/Shared/Modifier/](../Assets/Scripts/Shared/Modifier/).
**GDD §5.4 강화 카드 / §6.1 슬롯** 매핑. 모든 컴포넌트의 동적 스탯 조회 단일 창구.

#### 정의

| 항목 | 값 / 형식 | 설명 |
|---|---|---|
| `StatType` enum | `Damage / Range / FireRate / AreaRadius / Health / MoveSpeed / TurnSpeed` (7종) | 게임 전체에서 사용하는 스탯 카테고리. 추가 시 enum 갱신 + UI 표시 연동 필요. |
| `ModifierOp` enum | `Add / PercentAdd` | 합산 방식. |
| `Modifier` 직렬화 | `{ Stat, Op, Value }` | SO 또는 코드에서 생성. `ShipStats.AddModifier(m)`로 등록. |

#### 계산식

```
GetEffective(stat, baseValue) = (baseValue + Σ Modifier.Value where Op==Add) × (1 + Σ Modifier.Value where Op==PercentAdd)
```

- **합산 보너스 우선, 퍼센트는 마지막에 곱셈** — 뱀파이어 서바이버즈 풍 누적 가산식.
- 등록된 모든 모디파이어가 영구 누적 (현재 제거 API 없음).
- 컴포넌트는 base value를 가지고 `_stats.GetEffective(...)`로 실제 적용값을 매번 조회 (예: [CannonBase.cs:30](../Assets/Scripts/Components/MainSlot/Cannon/CannonBase.cs#L30) `Effective(...)`).

#### 근거 / 상태

- **근거**: §0.3 "빌드 다양성" — 카드 픽업으로 스탯이 누적되는 뱀서식 빌드 트랙의 토대. 가산+곱셈 분리로
  희귀 카드(PercentAdd) vs 일반 카드(Add)의 체감 차이를 만드는 여지를 남김.
- **상태**: 시스템 자체 `구현 완성`. 풀에 들어갈 구체 모디파이어 값들은 §3.10 참조.
- **🚧 미구현 / 추후**:
  - **모디파이어 제거 / 만료**: 일시 버프/디버프 도입 시 필요. v1.0 스코프 외 가능성.
  - **PercentAdd 누적 상한**: 무제한 누적 시 후반 OP 위험. 카드 풀 제약 또는 상한 캡 검토.

### 3.10 강화 카드 풀 (`UpgradePool` / `UpgradeDefinition` 계열)

소스: [Assets/Scripts/Data/Upgrade/](../Assets/Scripts/Data/Upgrade/),
UI [Assets/Scripts/UI/UpgradeCard/UpgradeUI.cs](../Assets/Scripts/UI/UpgradeCard/UpgradeUI.cs).
**GDD §5.4 3택 강화창** 매핑.

#### 동작 흐름

1. `PlayerXP.OnLevelUp` → `UpgradeUI.HandleLevelUp`.
2. `UpgradePool.Pick(cardCount=3, ship, stats)` 호출.
   - 풀 내 `UpgradeDefinition`을 `IsAvailable(ship, stats)`로 필터.
   - 남은 후보를 셔플해 앞 `Pick` 개만큼 반환 (중복 없음).
3. `Time.timeScale = 0`, 카드 프리팹을 `_cardGroup` 아래 생성, 클릭 시 `def.Apply(ship, stats)` 후 `timeScale=1`.

#### 카드 종류 (`UpgradeDefinition` 서브클래스)

| 서브클래스 | `IsAvailable` | `GetDisplayLevel` | `Apply` |
|---|---|---|---|
| `MainEquipment` | `ship.CanInstall(prefab)` | `IsEmpty? 1 : level+1` | `ship.Install(prefab)` |
| `RearEquipment` | 동일 (`RearAttachable`) | 동일 | 동일 |
| `SubEquipment` | 동일 (`SubAttachable`) | 동일 | 동일 |
| `StatModifierUpgrade` | `true` (**무제한**) | `0` | 보유 모디파이어 배열을 `stats.AddModifier(m)` 순차 등록 |

#### 풀에 등록된 카드 SO (`UpgradePool.asset`)

| 파일 | 종류 | 효과 | 표시명 / 설명 |
|---|---|---|---|
| `Modifier/Damage+1.asset` | StatModifier | `Damage` `Add` `+1` | "Damage" / "Damage + 1" |
| `Modifier/FireRate+30.asset` | StatModifier | `FireRate` `PercentAdd` `+0.3` (= +30%) | "Fire Rate" / "Fire Rate + 0.3" (⚠️ 표시 텍스트는 +30% 의미로 정정 권장) |
| `Modifier/Range+5.asset` | StatModifier | `Range` `Add` `+5` | "Range" / "Range + 5" |

#### 작성됐으나 풀 미연결 SO

| 파일 | 종류 | 비고 |
|---|---|---|
| `Attachable/Cannon.asset` | MainEquipment | Lv1 캐넌 신규 장착 카드. |
| `Attachable/MainDoubleWrapper.asset` | MainEquipment | DoubleCannon 데코레이터 래핑 카드. |
| `Attachable/MainTripleWrapper.asset` | MainEquipment | TripleCannon 데코레이터 래핑 카드. |

- **근거**: §0.3 "다층 루프의 5분 의사결정" — 레벨업 3택은 뱀서식 빌드 의사결정의 핵심.
  현재 풀은 스탯 모디파이어 3종만 활성화 — 의식적으로 컴포넌트 카드를 풀 미연결 상태로 두어
  **W1 부하/스탯 모디파이어 단독 밸런싱**을 먼저 검증한 뒤 컴포넌트 카드를 풀에 추가할 예정.
- **상태**: 시스템 `구현 완성`, 카드 풀 구성·값 `구현 기본값`.
- **🚧 미구현 / 추후**:
  - **스탯 모디파이어 누적 상한** (GDD §5.4): `IsAvailable=true`로 무제한 스택 — 카드 코멘트로 "일정 개수? false로 밸런싱" 플래그됨.
  - **풀에 컴포넌트 카드 연결**: Cannon/Double/Triple SO를 `UpgradePool.asset` `_definitions` 배열에 추가하면 즉시 등장 가능.
  - **드롭 접촉 시 일시정지 + 교체 UI** (GDD §6.1): 현재는 레벨업 시점에만 카드 등장. 드롭 접촉 분기 미구현.

### 3.11 보스 — Pirate Lord (`PirateLordData` + 4종 Pattern Config + 3종 Phase Movement)

소스: [Assets/Scripts/Data/Boss/](../Assets/Scripts/Data/Boss/),
사양 상세는 [Docs/Boss/PirateLord.md](Boss/PirateLord.md). GDD §5.6 매핑.
v1.0 유일 보스로 1 스테이지 클리어 게이트.

#### 3.11.1 PirateLordData (HP·페이즈)

에셋 [PirateLordData.asset](../Assets/ScriptableObjects/Boss/PirateLord/PirateLordData.asset).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Health` | 1000 | HP | 플레이어 `MaxSpeed 10`(§3.1) · `Damage 50`(§3.6) 기준 ≈20발 처치 — P1+P2 합쳐 약 3~4분 |
| `InvincibleTime` | 0 | s | P3 자연 감소(`OnDamaged` 매 프레임 호출)를 i-frame 가드가 막지 않도록 0 |
| `Phase1ToPhase2HpThreshold` | 0.5 | 0–1 | HP 50% 도달 시 BT `HpThresholdWatchAction`이 P1 → P2 신호 |
| `Phase3DecayPerSecond` | 25 | HP/s | P3 진입 후 HP 자연 감소율 — 1000HP / 25 = 40초 페이즈 (도망 승리 체감 시간) |

- **근거**: §0.2 "1런 10분"에서 보스전 비중을 약 4~5분으로 잡아 30초/5분 루프(§0.3)와 정합. P3 40초는
  Vampire Survivors의 "Reaper" 추격 클라이맥스 길이와 비슷한 체감 — 도망 압박이 지루하지 않으면서도
  공포 효과(`HorrorFXController`) 적용 사이클이 충분히 반복될 시간.
- **상태**: `구현 기본값`.

#### 3.11.2 PhaseMovements (P1/P2/P3 — `ShipMovementData[3]`)

에셋 [P1MovementData.asset](../Assets/ScriptableObjects/Movement/PirateLord/P1MovementData.asset) /
[P2MovementData.asset](../Assets/ScriptableObjects/Movement/PirateLord/P2MovementData.asset) /
[P3MovementData.asset](../Assets/ScriptableObjects/Movement/PirateLord/P3MovementData.asset).

| 페이즈 | MaxSpeed | Acceleration | BrakeStrength | TurnSpeed | LateralGrip |
|---|---|---|---|---|---|
| P1 | 8 m/s | 2 | 2 | 10 deg/s | 0.5 |
| P2 | 9 m/s | 3 | 3 | 12 deg/s | 0.5 |
| P3 | 14 m/s | 4 | 4 | 30 deg/s | 0.5 |

- **근거**: 플레이어 `MaxSpeed 10`(§3.1) 기준 — P1(80%) 도주 여지, P2(90%) 압박 강화, P3(140%) 캐치업
  추격으로 "도망 불가능에 가깝지만 거리 유지는 가능"한 GDD §5.6 leash-less 사양 충족. TurnSpeed가
  플레이어(60 deg/s)보다 낮은 건 보스가 큰 함선이라는 무게감 표현 + P3에서 30으로 올려 추격 강도 증가.
- **상태**: `구현 기본값`.

#### 3.11.3 Radial Sweep (`RadialSweepConfig`) — P1, P2

에셋 [RadialSweepConfig (PirateLord).asset](../Assets/ScriptableObjects/Boss/Patterns/RadialSweepConfig%20(PirateLord).asset).
구현 [RadialSweepAction.cs](../Assets/Scripts/Enemies/Boss/Actions/RadialSweepAction.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `ProjectileCount` | 12 | 발 | 360° / 12 = 30° 간격 |
| `ShotInterval` | 0.3 | s | 한 발씩 0.3s 간격 발사 |
| `Clockwise` | true | — | 시계방향 회전 발사 |
| `Cooldown` | 7 | s | 사이클 간 대기 |
| `Range` | 20 | m | 보스 본체 반경(5m) + 사거리 — 화면 절반 가량 도달 |
| `Damage` | 20 | HP | 플레이어 100HP 기준 한 발 20% (5발이면 즉사) |
| `ArcHeight` | 8 | m | CannonBall 베지어 호 높이 |
| `FlightDuration` | 1.2 | s | 발사~폭발 비행 시간 |
| `AreaRadius` | 4 | m | 폭발 광역 반경 |
| `TelegraphDuration` | 0 | s | (미사용) — Radial Sweep은 회전 자체가 텔레그래프 |
| `TargetLayerMask` | Player | — | 폭발 적용 대상 레이어 |

- **근거**: §0.2 "궤적 예측" — 12발이 회전 방향으로 순차 발사되면 플레이어는 회전 역방향으로 무빙하면 회피
  가능. 3.6초 휘두름 + 7초 쿨다운으로 GDD §5.6 보스 사양의 "압박 의도(제자리 금지)" 충족하면서도 P1+P2
  합쳐 약 30사이클 발생 → 다층 패턴 학습 시간 확보.
- **상태**: `구현 기본값`.

#### 3.11.4 Mortar Rain (`MortarRainConfig`) — P1, P2

에셋 [MortarRainConfig (PirateLord).asset](../Assets/ScriptableObjects/Boss/Patterns/MortarRainConfig%20(PirateLord).asset).
구현 [MortarRainAction.cs](../Assets/Scripts/Enemies/Boss/Actions/MortarRainAction.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `ShellCount` | 3 | 발 | 한 사이클 동시 낙하 셸 수 |
| `Cooldown` | 8 | s | 사이클 간 대기 |
| `TelegraphDuration` | 1.5 | s | 원형 텔레그래프 표시 시간 — 회피 윈도우 |
| `AreaRadius` | 8 | m | 플레이어 주변 셸 위치 산포 반경 |
| `ScatterRadius` | 3 | m | 개별 폭발 광역 반경 |
| `Damage` | 18 | HP | 3발이라 약간 낮춤. 직격 시 18%, 중복 적중 시 큰 타격 |
| `ArcHeight` | 20 | m | 위에서 떨어지는 시작 높이 |
| `FlightDuration` | 0.6 | s | 텔레그래프 후 낙하 시간 — 짧게(예측 시간은 텔레그래프가 담당) |
| `TargetLayerMask` | Player | — | 폭발 적용 대상 레이어 |

- **근거**: §0.2 "정지 금지, 안전지대 지속 갱신" — `AreaRadius 8m`는 플레이어 위치 중심 셸 산포라 정지하면
  3발 중 1발이 직격할 확률 높음. `TelegraphDuration 1.5s` + `FlightDuration 0.6s`로 회피 윈도우는 충분히
  주되 순간 반응이 아닌 사전 회피 의사결정 유도. Radial Sweep(쿨다운 7s)과 엇갈리는 Cooldown 8s로
  두 패턴이 P1/P2 동안 교차 사이클 형성.
- **상태**: `구현 기본값`.

#### 3.11.5 Proximity Channel (`ProximityChannelConfig`) — P3

에셋 [ProximityChannelConfig (PirateLord).asset](../Assets/ScriptableObjects/Boss/Patterns/ProximityChannelConfig%20(PirateLord).asset).
구현 [ProximityChannelAction.cs](../Assets/Scripts/Enemies/Boss/Actions/ProximityChannelAction.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `ZoneRadius` | 12 | m | 보스 본체 반경(5m) + 안전 거리(7m). 플레이어가 이 반경 안에 있으면 데미지 |
| `DpsTickInterval` | 0.5 | s | 데미지 틱 간격 |
| `DpsPerTick` | 8 | HP | 0.5s × 8 = DPS 16. 안에 계속 머물면 ≈6초 사망 |
| `TargetLayerMask` | Player | — | 데미지 적용 대상 레이어 |

- **근거**: GDD §5.6 P3 "도망만 쳐도 승리" 사양 충족. 보스 P3 MaxSpeed 14 vs 플레이어 10 → 거리 좁혀지는
  속도는 4m/s. ZoneRadius 12m라 ≈3초 안에 따라잡힘 — 그 동안 플레이어는 방향 전환으로 거리 유지 가능.
  DPS 16은 안에 계속 머물면 즉사하는 수준이라 §0.2 "궤적 예측"의 위협 회피 학습 강제.
- **상태**: `구현 기본값`.

#### 3.11.6 Horror FX (`HorrorFXConfig`) — P3

에셋 [HorrorFXConfig (PirateLord).asset](../Assets/ScriptableObjects/Boss/Patterns/HorrorFXConfig%20(PirateLord).asset).
구현 [HorrorFXAction.cs](../Assets/Scripts/Enemies/Boss/Actions/HorrorFXAction.cs) +
[HorrorFXController.cs](../Assets/Scripts/Enemies/Boss/Patterns/HorrorFXController.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `VolumeWeightLerpSec` | 2 | s | URP Volume weight 0→1 페이드인 시간 |
| `PerlinAmplitude` | 0.8 | — | Cinemachine Basic Multi Channel Perlin 진폭 |
| `PerlinFrequency` | 1 | — | Cinemachine Perlin 빈도 |
| `PlayerSlowPercent` | 0.25 | 0–1 | `PercentAdd(-0.25)` — MoveSpeed 25% 감속 |
| `PlayerSlowDuration` | 1.5 | s | 각 슬로우 적용 지속 시간 |
| `PlayerSlowInterval` | 3 | s | 슬로우 적용 주기 |

- **근거**: P3 "유령선 클라이맥스" 분위기 — Volume 페이드 2s로 점진적 압박, Perlin 진폭 0.8은 시야 흔들림은
  주되 어지러움 한계 아래. 슬로우는 3s마다 1.5s씩 적용(=절반 시간 슬로우 적용) → 캐치업 거리 유지 압박 강화.
  플레이어는 슬로우 적용 사이클을 읽어 무빙 패턴을 조정.
- **상태**: `구현 기본값`.
- **🚧 wiring 의존**: URP Volume 프로필(Lens Distortion/Chromatic Aberration/Vignette 등) + Cinemachine
  Basic Multi Channel Perlin + Player ShipStats 인스펙터 wiring 필요. 미연결 시 시각 효과만 누락되고
  나머지(슬로우)는 정상 동작.

#### 3.11.7 보스 등장 트리거 (`StageData.BossSpawnAfterSec`)

소스 [StageData.cs](../Assets/Scripts/Data/Stage/StageData.cs) + [StageManager.cs](../Assets/Scripts/Core/Stage/StageManager.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `BossSpawnAfterSec` | (스테이지별, SO 인스펙터) | s | 스테이지 시작 후 보스 활성화까지 경과 시간 |
| `_bossSpawnOffset` | (씬 인스펙터) | m | 플레이어 기준 월드 offset — 카메라 쿼터뷰 고정이라 항상 화면 위쪽에서 등장하도록 설정 |
| 보스 회전 | `LookRotation(player - spawnPos)` | — | 스폰 시 보스 forward가 플레이어를 향함 |

- **근거**: GDD §5.6 본래 사양 `(경과시간 ≥ X분) AND (네임드 처치 수 ≥ Y)`에서 네임드 시스템 미구현으로
  시간 단일 조건만 사용. v1.0 1런 10분 중 보스전 4~5분(§3.11.1)이 적정 비중이라 `BossSpawnAfterSec`은
  300~360s 범위에서 플레이테스트 튜닝 예정.
- **상태**: `구현 기본값` (네임드 시스템 미구현, 시간 단일 트리거).
- **🚧 미구현 / 추후**:
  - **네임드 처치 수 조건**: 네임드 시스템 도입 시 `StageData`에 `NamedKillsRequired` 필드 추가 예정.