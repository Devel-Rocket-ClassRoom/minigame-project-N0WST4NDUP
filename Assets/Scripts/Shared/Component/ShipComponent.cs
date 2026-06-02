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
    [SerializeField] private MainAttachableBase _fallbackMainSlot;
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
    // public SubAttachableBase SubSlot => _subSlot;
    // public RearAttachableBase RearSlot => _rearSlot;

    private void Awake()
    {
        _stats = GetComponent<ShipStats>();

        _mainSlot = Instantiate(
            GameManager.Instance.PlayerConfig.StartingMain ?? _fallbackMainSlot,
            _mainSlotPosition);
    }

    private void Start()
    {
        _mainSlot?.Attach(_target, _stats);
        // _subSlot?.Attach();
        // _rearSlot?.Attach();
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
    }
}