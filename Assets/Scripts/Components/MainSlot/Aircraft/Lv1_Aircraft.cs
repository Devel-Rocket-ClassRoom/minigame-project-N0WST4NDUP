public class Lv1_Aircraft : AircraftBase
{
    public override int Level => 1;
    public override bool CanUpgrade => true;
    public override int FightersCount => 1;
    public override int TargetsPerFighter => 1;

    public override AircraftBase Upgrade() => new Lv2_Aircraft();
}
