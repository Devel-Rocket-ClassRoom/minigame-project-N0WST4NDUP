using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipStats))]
public class ShipComponent : MonoBehaviour
{
    // [SerializeField][Min(1)] private int _mainSlotCount = 1;
    // [SerializeField][Min(0)] private int _subSlotCount = 0;
    // [SerializeField][Min(0)] private int _rearSlotCount = 1;
    [Header("Default Config")]
    [SerializeField] private LayerMask _target;

    [Header("Main Component")]
    private MainAttachableBase _mainSlot;
    [SerializeField] private Transform _mainSlotPosition;

    [Header("Sub Component")]
    private SubAttachableBase _subSlot;
    [SerializeField] private Transform _subSlotPosition;

    [Header("Rear Component")]
    private RearAttachableBase _rearSlot;
    [SerializeField] private Transform _rearSlotPosition;

    private ShipStats _stats;

    public MainAttachableBase MainSlot => _mainSlot;
    public SubAttachableBase SubSlot => _subSlot;
    public RearAttachableBase RearSlot => _rearSlot;

    // 슬롯 장착/강화/교체 시 발생 — UI 등 구독자가 다시 읽도록.
    public event Action OnSlotsChanged;

    private void Awake()
    {
        _stats = GetComponent<ShipStats>();
    }

    public bool IsEmpty(IAttachable attachable) => attachable switch
    {
        MainAttachableBase _ => _mainSlot == null,
        SubAttachableBase _ => _subSlot == null,
        RearAttachableBase _ => _rearSlot == null,
        _ => false
    };

    public int GetLevel(IAttachable attachable) => attachable switch
    {
        MainAttachableBase _ => _mainSlot?.Level ?? 0,
        SubAttachableBase _ => _subSlot?.Level ?? 0,
        RearAttachableBase _ => _rearSlot?.Level ?? 0,
        _ => 0
    };

    public bool CanInstall(IAttachable attachable) => attachable switch
    {
        MainAttachableBase main =>
            _mainSlot == null ||
            (_mainSlot.GetType() == main.GetType() && _mainSlot.CanUpgrade),
        SubAttachableBase sub =>
            _subSlot == null ||
            (_subSlot.GetType() == sub.GetType() && _subSlot.CanUpgrade),
        RearAttachableBase rear =>
            _rearSlot == null ||
            (_rearSlot.GetType() == rear.GetType() && _rearSlot.CanUpgrade),
        _ => false
    };

    // 드롭품의 타입으로 슬롯을 지목해, 현재 그 슬롯에 장착된 장비가 강화 가능한지.
    public bool SlotCanUpgrade(IAttachable attachable) => attachable switch
    {
        MainAttachableBase _ => _mainSlot != null && _mainSlot.CanUpgrade,
        SubAttachableBase _ => _subSlot != null && _subSlot.CanUpgrade,
        RearAttachableBase _ => _rearSlot != null && _rearSlot.CanUpgrade,
        _ => false
    };

    // 드롭품 타입으로 슬롯을 지목해, 현재 그 슬롯 장비가 드롭품과 같은 타입인지.
    public bool IsSameTypeEquipped(IAttachable attachable) => attachable switch
    {
        MainAttachableBase _ => _mainSlot != null && _mainSlot.GetType() == attachable.GetType(),
        SubAttachableBase _ => _subSlot != null && _subSlot.GetType() == attachable.GetType(),
        RearAttachableBase _ => _rearSlot != null && _rearSlot.GetType() == attachable.GetType(),
        _ => false
    };

    // 드롭품 타입으로 슬롯을 지목해, 그 슬롯의 현재 장비를 한 단계 강화(드롭품 타입 무관).
    public void UpgradeCurrentSlot(IAttachable attachable)
    {
        switch (attachable)
        {
            case MainAttachableBase _: _mainSlot?.Upgrade(); break;
            case SubAttachableBase _: _subSlot?.Upgrade(); break;
            case RearAttachableBase _: _rearSlot?.Upgrade(); break;
        }

        OnSlotsChanged?.Invoke();
    }

    // 현재 슬롯 장비를 떼어내고 드롭품을 Lv1로 새로 장착(다른 타입 스왑).
    public void Replace(IAttachable attachable)
    {
        switch (attachable)
        {
            case MainAttachableBase main:
                _mainSlot?.Detach();
                _mainSlot = Instantiate(main, _mainSlotPosition);
                _mainSlot.Attach(_target, _stats);
                break;
            case SubAttachableBase sub:
                _subSlot?.Detach();
                _subSlot = Instantiate(sub, _subSlotPosition);
                _subSlot.Attach(_target, _stats);
                break;
            case RearAttachableBase rear:
                _rearSlot?.Detach();
                _rearSlot = Instantiate(rear, _rearSlotPosition);
                _rearSlot.Attach(_target, _stats);
                break;
        }

        OnSlotsChanged?.Invoke();
    }

    public void Install(IAttachable attachable)
    {
        if (!CanInstall(attachable)) return;

        switch (attachable)
        {
            case MainAttachableBase main:
                if (_mainSlot == null)
                {
                    _mainSlot = Instantiate(main, _mainSlotPosition);
                    _mainSlot.Attach(_target, _stats);
                }
                else
                {
                    _mainSlot.Upgrade();
                }
                break;
            case SubAttachableBase sub:
                if (_subSlot == null)
                {
                    _subSlot = Instantiate(sub, _subSlotPosition);
                    _subSlot.Attach(_target, _stats);
                }
                else
                {
                    _subSlot.Upgrade();
                }
                break;
            case RearAttachableBase rear:
                if (_rearSlot == null)
                {
                    _rearSlot = Instantiate(rear, _rearSlotPosition);
                    _rearSlot.Attach(_target, _stats);
                }
                else
                {
                    _rearSlot.Upgrade();
                }
                break;
        }

        OnSlotsChanged?.Invoke();
    }
}