using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatChange : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _beforeText;
    [SerializeField] private TextMeshProUGUI _afterText;

    public void Bind(Sprite icon, StatChangeData data)
    {
        _icon.sprite = icon;
        _beforeText.text = data.Before.ToString();
        _afterText.text = data.After.ToString();
    }
}
