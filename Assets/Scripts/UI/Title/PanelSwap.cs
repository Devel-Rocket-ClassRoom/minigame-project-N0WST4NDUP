using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwap : MonoBehaviour
{
    private const string k_GameSceneName = "InGame";

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _titlePanel;
    [SerializeField] private GameObject _selectPanel;
    [SerializeField] private GameObject _loginPanels;

    private void Awake()
    {
        SwitchToTitle();

        Time.timeScale = 1f;
        _camera.SetActive(false);
    }

    // --- 패널 전환 API (다른 UI 스크립트가 호출) ------------------------------
    public void SwitchToTitle() => ShowOnly(_titlePanel);
    public void SwitchToSelect() => ShowOnly(_selectPanel);
    public void SwitchToLogin() => ShowOnly(_loginPanels);

    private void ShowOnly(GameObject target)
    {
        if (_titlePanel != null) _titlePanel.SetActive(target == _titlePanel);
        if (_selectPanel != null) _selectPanel.SetActive(target == _selectPanel);
        if (_loginPanels != null) _loginPanels.SetActive(target == _loginPanels);
    }

    // 선택 완료 → 인게임 씬 로드
    public void EnterGame()
    {
        ShowOnly(null);
        StartCoroutine(LoadGameSceneAsync());
    }

    private IEnumerator LoadGameSceneAsync()
    {
        _camera.SetActive(true);

        var asyncOperation = SceneManager.LoadSceneAsync(k_GameSceneName);
        asyncOperation.allowSceneActivation = false;

        yield return null;
        CinemachineBrain brain = CinemachineBrain.GetActiveBrain(0);
        while (brain != null && brain.IsBlending)
            yield return null;

        while (asyncOperation.progress < 0.9f)
            yield return null;
        asyncOperation.allowSceneActivation = true;
    }
}
