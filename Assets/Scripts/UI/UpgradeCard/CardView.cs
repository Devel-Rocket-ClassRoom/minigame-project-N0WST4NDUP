using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private StatChange[] _statRows;
    [SerializeField] private StatIconSet _statIcons;

    public void Bind(UpgradeOption option)
    {
        _icon.sprite = option.Icon;
        _nameText.text = option.Name;
        _levelText.text = $"Lv.{option.Level}";

        for (int i = 0; i < _statRows.Length; i++)
        {
            bool hasData = option.StatChanges != null && i < option.StatChanges.Length;
            _statRows[i].gameObject.SetActive(hasData);
            if (hasData)
            {
                var data = option.StatChanges[i];
                _statRows[i].Bind(_statIcons.GetIcon(data.Type), data);
            }
        }
    }
}
