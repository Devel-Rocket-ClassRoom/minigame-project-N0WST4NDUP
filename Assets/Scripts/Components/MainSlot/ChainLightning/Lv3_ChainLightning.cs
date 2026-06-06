public class Lv3_ChainLightning : ChainLightningBase
{
    public override int Level => 3;
    public override bool CanUpgrade => false;
    public override int StrandCount => 1;
    public override int JumpsPerStrand => 3;

    public override ChainLightningBase Upgrade() => this;
}
