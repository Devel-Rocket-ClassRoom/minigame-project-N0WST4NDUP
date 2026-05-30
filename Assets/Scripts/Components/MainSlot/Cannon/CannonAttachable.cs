using UnityEngine;

public class CannonAttachable : MainAttachableBase
{
    [Header("Cannon Config")]
    [SerializeField] protected Transform _firePoint;

    [Tooltip("포물선 정점 높이(m)")]
    [SerializeField] protected float _arcHeight = 5f;

    [Tooltip("비행 시간(초)")]
    [SerializeField] protected float _flightDuration = 0.7f;

    private CannonBase _cannon;

    public CannonBase Cannon => _cannon;
    public override int Level => _cannon?.Level ?? 0;
    public override bool CanUpgrade => _cannon == null ? true : _cannon.CanUpgrade;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _cannon = new Lv1_Cannon(_data, _stats);
        _cannon.SetBarrel(
            _targetLayer,
            _firePoint,
            _arcHeight,
            _flightDuration
        );
    }

    private void Update()
    {
        _cannon?.Tick();
    }

    public override void Upgrade()
    {
        _cannon = _cannon.Upgrade();
    }

    public override void WrapDouble()
    {
        _cannon = new DoubleCannon(_cannon);

    }

    public override void WrapTriple()
    {
        _cannon = new TripleCannon(_cannon);
    }
}