using System;
using Cysharp.Threading.Tasks;
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
    [SerializeField] private Button _leaderboardButton;
    // [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private bool _processingGate = false;

    private void Start()
    {
        if (_startButton != null) _startButton.onClick.AddListener(OnStartClicked);
        if (_leaderboardButton != null) _leaderboardButton.onClick.AddListener(OnLeaderboardClicked);
        // if (_settingsButton != null) _settingsButton.onClick.AddListener(OpenSettings);
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
        if (_startButton != null) _startButton.onClick.RemoveListener(OnStartClicked);
        if (_leaderboardButton != null) _leaderboardButton.onClick.RemoveListener(OnLeaderboardClicked);
        // if (_settingsButton != null) _settingsButton.onClick.RemoveListener(OpenSettings);
        if (_exitButton != null) _exitButton.onClick.RemoveListener(ExitGame);
    }

    private void OnStartClicked() => RequireLoginAsync(StartGame).Forget();

    private void OnLeaderboardClicked() => RequireLoginAsync(OpenLeaderboard).Forget();

    private async UniTaskVoid RequireLoginAsync(Action onAuthorized)
    {
        if (_processingGate) return;
        _processingGate = true;

        try
        {
            bool ready = await AuthManager.Instance.WaitForInitializationAsync();

            if (ready && AuthManager.Instance.IsLoggedIn)
            {
                onAuthorized();
            }
            else
            {
                Debug.Log("[Title] 비로그인 상태 → 로그인 패널로 전환");
                _panelSwap.SwitchToLogin();
            }
        }
        finally
        {
            _processingGate = false;
        }
    }

    private void StartGame()
    {
        _panelSwap.SwitchToSelect();
    }

    private void OpenLeaderboard()
    {
        // TODO: 리더보드 패널/씬 열기 연결 (다음 단계)
        Debug.Log("[Title] 로그인 확인됨 → 리더보드 열기 (구현 예정)");
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
