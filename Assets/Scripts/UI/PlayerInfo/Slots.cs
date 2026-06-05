using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ShipComponent _ship;

    [Header("Main Slot")]
    [SerializeField] private Image _mainSlot;
    [SerializeField] private TextMeshProUGUI _mainLevel;

    [Header("Sub Slot")]
    [SerializeField] private Image _subSlot;
    [SerializeField] private TextMeshProUGUI _subLevel;

    [Header("Rear Slot")]
    [SerializeField] private Image _rearSlot;
    [SerializeField] private TextMeshProUGUI _rearLevel;

    private void Start()
    {
        _ship.OnSlotsChanged += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        _ship.OnSlotsChanged -= Refresh;
    }

    private void Refresh()
    {
        UpdateSlot(_mainSlot, _mainLevel, _ship.MainSlot);
        UpdateSlot(_subSlot, _subLevel, _ship.SubSlot);
        UpdateSlot(_rearSlot, _rearLevel, _ship.RearSlot);
    }

    private void UpdateSlot(Image slot, TextMeshProUGUI levelText, IAttachable equipment)
    {
        bool equipped = equipment != null;

        slot.enabled = equipped;
        levelText.gameObject.SetActive(equipped);

        if (equipped)
        {
            slot.sprite = equipment.Icon;
            levelText.text = $"Lv. {equipment.Level}";
        }
    }
}
