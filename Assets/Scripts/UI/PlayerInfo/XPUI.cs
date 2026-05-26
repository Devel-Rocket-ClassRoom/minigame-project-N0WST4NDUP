using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerXP _playerXP;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Slider _xpSlider;

    private void Start()
    {
        _playerXP.OnXPChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDestroy()
    {
        _playerXP.OnXPChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        var (level, current, max) = _playerXP.Resolve(_playerXP.TotalXp);
        float fillAmount = max > 0 ? (float)current / max : 0f;

        _levelText.text = $"{level}";
        _xpSlider.value = fillAmount;
    }
}