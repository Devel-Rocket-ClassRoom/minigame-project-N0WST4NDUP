using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipBody))]
[RequireComponent(typeof(ShipMovement))]
[RequireComponent(typeof(ShipComponent))]
public class Named : MonoBehaviour
{
    private const float ScaleFactor = 1.5f;

    // TODO: 임시로 인스펙터 노출
    // 추후 배의 종류가 늘어나면 주입 받는 식으로 변경
    [SerializeField] private ShipData _shipData;
    [SerializeField] private UpgradePool _loadoutPool;

    private ShipBody _body;
    private ShipMovement _movement;
    private ShipComponent _component;
    private ShipStats _stats;

    // 드롭 시 운반할 원본 정의(에셋이라 네임드 파괴 후에도 유효).
    private readonly List<UpgradeDefinition> _equippedDefs = new();

    private void Awake()
    {
        _body = GetComponent<ShipBody>();
        _movement = GetComponent<ShipMovement>();
        _component = GetComponent<ShipComponent>();
        _stats = GetComponent<ShipStats>();

        _body.Init(_shipData);

        _body.OnDeadEvent += HandleNamedDeath;
    }

    private void Start()
    {
        transform.localScale = Vector3.one * ScaleFactor;

        EquipRandomLoadout();
    }

    public void Init(ShipData shipData)
    {
        _shipData = shipData;
        _body.Init(_shipData);
    }

    private void EquipRandomLoadout()
    {
        if (_loadoutPool == null) return;

        Equip(PickRandom<MainEquipment>());
        Equip(PickRandom<SubEquipment>());
        Equip(PickRandom<RearEquipment>());
    }

    private void Equip(UpgradeDefinition def)
    {
        if (def == null) return;

        def.Apply(_component, _stats);
        _equippedDefs.Add(def);
    }

    // 사망 시 드롭용: 장착했던 정의 중 하나를 무작위로 반환.
    public bool TryGetRandomEquippedDefinition(out UpgradeDefinition def)
    {
        if (_equippedDefs.Count == 0)
        {
            def = null;
            return false;
        }

        def = _equippedDefs[Random.Range(0, _equippedDefs.Count)];
        return true;
    }

    // 다른 네임드가 드롭을 주웠을 때: 무조건 강화(빈 슬롯=장착, 최대레벨=모디파이어).
    public void PickupComponent(UpgradeDefinition def)
    {
        var outcomes = PickupOutcomes.Build(def, _component, _stats, _loadoutPool);
        if (outcomes.Count > 0) outcomes[0].Apply();
    }

    private T PickRandom<T>() where T : UpgradeDefinition
    {
        var matches = new List<T>();

        foreach (var def in _loadoutPool.AttachmentDefinitions)
        {
            if (def is T typed) matches.Add(typed);
        }
        if (matches.Count == 0) return null;

        return matches[Random.Range(0, matches.Count)];
    }

    private void HandleNamedDeath()
    {
        _body.OnDeadEvent -= HandleNamedDeath;



        ParticlePoolRegistry.Get(ParticleKind.Die).Play(transform.position);
        Destroy(gameObject);
    }
}