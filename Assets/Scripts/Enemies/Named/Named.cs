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

    // Main/Sub/Rear 슬롯에 풀에서 무작위로 하나씩 장착.
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
        Destroy(gameObject);
    }
}