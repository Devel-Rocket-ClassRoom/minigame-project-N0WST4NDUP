using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        gameObject.SetActive(true);

        if (_panel != null) _panel.SetActive(false);
        StageManager.OnGameClear += HandleGameClear;
        if (_homeButton != null) _homeButton.onClick.AddListener(Restart);
        if (_restartButton != null) _restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        StageManager.OnGameClear -= HandleGameClear;
        if (_restartButton != null) _restartButton.onClick.RemoveListener(Restart);
    }

    private void HandleGameClear()
    {
        Debug.Log("[StageClearUI] Game clear received → showing Win panel, timeScale = 0");
        if (_panel != null) _panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
