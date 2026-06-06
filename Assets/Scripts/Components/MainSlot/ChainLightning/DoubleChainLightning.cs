public class DoubleChainLightning : ChainLightningBase
{
    private ChainLightningBase _inner;

    public DoubleChainLightning(ChainLightningBase inner) => _inner = inner;

    public override int Level => _inner.Level;
    public override bool CanUpgrade => _inner.CanUpgrade;
    public override int StrandCount => _inner.StrandCount * 2;       // 가닥 ×2
    public override int JumpsPerStrand => _inner.JumpsPerStrand;     // 점프 수는 레벨 소관 — 그대로 전달

    public override ChainLightningBase Upgrade()
    {
        _inner = _inner.Upgrade();
        return this;
    }
}
