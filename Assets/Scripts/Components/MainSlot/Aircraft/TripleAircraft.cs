public class TripleAircraft : AircraftBase
{
    private AircraftBase _inner;

    public TripleAircraft(AircraftBase inner) => _inner = inner;

    public override int Level => _inner.Level;
    public override bool CanUpgrade => _inner.CanUpgrade;
    public override int FightersCount => _inner.FightersCount * 3;      // 대수 ×3
    public override int TargetsPerFighter => _inner.TargetsPerFighter;  // 타겟 수는 레벨 소관 — 그대로 전달

    public override AircraftBase Upgrade()
    {
        _inner = _inner.Upgrade();
        return this;
    }
}
