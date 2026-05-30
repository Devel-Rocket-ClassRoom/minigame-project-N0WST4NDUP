using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _phaseText;

    private ShipBody _bossBody;

    private void Start()
    {
        StageManager.OnStageStarted += HandleStageStarted;
        PirateLord.OnBossSpawned += HandleBossSpawned;
        PirateLord.OnPhaseChanged += HandlePhaseChanged;
        PirateLord.OnBossDeathEvent += HandleBossDeath;

        SetVisible(StageManager.CurrentStage != null);
    }

    private void OnDestroy()
    {
        StageManager.OnStageStarted -= HandleStageStarted;
        PirateLord.OnBossSpawned -= HandleBossSpawned;
        PirateLord.OnPhaseChanged -= HandlePhaseChanged;
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
        if (_bossBody != null) _bossBody.OnHealthChanged -= HandleHealthChanged;
    }

    private void Update()
    {
        // 보스 등장 후에는 OnHealthChanged 이벤트가 표시 갱신을 담당
        if (_bossBody != null) return;
        if (StageManager.CurrentStage == null) return;

        float total = StageManager.BossSpawnAfterSec;
        float remain = Mathf.Max(0f, total - StageManager.Elapsed);
        _slider.value = total > 0f ? 1f - remain / total : 0f;
        if (_valueText != null) _valueText.text = FormatTime(remain);
        if (_phaseText != null) _phaseText.text = string.Empty;
    }

    private void HandleStageStarted(StageData _)
    {
        if (_bossBody != null)
        {
            _bossBody.OnHealthChanged -= HandleHealthChanged;
            _bossBody = null;
        }
        SetVisible(true);
    }

    private void HandleBossSpawned(PirateLord boss)
    {
        _bossBody = boss.Body;
        if (_bossBody != null) _bossBody.OnHealthChanged += HandleHealthChanged;
        if (_phaseText != null) _phaseText.text = boss.CurrentPhase.ToString();
        RefreshHpDisplay();
    }

    private void HandlePhaseChanged(Phase phase)
    {
        if (_phaseText != null) _phaseText.text = phase.ToString();
        RefreshHpDisplay();
    }

    private void HandleHealthChanged(float _) => RefreshHpDisplay();

    private void HandleBossDeath(Vector3 _)
    {
        if (_bossBody != null)
        {
            _bossBody.OnHealthChanged -= HandleHealthChanged;
            _bossBody = null;
        }
        SetVisible(false);
    }

    private void RefreshHpDisplay()
    {
        if (_bossBody == null) return;
        float max = _bossBody.MaxHealth;
        float cur = _bossBody.CurrentHealth;
        _slider.value = max > 0f ? cur / max : 0f;
        if (_valueText != null) _valueText.text = $"{Mathf.CeilToInt(cur)}/{Mathf.CeilToInt(max)}";
    }

    private void SetVisible(bool visible)
    {
        if (_panel != null) _panel.SetActive(visible);
    }

    private static string FormatTime(float seconds)
    {
        int s = Mathf.CeilToInt(seconds);
        return $"{s / 60}:{s % 60:00}";
    }
}
