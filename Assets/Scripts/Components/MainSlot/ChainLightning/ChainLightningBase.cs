// 순수 로직 체인 — 동시 가닥 수(StrandCount)와 가닥당 점프 횟수(JumpsPerStrand)만 산출.
// 실제 타게팅·연쇄 전파·VFX는 ChainLightningAttachable이 소유/구동한다.
public abstract class ChainLightningBase : IUpgradable<ChainLightningBase>
{
    public abstract int Level { get; }
    public abstract bool CanUpgrade { get; }

    public abstract int StrandCount { get; }       // Double/Triple 데코레이터가 곱하는 축 (동시 가닥 수)
    public abstract int JumpsPerStrand { get; }     // 레벨업(Lv1→2→3)이 올리는 축 (가닥당 연쇄 점프 횟수)

    public abstract ChainLightningBase Upgrade();
}
