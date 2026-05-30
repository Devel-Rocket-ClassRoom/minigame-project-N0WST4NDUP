using UnityEngine;

public class MineDropperAttachable : RearAttachableBase
{
    [Header("Mine Config")]
    [SerializeField] private Transform _spawnPoint;

    [Tooltip("지뢰 수명(초)")]
    [SerializeField] private float _mineLifetime = 30f;

    private MineDropperBase _dropper;

    public MineDropperBase Dropper => _dropper;
    public override int Level => _dropper?.Level ?? 0;
    public override bool CanUpgrade => _dropper == null ? true : _dropper.CanUpgrade;

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _dropper = new Lv1_MineDropper(_data, _stats);
        _dropper.SetDropper(
            _target,
            _spawnPoint,
            _mineLifetime
        );
    }

    private void Update()
    {
        _dropper?.Tick();
    }

    public override void Upgrade()
    {
        _dropper = _dropper.Upgrade();
    }
}
