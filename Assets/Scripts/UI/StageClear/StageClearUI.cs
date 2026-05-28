using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        if (_panel != null) _panel.SetActive(false);
        PirateLord.OnBossDeathEvent += HandleBossDeath;
        if (_restartButton != null) _restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        PirateLord.OnBossDeathEvent -= HandleBossDeath;
        if (_restartButton != null) _restartButton.onClick.RemoveListener(Restart);
    }

    private void HandleBossDeath(Vector3 _)
    {
        Debug.Log("[StageClearUI] Boss death received → showing clear panel, timeScale = 0");
        if (_panel != null) _panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
