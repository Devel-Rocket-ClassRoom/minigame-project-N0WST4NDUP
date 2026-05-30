using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ShipBody _playerHP;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private Slider _hpSlider;

    private void Start()
    {
        _playerHP.OnHealthChanged += UpdateUI;
        UpdateUI(_playerHP.CurrentHealth);
    }

    private void OnDestroy()
    {
        _playerHP.OnHealthChanged -= UpdateUI;
    }

    private void UpdateUI(float currentHP)
    {
        _hpText.text = $"{currentHP:F0}/{_playerHP.MaxHealth:F0}";
        _hpSlider.value = currentHP / _playerHP.MaxHealth;
    }
}