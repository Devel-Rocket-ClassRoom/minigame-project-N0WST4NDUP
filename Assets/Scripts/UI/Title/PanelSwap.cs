using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwap : MonoBehaviour
{
    private const string k_GameSceneName = "InGame";

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject[] _panels;

    private int _currentPanelIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetActive(i == _currentPanelIndex);
        }

        Time.timeScale = 1f;
        _camera.SetActive(false);
    }

    public void NextPanel()
    {
        if (_currentPanelIndex < _panels.Length - 1)
        {
            _panels[_currentPanelIndex++].SetActive(false);
            _panels[_currentPanelIndex].SetActive(true);
        }
        else
        {
            _panels[_currentPanelIndex].SetActive(false);
            StartCoroutine(LoadGameSceneAsync());
        }
    }

    public void PreviousPanel()
    {
        if (_currentPanelIndex > 0)
        {
            _panels[_currentPanelIndex--].SetActive(false);
            _panels[_currentPanelIndex].SetActive(true);
        }
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