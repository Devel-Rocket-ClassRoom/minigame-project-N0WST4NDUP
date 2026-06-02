using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    private const string k_GameSceneName = "InGame";

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (_startButton != null) _startButton.onClick.AddListener(StartGame);
        if (_settingsButton != null) _settingsButton.onClick.AddListener(OpenSettings);
        if (_exitButton != null) _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnDestroy()
    {
        if (_startButton != null) _startButton.onClick.RemoveListener(StartGame);
        if (_settingsButton != null) _settingsButton.onClick.RemoveListener(OpenSettings);
        if (_exitButton != null) _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(k_GameSceneName);
    }

    private void OpenSettings()
    {
        return;
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}