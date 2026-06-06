public class Lv2_ChainLightning : ChainLightningBase
{
    public override int Level => 2;
    public override bool CanUpgrade => true;
    public override int StrandCount => 1;
    public override int JumpsPerStrand => 2;

    public override ChainLightningBase Upgrade() => new Lv3_ChainLightning();
}
