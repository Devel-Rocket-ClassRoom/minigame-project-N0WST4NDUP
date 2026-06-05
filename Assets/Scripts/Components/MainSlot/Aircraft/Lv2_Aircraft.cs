public class Lv2_Aircraft : AircraftBase
{
    public override int Level => 2;
    public override bool CanUpgrade => false;
    public override int FightersCount => 1;
    public override int TargetsPerFighter => 2;

    public override AircraftBase Upgrade() => this;
}
