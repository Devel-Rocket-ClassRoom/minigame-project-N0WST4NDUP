using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _button;

    private Action _onClick;

    private void Awake()
    {
        if (_button != null) _button.onClick.AddListener(HandleClick);
    }

    public void Bind(UpgradeOption option, Action onClick)
    {
        _icon.sprite = option.Icon;
        _nameText.text = option.Name;
        _levelText.text = $"Lv.{option.Level}";
        _descriptionText.text = option.Description;

        _levelText.gameObject.SetActive(option.Level > 0);
        _onClick = onClick;
    }

    private void HandleClick() => _onClick?.Invoke();
}
