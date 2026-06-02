using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _camera;
    [SerializeField] private PanelSwap _panelSwap;

    [Header("UI Elements")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        if (_startButton != null) _startButton.onClick.AddListener(StartGame);
        if (_settingsButton != null) _settingsButton.onClick.AddListener(OpenSettings);
        if (_exitButton != null) _exitButton.onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        if (_camera != null)
        {
            _camera.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (_camera != null)
        {
            _camera.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (_startButton != null) _startButton.onClick.RemoveListener(StartGame);
        if (_settingsButton != null) _settingsButton.onClick.RemoveListener(OpenSettings);
        if (_exitButton != null) _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void StartGame()
    {
        _panelSwap.NextPanel();
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