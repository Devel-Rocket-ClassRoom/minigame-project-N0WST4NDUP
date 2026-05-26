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
    [SerializeField] private MainAttachableBase _startingComponent;
    private MainAttachableBase _mainSlot;
    [SerializeField] private Transform _mainSlotPosition;

    [Header("Sub Component")]
    private SubAttachableBase _subSlot;
    [SerializeField] private Transform _subSlotPosition;

    [Header("Rear Component")]
    private RearAttachableBase _rearSlot;
    [SerializeField] private Transform _rearSlotPosition;

    private ShipStats _stats;

    private void Awake()
    {
        _stats = GetComponent<ShipStats>();

        _mainSlot = Instantiate(_startingComponent, _mainSlotPosition);
    }

    private void Start()
    {
        _mainSlot?.Attach(_target, _stats);
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