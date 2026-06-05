// 순수 로직 체인 — 함대 규모(FightersCount)와 전투기당 타겟 수(TargetsPerFighter)만 산출.
// 실제 전투기(GameObject)·전투·로밍은 AircraftAttachable이 소유/구동한다.
public abstract class AircraftBase : IUpgradable<AircraftBase>
{
    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }

    public abstract int FightersCount { get; }      // Double/Triple 데코레이터가 곱하는 축 (대수)
    public abstract int TargetsPerFighter { get; }  // 레벨업(Lv1→2→3)이 올리는 축 (전투기당 동시 타겟)

    public abstract AircraftBase Upgrade();
}
