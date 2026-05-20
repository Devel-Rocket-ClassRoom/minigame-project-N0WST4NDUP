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
| 자동 사격 (타입별 발사 규칙·사거리) | §5.2 | W1 | 미확정(❓) |
| 적 스폰·일반 몹 스탯 (HP/데미지/EXP/등장률) | §5.3.1 | 플레이테스트 | **부분 등재 (§3.2~3.5)** — 일반 몹 3종 `구현 기본값`. EXP·등장률·스폰 곡선은 여전히 `미확정(❓)` |
| **부하 테스트 임계** (동시 적/투사체/드롭/VFX @60fps) | §5.3.2 | W1 (P0) | 미확정(❓) — W1 종료 전 도출 |
| EXP / 레벨업 곡선 | §5.4 | W1 | 미확정(❓) |
| 네임드 (드롭 타이머·흡수 우선순위·leash 반경) | §5.5 | W1 / 플레이테스트 | 미확정(❓) |
| 보스 (트리거 X분/Y킬·캐치업 속도·패턴 단계) | §5.6 | 플레이테스트 / W2 | 미확정(❓) |
| 컴포넌트 슬롯 (카테고리·개수·Lv 상한·레벨업 효과) | §6.1 | W1 / W2 | 미확정(❓) |
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
| `CombatData.Range` | 9 | m | Chase→Attack 전이 거리. **`_detectRange`(10)보다 작아야 함** |
| `CombatData.Cooldown` | 1 | s | Attack 재발사 쿨다운 |
| `CombatData.IsAreaAttack` | false | — | 광역 공격 여부 |
| `CombatData.AreaRadius` | 1 | m | 광역 공격 반경 (현재 사용 안 함) |

- **근거**: GDD §5.3.1 #2 "사거리 유지가 회피의 핵심, 거리 의사결정 유도".
  §0.2 "궤적 예측" — Range 9m와 MoveSpeed 6m/s 조합으로 플레이어가 "다가오는 사거리"를 예측하고
  피할 수 있는 출발점. 패트롤은 GDD 표 본문에 없는 코드 측 추가로, "정찰 중인 적" 느낌을 주어
  스폰 직후 어색함을 줄이는 보조 행동.
- **상태**: `구현 기본값`.
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