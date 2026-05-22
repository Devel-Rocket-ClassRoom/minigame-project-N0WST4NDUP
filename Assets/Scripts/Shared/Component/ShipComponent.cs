using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    // [SerializeField][Min(1)] private int _mainSlotCount = 1;
    // [SerializeField][Min(0)] private int _subSlotCount = 0;
    // [SerializeField][Min(0)] private int _rearSlotCount = 1;
    [Header("Default Config")]
    [SerializeField] private LayerMask _target;

    [Header("Main Component")]
    [SerializeField] private MainAttachableBase _mainSlot;
    [SerializeField] private Transform _mainSlotPosition;

    [Header("Sub Component")]
    [SerializeField] private SubAttachableBase _subSlot;
    [SerializeField] private Transform _subSlotPosition;

    [Header("Rear Component")]
    [SerializeField] private RearAttachableBase _rearSlot;
    [SerializeField] private Transform _rearSlotPosition;

    private void Awake()
    {
        var go = Instantiate(_mainSlot, _mainSlotPosition);
        go.SetTarget(_target);
    }

    private void Start()
    {
        _mainSlot?.Attach();
        _subSlot?.Attach();
        _rearSlot?.Attach();
    }

    // private void Awake()
    // {
    //     _mainSlots = new MainAttachableBase[_mainSlotCount];
    //     _subSlots = new SubAttachableBase[_subSlotCount];
    //     _rearSlots = new RearAttachableBase[_rearSlotCount];
    // }
}