public class Lv1_ChainLightning : ChainLightningBase
{
    public override int Level => 1;
    public override bool CanUpgrade => true;
    public override int StrandCount => 1;
    public override int JumpsPerStrand => 1;

    public override ChainLightningBase Upgrade() => new Lv2_ChainLightning();
}
