using UnityEngine;

public abstract class ShipComponent : MonoBehaviour
{
    [SerializeField][Min(1)] private int _mainSlotCount = 1;
    [SerializeField][Min(0)] private int _subSlotCount = 0;
    [SerializeField][Min(0)] private int _rearSlotCount = 1;

    private MainAttachableBase[] _mainSlots;
    private SubAttachableBase[] _subSlots;
    private RearAttachableBase[] _rearSlots;

    private void Awake()
    {
        _mainSlots = new MainAttachableBase[_mainSlotCount];
        _subSlots = new SubAttachableBase[_subSlotCount];
        _rearSlots = new RearAttachableBase[_rearSlotCount];
    }

    private void Update()
    {
        OperateSlots(_mainSlots);
        OperateSlots(_subSlots);
        OperateSlots(_rearSlots);
    }

    private void OperateSlots(IAttachable[] slots)
    {
        foreach (var slot in slots)
        {
            slot?.Tick();
        }
    }
}