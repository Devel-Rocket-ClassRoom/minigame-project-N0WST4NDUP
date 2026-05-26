using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    public void Bind(UpgradeOption option)
    {
        _icon.sprite = option.Icon;
        _nameText.text = option.Name;
        _levelText.text = $"Lv.{option.Level}";
        _descriptionText.text = option.Description;

        _levelText.gameObject.SetActive(option.Level > 0);
    }
}
