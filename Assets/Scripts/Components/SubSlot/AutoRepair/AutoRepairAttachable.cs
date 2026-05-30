using UnityEngine;

public class AutoRepairAttachable : SubAttachableBase
{
    [SerializeField] private int _maxLevel = 3;

    [Tooltip("회복 주기 (초)")]
    [SerializeField] private float _interval = 3f;
    [Tooltip("Lv1 회복 비율")]
    [SerializeField] private float _baseHealPercent = 0.05f;
    [Tooltip("레벨당 추가 회복 비율")]
    [SerializeField] private float _incrementPerLevel = 0.07f;

    private int _level;
    private ShipBody _body;
    private float _timer;

    public override int Level => _level;
    public override bool CanUpgrade => _level < _maxLevel;

    private float CurrentHealAmount => _body.MaxHealth * (_baseHealPercent + _incrementPerLevel * (_level - 1));

    public override void Attach(LayerMask target, ShipStats stats)
    {
        base.Attach(target, stats);

        _level = 1;
        _body = GetComponentInParent<ShipBody>();
        _timer = 0f;
    }

    private void Update()
    {
        if (_body == null) return;

        _timer += Time.deltaTime;
        if (_timer < _interval) return;

        _timer = 0f;
        if (_body.CurrentHealth >= _body.MaxHealth) return;

        _body.Repair(CurrentHealAmount);
        ParticlePoolRegistry.Get(ParticleKind.Heal).Play(_body.transform.position);
    }

    public override void Upgrade()
    {
        if (!CanUpgrade) return;

        _level++;
    }
}
