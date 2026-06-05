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

v0.3.0 기준 §3에 다수 영역이 `구현 기본값`(다수는 인스펙터 튜닝값)으로 등재돼 있다. 아직 플레이테스트로
검증되지 않았거나 미등재인 항목만 아래 표·본문에서 `미확정(❓)`로 표기한다.

### 2.1 영역별 등재 현황

| 영역 | GDD 참조 | 확정 마일스톤 | 현재 상태 |
|---|---|---|---|
| 자동 사격 (주포 발사 규칙·사거리) | §5.2 | W1 | **등재 (§3.6, §3.12)** — Main 캐넌·머신건 2종 + 데코레이터. 데미지/타게팅 일부 `미확정(❓)` |
| 부포·후방 패시브 (AutoRepair/Propeller/Rudder) | §5.2 | — | **신규 등재 (§3.13)** — 레벨식 `구현 기본값` |
| 후방 기뢰 (MineDropper) | §5.2 | — | **신규 등재 (§3.14)** |
| 적 스폰·일반 몹 스탯 (HP/데미지/이동/감지) | §5.3.1 | 플레이테스트 | **등재 (§3.2~3.5, §3.16)** — 일반 몹 3종(잠수함 기뢰 포함) + 스포너 씬값 `구현 기본값`. EXP·등장률 일부 `미확정(❓)` |
| **부하 테스트 임계** (동시 적/투사체/드롭/VFX @60fps) | §5.3.2 | W1 (P0) | 미확정(❓) — CommonSpawner `_maxAlive 200`(§3.16)이 검증 대상 |
| EXP / 레벨업 곡선 | §5.4 | W1 | **등재 (§3.7)** — `PlayerXP` 선형 곡선 `구현 기본값`. 곡선 형태 재검토 필요 |
| EXP 드롭·자석·젬 이동 | §5.4 | W1 | **등재 (§3.8)** — 자석 반경 10m(인스펙터), 젬 10m/s `구현 기본값` |
| ShipStats / Modifier 시스템 | §5.4 / §6.1 | — | **등재 (§3.9)** — 시스템 `구현 완성` |
| 강화 카드 풀 (3택 레벨업) | §5.4 | W1~W2 | **등재 (§3.10)** — 스탯 8종 + 컴포넌트 8종 **전부 풀 연결**. 누적 상한 `미확정(❓)` |
| 네임드 + 드롭 경쟁 (타이머·흡수·leash) | §5.5 | 플레이테스트 | **신규 등재 (§3.15)** — 구현 완료. 임시 AI·종 1종·흡수 의지는 v1.0/`미확정(❓)` |
| 미니맵 레이더 | §6.2 | W1 | **신규 등재 (§3.17)** — 프로토타입 `구현 기본값` |
| 보스 (트리거·캐치업·패턴) | §5.6 | 플레이테스트 / W2 | **등재 (§3.11)** — Pirate Lord 1체 HP·페이즈·decay·패턴 `구현 기본값`(인스펙터 재확인 반영). P2 콤보(Whirlpool/Ramming) **주말 후속**, 트리거 시간 단일(네임드 처치 수 미통합) |
| 컴포넌트 슬롯 (카테고리·Lv 상한) | §6.1 | W1 / W2 | **등재 (§3.6, §3.12~3.14)** — Main/Sub/Rear 각 1슬롯 + 어태처블 레벨식(Lv 상한 3) |
| 피해 / 사망 (i-frame 무적시간) | §5.7 | W1 | **부분** — 적별 `InvincibleTime`(§3.3~3.5) 등재. 플레이어 i-frame 값 `미확정(❓)` |

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

소스: [Assets/Scenes/InGame.unity](../Assets/Scenes/InGame.unity) `InPlay Camera` 게임오브젝트
(Cinemachine 컴포넌트 직배치). Main Camera에는 `CinemachineBrain` 부착. (v0.2.0의 `MovementTest.unity`에서 이전.)

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

소스: `ShipMovementData`,
에셋 [DefaultShip_Movement.asset](../Assets/ScriptableObjects/Movement/DefaultShip/DefaultShip_Movement.asset)
(플레이어·네임드 공용).

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
`[SerializeField]` 필드라 적 프리팹마다 인스펙터에서 오버라이드된다. **실제 적용값은 프리팹 인스펙터 값**(아래 표)이며,
클래스 기본값(`_detectRange 10 / _detectInterval 2.5 / _moveSpeed 6`)은 출발점일 뿐 실제와 다르다.

| 적 (프리팹) | `_moveSpeed` | `_detectRange` | `_detectInterval` | 비고 |
|---|---|---|---|---|
| 나룻배 `SailBoat.prefab` | 3 | 20 | 3 | — |
| 소형 함선 `GunBoat.prefab` | 3 | 17 | 2 | + `_idlingInterval 3`, `_patrolPointRadius 20`, 패트롤 포인트 8개(`k_patrolPointRadius` const 배열) |
| 잠수함 `Submarine.prefab` | 2 | 13 | 2.5 | 잠수 중 `_submergedSpeedMult 1.7`배 가속 |

(`k_DetectBufferSize 8` = `OverlapSphereNonAlloc` 결과 버퍼, `const`, 풀링 시에도 GC-free.)

- **근거**: §0.2 "1런 10분"과 §0.3 "다층 루프의 30초 리듬" 출발점. 세 적 모두 플레이어 `MaxSpeed 10`(§3.1)보다
  느린 2~3 m/s라 도주 여지를 남기되, 감지 반경(13~20m)을 이동속도보다 크게 잡아 "시야 안에서 다가오는 위협"을
  만든다. 잠수함만 가장 느리되(2) 잠수 시 1.7배 가속해 출수 타이밍 압박을 준다. 감지 간격 2~3s는 매 프레임
  `Physics.OverlapSphere` 호출을 피해 §5.3.2 부하 부담을 줄인다.
- **상태**: `구현 기본값` — 인스펙터 튜닝 반영값. W1 부하/플레이테스트 후 `확정` 승격 예정.

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
[GunBoatData.asset](../Assets/ScriptableObjects/Combat/GunBoatData.asset) (전투 `CombatData`).
**GDD §5.3.1 #2** 매핑. FSM 4상태: `Idle → Patrol → Chase → Attack`. 이동·감지값은 §3.2 표.

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Health` (`GunBoat_Data`) | 15 | HP | 나룻배보다 약간 단단 |
| `InvincibleTime` | 1 | s | 피격 i-frame |
| `_idlingInterval` | 3 | s | Idle 대기 시간 — 끝나면 Patrol로 |
| 패트롤 포인트 수 | 8 | 개 | `k_patrolPointRadius` const 배열 크기 |
| `_patrolPointRadius` (프리팹) | 20 | m | 패트롤 포인트 분포 반경 (insideUnitCircle) |
| `GunBoatData.Damage` | 7 | HP | 즉발 데미지 (`OnFire`에서 `IDamageable.OnDamaged`) |
| `GunBoatData.MinRange` | 1 | m | (GunBoat 미사용) |
| `GunBoatData.MaxRange` | 7 | m | Chase→Attack 전이 거리. `_detectRange`(17) 이하 |
| `GunBoatData.Cooldown` | 3 | s | Attack 재발사 쿨다운 |
| `GunBoatData.IsAreaAttack` | false | — | 광역 공격 여부 |
| `GunBoatData.AreaRadius` | 1 | m | 광역 공격 반경 (사용 안 함) |

- **근거**: GDD §5.3.1 #2 "사거리 유지가 회피의 핵심, 거리 의사결정 유도".
  §0.2 "궤적 예측" — MaxRange 7m와 MoveSpeed 3m/s(§3.2) 조합으로 플레이어가 "다가오는 사거리"를 예측하고
  피할 수 있다. Cooldown 3s는 즉발 데미지 7이 연사로 누적돼 과해지지 않게 텀을 둔 값. 패트롤은 GDD 표 본문에
  없는 코드 측 추가로, "정찰 중인 적" 느낌을 주어 스폰 직후 어색함을 줄이는 보조 행동.
- **상태**: `구현 기본값` (인스펙터 튜닝 반영).
- **✅ 마이그레이션 완료**: `CombatData`는 구 단일 `Range` 필드를 `MinRange`/`MaxRange`로 분리 완료 —
  클래스·전체 에셋에 구 `Range` 잔재 없음. (이전 빌드의 `Range 9` 잔재 문제 해소.)
- **✅ GDD 정렬**: 일반 몹은 Rigidbody 미사용·`transform` 직접 조작이라 물리 기반 자연 감속이 없다.
  GDD §5.3.1 #2 본문도 "추격 → 사거리 진입 시 **정지** → 공격"으로 정렬됨 (CLAUDE.md §3 SSoT).

### 3.5 일반 몹 — 잠수함 (`Submarine` / `Submarine_Data` / `Mine`)

소스: [Assets/Scripts/Enemies/Common/Submarine/](../Assets/Scripts/Enemies/Common/Submarine/) 일괄,
[Mine.cs](../Assets/Scripts/Combat/Mine/Mine.cs),
에셋 [Submarine_Data.asset](../Assets/ScriptableObjects/Ship/Common/Submarine_Data.asset).
**GDD §5.3.1 #3** 매핑. FSM 5상태: `Idle → Diving → SubmergedFlee → Surfacing → SurfacedFlee`. 이동·감지값은 §3.2.

| 수치 | 값 (출처) | 단위 | 설명 |
|---|---|---|---|
| `Health` (`Submarine_Data`) | 50 | HP | 일반 몹 중 가장 단단 — 잠수 무적 윈도우 보정 |
| `InvincibleTime` | 1 | s | 피격 i-frame (수면 노출 시) |
| `_submergedSpeedMult` (프리팹) | 1.7 | × | 잠수 중 이동 속도 배율 (코드 기본 1.3 → 인스펙터 1.7) |
| `_surfacedHoldDuration` | 2 | s | 출수 후 수면 체류(공격 윈도우) 시간 |
| `_submergedHoldDuration` | 3 | s | 잠수 도주 지속 시간 |
| `_transitionDuration` | 1 | s | 잠수/부상 보간 시간 (smoothstep) |
| `_divingOffset` | (0, −1, 0) | m | 잠수 시 자식 모델 하강 오프셋 |

**기뢰(`Mine`) — 출수 시 살포** (`SubmarineSurfacedFleeState.OnEnter` → `Submarine.LayMine()`, 출수마다 1발):

| 수치 (프리팹) | 값 | 단위 | 설명 |
|---|---|---|---|
| `_mineDamage` | 10 | HP | 폭발 시 반경 내 모든 ShipBody에 적용 (스플래시) |
| `_mineRadius` | 1 | m | 무장 후 `CheckSphere` 접촉 감지 + 폭발 반경 (코드 기본 2 → 인스펙터 1) |
| `_mineLifetime` | 10 | s | 미접촉 시 자연 소멸(`WaterSplash`) (코드 기본 20 → 인스펙터 10) |
| `_mineArmDelay` | 1 | s | 설치 후 무장까지 지연 (이 시간 전엔 폭발 안 함) |

- **근거**: GDD §5.3.1 #3 "잠수 → 이동 → 출수 → 공격 → 재잠수" 사이클 충족. HP 50은 잠수 중 콜라이더
  비활성(무적)을 감안한 보정 — 출수 윈도우(`_surfacedHoldDuration 2s`)가 짧아 그 사이에 처치해야 한다.
  기뢰는 GDD §5.3 "정적 트랩 예외" — 풀링(`CombatPool`) + 수명 10s + 무장 지연 1s로 동시 수를 구조적으로
  제어해 60fps 방어(§5.3.2). `_mineRadius 1`·수명 10s로 인스펙터 튜닝되어 초기값(반경 2·수명 20)보다
  봉쇄 강도를 낮춤 — §0.2 "궤적 예측"이 가혹해지지 않게.
- **상태**: `구현 기본값` (인스펙터 튜닝 반영). 기뢰 공격 사이클 **구현 완료**.
- **🚧 추후**:
  - **잠수 중 시인성 보조**: GDD §5.3.1 #3 비고 + §11 — 그림자/물결/미니맵 마커 중 채택 필요. 현재 잠수 시 시각 단서 미흡.
  - **기뢰 아군/적 시각 구분**: 플레이어 `MineDropper` 기뢰(§3.14)와 색/아이콘 차별화 미적용 (GDD §11).

### 3.6 주포 — 캐넌 (`CannonBase` 계열 / `CannonData` / `CannonBall`)

소스: [Assets/Scripts/Components/MainSlot/Cannon/](../Assets/Scripts/Components/MainSlot/Cannon/),
[Assets/Scripts/Combat/CannonBall/CannonBall.cs](../Assets/Scripts/Combat/CannonBall/CannonBall.cs),
에셋 [CannonData.asset](../Assets/ScriptableObjects/Combat/CannonData.asset).
**GDD §5.2 자동 사격 / §6.1 슬롯 & 컴포넌트** 매핑 — 주포 슬롯 1차 구현.

#### CannonData (SO) — `CannonData.asset`

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Damage` | 50 | HP | `CannonBall` 폭발 시 `AreaRadius` 내 타겟 레이어에 적용 |
| `MinRange` | 1 | m | 랜덤 발사 거리 하한 |
| `MaxRange` | 10 | m | 랜덤 발사 거리 상한 (`Effective(Range, MaxRange)`로 스탯 보정) |
| `Cooldown` | 2 | s | 발사 간격 — `_cooldownTimer = Cooldown / Effective(FireRate)` (**하드코딩 없음**) |
| `IsAreaAttack` | true | — | 광역 공격 여부 |
| `AreaRadius` | 1.6 | m | 광역 폭발 반경 |

#### 캐넌 본체 — 발사 파라미터 (`CannonAttachable`)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `_arcHeight` | 5 | m | 베지어 포물선 호 높이 (SerializeField) |
| `_flightDuration` | 0.7 | s | 발사~착탄 비행 시간 (SerializeField) |
| 발사 위치 | yaw 랜덤 360° × `Random(MinRange, MaxRange)` | — | 거리·방향 모두 랜덤 (타게팅 무지향) |
| `BallScale` (레벨별) | Lv1 1.0 / Lv2 1.2 / Lv3 1.4 | × | **시각 크기만** — 전투 수치 무관 |

#### 업그레이드 사슬 (클래스 체인 + 데코레이터)

| 단계 | 클래스 | 역할 |
|---|---|---|
| Lv1 | `Lv1_Cannon` | 단일 발사 (BallScale 1.0, CanUpgrade) |
| Lv2 | `Lv2_Cannon` | 단일 발사 (BallScale 1.2, CanUpgrade) |
| Lv3 | `Lv3_Cannon` | 단일 발사 (BallScale 1.4, **CanUpgrade=false** 최종) |
| ×2 | `DoubleCannon` | 데코레이터 — 내부 캐넌의 `FireProcess`를 1 Tick에 2회 실행 |
| ×3 | `TripleCannon` | 데코레이터 — 1 Tick에 3회 실행 |

**데코레이터 cooldown 위임 규약 (중요)**: `DoubleCannon`/`TripleCannon`은 `CannonBase`를 상속하지만
자기 자신의 `_cooldownTimer`를 **절대 세팅하지 않는다**. 대신 `CanFire`와 `TickCooldown`을 `override`해
inner `_cannon`으로 위임 — 가장 안쪽 leaf cannon(Lv1/Lv2)의 타이머 하나로 전체 체인이 발사 주기를 결정한다.
이로써 `TripleCannon(DoubleCannon(Lv1))` 스택 시 **1 cooldown 주기에 2×3=6발이 동시 발사**된다.
초기 구현에서는 wrapper의 자체 `_cooldownTimer`가 0으로 방치돼 항상 `CanFire=true`가 되어 무한 발사
버그가 있었으며, [CannonBase.cs:9, :60](../Assets/Scripts/Components/MainSlot/Cannon/CannonBase.cs)의
`virtual` 승격 + 두 데코레이터의 위임 override로 수정됨.

업그레이드 트리거: **레벨업 카드** (`UpgradeUI` → `UpgradePool.Pick`). `UpgradePool.asset`에 캐넌 계열
SO(`Cannon` / `MainDoubleWrapper` / `MainTripleWrapper`)가 **모두 연결됨**(§3.10) — 카드로 등장.
테스트용 키 입력은 제거되어 카드 시스템으로 일원화.

#### CannonBall (포탄, `CombatItemBase` 풀링 대상)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| 궤적 | 2차 베지어 (`ArcHeight` 정점, `_duration = max(0.01, FlightDuration)`) | — | `_p0/_p1/_p2` 보간 |
| 착탄 | 베지어 t≥1 → `WaterSplash` / 타겟 레이어 트리거 진입 → `Explosion` | — | 둘 다 `AreaRadius` 스플래시 데미지 |
| `k_hitBufferSize` | 32 | — | `OverlapSphere` 충돌 버퍼 (`const`) |

- **근거**: GDD §5.2 "슬롯 컴포넌트가 각자 쿨다운/타게팅 규칙으로 자동 발사" 충족의 첫 슬롯 구현.
  §0.3 "자동 사격(Vampire Survivors)" 결합 출발점. Damage 50은 GunBoat HP 15(§3.4)·나룻배 HP 10(§3.3)
  기준 1~2발 처치 — §0.3 30초 루프 즉시 체감. Cooldown 2s는 더블/트리플 데코레이터로 체감 DPS를 키우는
  여지를 남긴 출발점. 베지어 착탄 시 `AreaRadius 1.6` 스플래시로 데미지가 적용되며, 무지향 랜덤 살포라
  "면으로 맞히는" 주포 설계.
- **상태**: `구현 기본값` — 데미지·광역·착탄 구현 완료. W1 부하/플레이테스트로 타게팅 방식·수치 재평가.
- **🚧 추후**:
  - **타게팅**: 현재 무지향(yaw 랜덤). GDD §5.2 "주포=전방" 규약은 미적용 — 랜덤 살포 유지 여부 플레이테스트 판단.
  - **데미지/광역 적용**: 구현됨(`Explosion` → `AreaRadius` 스플래시). 초기 빌드의 미적용 이슈 해소.

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
| `XPMagnet._radius` (플레이어/네임드 프리팹) | 10 | m | 자석 흡인 반경 (코드 기본 5 → 인스펙터 10). `SphereCollider isTrigger=true` 진입 감지 → `OnPick(target)`. |
| `XPGem._moveSpeed` | 10 | m/s | 흡인 시작 후 타겟을 향한 직선 이동 속도. |
| 흡인 동작 | 직선 추적 (관성·곡선 없음) | — | `dir = (target - pos).normalized; pos += dir × speed × dt` |
| 흡수 조건 | 트리거 충돌 + `gameObject.layer == _targetLayer` | — | 자석 진입 시 캐싱한 레이어와 일치해야 `PlayerXP.AddXp` 호출 후 풀로 반환 |

- **근거**: §0.3 "30초 루프 즉시 체감" — 자석 반경 10m는 플레이어 시야(카메라 가시 영역 §3.0.1) 안에서
  처치 직후 흡인이 시작되는 거리. 젬 속도 10m/s는 플레이어 `MaxSpeed`(§3.1)와 동일해 정지 상태일 때
  ≈1초 안에 도달해 도파민 루프 유지, 이동 중에는 따라잡는 텐션도 발생.
  드롭당 1 XP는 §3.7 곡선과 직결 — Lv2 진입에 10마리 처치 필요.
- **상태**: `구현 기본값` (인스펙터 튜닝 반영) — W1 부하/플레이테스트로 자석 반경·젬 속도 재튜닝.
- **⚠️ 코드 품질 노트**: `XPMagenet.cs` 파일/클래스명에 typo (`Magenet` → 정확히는 `Magnet`).
  리네이밍은 별도 chore 이슈로 분리 권장.
- **🚧 추후**:
  - **드롭 타이머·소멸 연출**: EXP 젬은 시간 만료 없이 영구 상주 (네임드 컴포넌트 드롭 §3.15와 달리 EXP 젬은 타이머 불필요로 보임 — 디자인 확정 필요).
  - **EXP는 플레이어 전용**: 드롭 경쟁(GDD §2·§5.5)은 **컴포넌트 드롭**(§3.15)에만 적용 — EXP 젬은 네임드가 흡수하지 않는다.

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

#### 풀에 등록된 카드 SO (`UpgradePool.asset`) — **전부 연결됨**

**`_modifierDefinitions` (8종, `StatModifierUpgrade`)**

| 파일 | Stat | Op | Value | 표시명 |
|---|---|---|---|---|
| `Modifier/Damage+1` | Damage | Add | +1 | Damage + 1 |
| `Modifier/Damage+3` | Damage | Add | +3 | Damage + 3 |
| `Modifier/Damage+5` | Damage | Add | +5 | Damage + 5 |
| `Modifier/FireRate+10` | FireRate | PercentAdd | +0.10 | Fire Rate + 10% |
| `Modifier/FireRate+15` | FireRate | PercentAdd | +0.15 | Fire Rate + 15% |
| `Modifier/FireRate+20` | FireRate | PercentAdd | +0.20 | Fire Rate + 20% |
| `Modifier/Range+3` | Range | Add | +3 | Range + 3 |
| `Modifier/Range+5` | Range | Add | +5 | Range + 5 |

**`_attachmentDefinitions` (8종, 컴포넌트 장착/진화)**

| 파일 | 슬롯 / 종류 | 표시명 | 효과 |
|---|---|---|---|
| `Attachable/Cannon` | Main (MainEquipment) | Cannon | 캐넌 장착/레벨업 (§3.6) |
| `Attachable/MachineGun` | Main (MainEquipment) | Machine Gun | 머신건 장착/레벨업 (§3.12) |
| `Attachable/MainDoubleWrapper` | Main 데코레이터 | x2 | 주포 동시 발사/타격 ×2 |
| `Attachable/MainTripleWrapper` | Main 데코레이터 | x3 | 주포 동시 발사/타격 ×3 |
| `Attachable/AutoRepair` | Sub (SubEquipment) | AutoRepair | 주기 HP 회복 (§3.13) |
| `Attachable/Propeller` | Rear (RearEquipment) | Propeller | 이동속도 +% (§3.13) |
| `Attachable/Rudder` | Rear (RearEquipment) | Rudder | 선회속도 +% (§3.13) |
| `Attachable/MineDropper` | Rear (RearEquipment) | MineDropper | 후방 기뢰 살포 (§3.14) |

- **근거**: §0.3 "다층 루프의 5분 의사결정" — 레벨업 3택은 뱀서식 빌드 의사결정의 핵심. v0.2.0의 "스탯 3종만
  연결" 단계를 넘어 **스탯 8종 + 컴포넌트 8종 전부 풀에 연결**되어 빌드 선택지가 확장됨. (다만 결과 보고서상
  체감 다양성은 아직 부족 — 슬롯별 종수 추가는 v1.0 과제.)
- **상태**: 시스템 `구현 완성`, 카드 풀 구성 `구현 기본값`.
- **🚧 추후**:
  - **스탯 모디파이어 누적 상한** (GDD §5.4): `StatModifierUpgrade.IsAvailable=true`로 무제한 스택 — 카드 코멘트로 "일정 개수? false로 밸런싱" 플래그됨.
  - **드롭 접촉 교체 UI** (GDD §6.1): 네임드 드롭 픽업은 구현됨(`UpgradeUI.OpenComponentPickup`, §3.15). 슬롯 풀 시 현재 vs 신규 비교 UI 정교화는 추후.

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
| `Cooldown` | 4 | s | 사이클 간 대기 |
| `TelegraphDuration` | 2 | s | 원형 텔레그래프 표시 시간 — 회피 윈도우 |
| `AreaRadius` | 8 | m | 플레이어 주변 셸 위치 산포 반경 |
| `ScatterRadius` | 3 | m | 개별 폭발 광역 반경 |
| `Damage` | 18 | HP | 3발이라 약간 낮춤. 직격 시 18%, 중복 적중 시 큰 타격 |
| `ArcHeight` | 20 | m | 위에서 떨어지는 시작 높이 |
| `FlightDuration` | 0.6 | s | 텔레그래프 후 낙하 시간 — 짧게(예측 시간은 텔레그래프가 담당) |
| `TargetLayerMask` | Player (m_Bits 8 = Layer 3) | — | 폭발 적용 대상 레이어 |

- **근거**: §0.2 "정지 금지, 안전지대 지속 갱신" — `AreaRadius 8m`는 플레이어 위치 중심 셸 산포라 정지하면
  3발 중 1발이 직격할 확률 높음. `TelegraphDuration 2s` + `FlightDuration 0.6s`로 회피 윈도우는 충분히
  주되 순간 반응이 아닌 사전 회피 의사결정 유도. Cooldown 4s로 사이클이 자주 돌아 정지 페널티를 지속 부과.
- **상태**: `구현 기본값` (에셋 실제값 — Cooldown 4 / Telegraph 2).

#### 3.11.5 Proximity Channel (`ProximityChannelConfig`) — P3

에셋 [ProximityChannelConfig (PirateLord).asset](../Assets/ScriptableObjects/Boss/Patterns/ProximityChannelConfig%20(PirateLord).asset).
구현 [ProximityChannelAction.cs](../Assets/Scripts/Enemies/Boss/Actions/ProximityChannelAction.cs).

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `ZoneRadius` | 40 | m | 이 반경 안에 플레이어가 있으면 DoT. 본체 콜라이더와 분리 |
| `DpsTickInterval` | 0.7 | s | 데미지 틱 간격 |
| `DpsPerTick` | 4 | HP | 4 / 0.7s ≈ **DPS 5.7**. 플레이어 100HP 기준 계속 머물면 ≈17초 사망 |
| `TargetLayerMask` | Player (m_Bits 8 = Layer 3) | — | 데미지 적용 대상 레이어 |

- **근거 / ⚠️ 검증 필요**: GDD §5.6 P3 "도망만 쳐도 승리" 사양. 보스 P3 MaxSpeed 14 vs 플레이어 10 →
  접근 속도 4m/s. **ZoneRadius 40m는 카메라 가시(70m)의 절반 이상으로 매우 넓어** P3 내내 거리 유지를
  강제하는 광역 압박존으로 작동한다. 다만 DPS 5.7은 즉사가 아닌 누적 압박(≈17초) 수준이라, 원 설계 의도
  (좁은 12m 존 + 즉사급 DPS 16)와 체감이 다르다. "넓은 존 + 약한 DPS"가 의도인지 미스튜닝인지는
  **플레이테스트로 확정** — P3 자연감소 40초(§3.11.1) 동안 "닿으면 조금씩 깎이되 도망이 정답"인 균형이 핵심.
- **상태**: `구현 기본값` (에셋 실제값 — Zone 40 / Interval 0.7 / DpsPerTick 4). 원 설계와 차이 커 플레이테스트 우선 검증.

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
| `BossSpawnAfterSec` (`StageData.asset`) | 180 | s | 스테이지 시작 후 보스 활성화까지 경과 시간 |
| `_bossSpawnOffset` | (씬 인스펙터) | m | 플레이어 기준 월드 offset — 카메라 쿼터뷰 고정이라 항상 화면 위쪽에서 등장 |
| 보스 회전 | `LookRotation(player - spawnPos)` | — | 스폰 시 보스 forward가 플레이어를 향함 |

- **근거**: GDD §5.6 본래 사양 `(경과시간 ≥ X분) AND (네임드 처치 수 ≥ Y)` 중 **시간 조건만** 사용.
  네임드 시스템은 v0.3.0에 구현됐으나(§3.15) `StageData`에 `NamedKillsRequired`가 아직 없어 트리거 통합은
  미완. 현재 180s(3분)는 1런 10분 중 보스전 4~5분(§3.11.1) 비중에 맞춘 출발점 — 플레이테스트 튜닝 예정.
- **상태**: `구현 기본값` (시간 단일 트리거 — 네임드 처치 수 조건 미통합).
- **🚧 추후**:
  - **네임드 처치 수 조건**: `StageData.cs`에 TODO 주석만 존재(`NamedKillsRequired`). 필드 추가 + `StageManager` 조건 결합 필요 — v1.0 과제.

### 3.12 주포 — 머신건 (`MachineGunBase` 계열 / `MachineGunData`)

소스: [Assets/Scripts/Components/MainSlot/MachineGun/](../Assets/Scripts/Components/MainSlot/MachineGun/),
에셋 [MachineGunData.asset](../Assets/ScriptableObjects/Combat/MachineGunData.asset). **GDD §5.2 주포** 매핑 — 캐넌과 양자택일 주포.

#### MachineGunData (SO)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| `Damage` | 2.3 | HP | 발당 데미지 — `Effective(Damage, 2.3) × DamageMultiplier` |
| `MaxRange` | 10 | m | 타게팅 `OverlapSphere` 반경 (`MinRange` 미사용) |
| `Cooldown` | 1 | s | 발사 간격 — `Cooldown / Effective(FireRate)` (하드코딩 없음) |
| `IsAreaAttack` / `AreaRadius` | false / 1 | — | 단일 타겟 즉발(히트스캔), 광역 아님 |

#### 머신건 본체

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| 타게팅 | 최근접 정렬, 동일 ShipBody dedupe | — | 즉발(히트스캔), 투사체 없음 |
| `TargetsPerShot` (Lv1) | 1 | 명 | 발당 동시 타격 수 |
| `_turnSpeed` (터렛 조준) | 720 | deg/s | 연출용 터렛 회전 (히트스캔이라 명중과 무관) |
| `k_targetBufferSize` | 32 | — | `OverlapSphereNonAlloc` 버퍼 (`const`) |
| `DamageMultiplier` (레벨별) | Lv1 1.0 / Lv2 1.2 / Lv3 1.4 | × | Lv3 `CanUpgrade=false` 최종 |
| `DoubleMachineGun` / `TripleMachineGun` | TargetsPerShot ×2 / ×3 | — | 데코레이터 — 동시 타격 대상 수 곱셈 |

- **근거**: §0.3 "자동 사격" — 캐넌(랜덤 살포 광역)과 대비되는 **즉발 단일 타겟 정밀** 주포. Damage 2.3 ×
  Cooldown 1s ≈ DPS 2.3은 캐넌(50/2s=25)보다 훨씬 낮지만, 최근접 자동 조준 + 데코레이터로 다수 동시 타격
  (×2/×3)해 군집 처리에 강한 결을 만든다. §0.3 "빌드 다양성" — 두 주포가 다른 플레이 결을 제공.
- **상태**: `구현 기본값` — 데미지 2.3은 매우 낮아 플레이테스트 재조정 가능성 높음.

### 3.13 부포·후방 패시브 (`AutoRepair` / `Propeller` / `Rudder`)

소스: [SubSlot/AutoRepair/](../Assets/Scripts/Components/SubSlot/AutoRepair/),
[RearSlot/Propeller/](../Assets/Scripts/Components/RearSlot/Propeller/), [RearSlot/Rudder/](../Assets/Scripts/Components/RearSlot/Rudder/).
**GDD §5.2 부포(Sub)·후방(Rear)** 매핑. 수치는 코드 SerializeField(레벨식). 데이터 SO(`AutoRepairDummy`/`PropellerDummy`/`RudderDummy`)는 전 필드 0 — 마커/아이콘 용도이며 실제 수치는 어태처블 코드가 보유.

| 컴포넌트 | 슬롯 | 효과 | Lv1 | Lv2 | Lv3 | 비고 |
|---|---|---|---|---|---|---|
| `AutoRepair` | Sub | MaxHealth 비례 회복 (3s 주기) | +5% | +12% | +19% | `MaxHealth × (0.05 + 0.07×(lv−1))`, 풀피 아닐 때만 |
| `Propeller` | Rear | `MoveSpeed` PercentAdd | +12% | +19% | +26% | `0.12 + 0.07×(lv−1)` |
| `Rudder` | Rear | `TurnSpeed` PercentAdd | +20% | +25% | +30% | `0.20 + 0.05×(lv−1)` |

- **근거**: §0.3 "빌드 다양성" — 공격(주포) 외 **생존(AutoRepair)·기동(Propeller/Rudder)** 축을 추가해
  3택 카드 의사결정을 풍부하게. AutoRepair 회복은 MaxHealth 비례라 후반 스케일링에 자연 대응. 기동 강화는
  §0.2 "궤적 예측" 조작감을 직접 끌어올린다.
- **상태**: `구현 기본값` (`_maxLevel 3`).

### 3.14 후방 — 기뢰 투척기 (`MineDropperBase` 계열 / `MineDropperData`)

소스: [RearSlot/MineDropper/](../Assets/Scripts/Components/RearSlot/MineDropper/),
에셋 [MineDropperData.asset](../Assets/ScriptableObjects/Combat/MineDropperData.asset). 플레이어 후방 기뢰 — 잠수함 기뢰(§3.5)와 별개 시스템(동일 `Mine` 사용).

| 수치 | 값 (출처) | 단위 | 설명 |
|---|---|---|---|
| `MineDropperData.Damage` | 30 | HP | 폭발 반경 내 ShipBody에 적용 |
| `MineDropperData.Cooldown` | 10 | s | 설치 주기 — `Cooldown × CooldownMultiplier` |
| `MineDropperData.AreaRadius` | 2 | m | 폭발/접촉 반경 |
| `_mineLifetime` (`MineDropperAttachable`) | 30 | s | 미접촉 시 자연 소멸 |
| ArmDelay | 0 | s | 즉시 무장 (잠수함 기뢰는 1s) |
| `CooldownMultiplier` (레벨별) | Lv1 1.0 (10s) / Lv2 0.9 (9s) / Lv3 0.7 (7s) | — | 레벨업 시 설치 주기 단축 (데미지/반경/수명은 레벨 무관) |

- **근거**: §0.3 "빌드 다양성" — 후방에 깔아두는 지역 거부(area-denial) 무기로 추격 잡몹 견제. 플레이어
  기뢰는 데미지 30·반경 2로 잠수함 기뢰(10·1, §3.5)보다 강하게 차등 — 능동 설치 vs 적의 함정의 역할 구분.
  GDD §5.3 "정적 트랩 예외" — `CombatPool` + 수명 30s로 동시 수 제어.
- **상태**: `구현 기본값`.
- **🚧 추후**: 플레이어 기뢰 vs 잠수함 기뢰 **아군/적 시각 구분** 미적용 (GDD §11).

### 3.15 네임드 + 드롭 경쟁 (`Named` / `NamedWander` / `AttachableWrapper`) — 핵심 차별화

소스: [Enemies/Named/](../Assets/Scripts/Enemies/Named/), [Shared/ItemDrop/](../Assets/Scripts/Shared/ItemDrop/).
프리팹 `Assets/Prefabs/Enemies/Named/DefaultShip.prefab`. **GDD §5.5** 매핑 — v0.3.0 신규 구현.

#### 네임드 본체

| 수치 | 값 (출처) | 단위 | 설명 |
|---|---|---|---|
| `_shipData` | `DefaultShip_Data` (Health 100) | HP | 플레이어와 동일 함체 데이터 |
| `ShipMovement._data` | `DefaultShip_Movement` (MaxSpeed 10 등) | — | 플레이어와 동일 이동 성능 |
| `ScaleFactor` | 1.5 | × | 런타임 스케일 (코드 const) |
| 로드아웃 | `UpgradePool`에서 Main/Sub/Rear 랜덤 1개씩 | — | 스폰 시 무작위 장착 |
| 스테이지 강화 | `StageManager.CurrentStageIndex + 1` 회 모디파이어 적용 | — | 스테이지 진행마다 누적 강화 |
| `XPMagnet._radius` | 10 | m | (네임드도 흡인 반경 보유) |

#### 임시 이동 AI (`NamedWander`) — 추적 AI 전 placeholder

| 수치 (프리팹) | 값 | 단위 | 설명 |
|---|---|---|---|
| `_changeInterval` | 0.7 | s | 무작위 throttle/turn 재추첨 주기 (코드 기본 2.5 → 인스펙터 0.7로 더 자주) |
| `_minThrottle` | 0.3 | — | throttle = `Random(0.3, 1)`, turn = `Random(−1, 1)` |

#### 드롭 + 흡수 경쟁 (`AttachableDropper` → `AttachableWrapper`)

| 수치 | 값 | 단위 | 설명 |
|---|---|---|---|
| 드롭 | 사망 시 장착 어태처블 1개를 필드에 생성 | — | `TryGetRandomEquippedDefinition` |
| `AttachableWrapper._lifetime` | 30 | s | 경과 시 `Destroy` (풀링 미적용 — 주석 "추후") |
| 경쟁 흡수 | 선착순 1인 — `Consume()`로 중복 방지 | — | Player 접촉 → `UpgradeUI.OpenComponentPickup`(픽업 UI) / Named 접촉 → `PickupComponent`(자동 강화) |
| leash 디스폰 | 드롭 없음 | — | leash 이탈은 `OnDeadEvent`를 안 거침(= 회피 성공) |

- **근거**: GDD §2·§5.5 핵심 차별점 "드롭 경쟁". 네임드가 플레이어와 동일 함체/이동(Health 100·MaxSpeed 10)에
  스케일 1.5로 위압감을 주고, 드롭 수명 30s 안에 플레이어가 선점하지 못하면 다른 네임드가 흡수해 강해진다 —
  §0.3 "시간/공간/우선순위 경쟁" 긴장. **단 임시 `NamedWander`는 추격·흡수 의지가 없어** 경쟁 긴장이
  설계만큼 살지 않음(결과 보고서 §1) → v1.0 추적 AI로 교체 예정.
- **상태**: 시스템 `구현 기본값` · AI는 **임시(placeholder)**. 네임드 종 1종, 다형화 v1.0.

### 3.16 스폰 (`CommonSpawner` / `NamedSpawner`) — 씬 인스펙터

소스 [Core/Spawner/Enemy/](../Assets/Scripts/Core/Spawner/Enemy/), 값은 [InGame.unity](../Assets/Scenes/InGame.unity) 인스턴스 오버라이드(코드 기본값과 다름). **GDD §5.3** 매핑.

| 필드 | CommonSpawner | NamedSpawner | 단위 |
|---|---|---|---|
| `_spawnInterval` | 1 | 7 | s |
| `_maxAlive` | 200 | 5 | 마리 |
| `_spawnMinDistance` | 30 | 60 | m |
| `_spawnMaxDistance` | 90 | 100 | m |
| 이탈 처리 | `_cullRadius 150` (풀 반환) | `_leashRadius 120` (디스폰) | m |
| 체크 간격 | `_cullCheckInterval 1` | `_leashCheckInterval 1` | s |

- **근거**: GDD §5.3 "시간 기반 일반 몹 + 주기 네임드". 일반 몹은 1s마다 최대 200마리까지 — **§5.3.2 부하 테스트
  임계 후보값**(60fps 검증 대상). 네임드는 더 멀리(60~100m)·드물게(7s, 최대 5) 등장해 "이벤트성" 위협.
  `_leashRadius 120` > 미니맵 범위 40(§3.17)이라 GDD §5.5 "미니맵 밖에서도 한동안 추격" 충족.
- **상태**: `구현 기본값` (씬 튜닝값). `_maxAlive 200`은 부하 테스트로 검증 필요.
- **⚠️ 관찰**: NamedSpawner `_spawnMinDistance 60` > 미니맵 범위 40 → 네임드는 **미니맵 밖에서 스폰**해 접근 시
  레이더에 잡힌다. 의도(원거리 접근 위협)인지 가시성 문제인지 플레이테스트 확인 권장.

### 3.17 미니맵 레이더 (`MinimapUI` / `RadarModel`)

소스 [UI/Minimap/](../Assets/Scripts/UI/Minimap/), 값은 [InGame.unity](../Assets/Scenes/InGame.unity). **GDD §6.2** 매핑 — v0.3.0 신규(프로토타입).

| 수치 | 값 (출처) | 단위 | 설명 |
|---|---|---|---|
| `_worldRange` | 40 | m | 레이더가 커버하는 월드 반경 (씬 오버라이드, 코드 기본 60) |
| `_sweepDegPerSec` | 120 | deg/s | 스윕 라인 회전 속도 |
| 블립 — 보스 | 빨강 × 2.5 | — | 범위 밖이면 가장자리 클램프 표시 |
| 블립 — 네임드 | 흰색 × 2 | — | 부위는 표시 안 함 (GDD §5.5 — 부위는 화면뷰로) |
| 블립 — 일반 적 | 흰색 × 1 | — | `OverlapSphere`로 범위 내 검출, 태그 분류 |

- **근거**: GDD §6.2 "배틀쉽 레이더". 보스만 가장자리 클램프 + 빨강·대형으로 leash-less 추격(§3.11.2) 방향을
  항상 알려준다. 네임드(흰색 대형)는 부위 미표시로 §5.5 "부위는 화면뷰 판단" 규칙 유지.
- **상태**: `구현 기본값` (프로토타입). 위치/줌·드롭 아이콘·뷰포트 표시는 추후(GDD §6.2 `❓`).