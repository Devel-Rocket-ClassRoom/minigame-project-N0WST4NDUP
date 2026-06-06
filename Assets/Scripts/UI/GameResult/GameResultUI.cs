using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    private const string k_homeSceneName = "EntryPoint";
    private const float k_countUpDuration = 1.5f;

    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        gameObject.SetActive(true);

        if (_panel != null) _panel.SetActive(false);
        StageManager.OnGameClear += HandleGameClear;
        StageManager.OnGameOver += HandleGameOver;
        if (_homeButton != null) _homeButton.onClick.AddListener(Home);
        if (_restartButton != null) _restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        StageManager.OnGameClear -= HandleGameClear;
        StageManager.OnGameOver -= HandleGameOver;
        if (_restartButton != null) _restartButton.onClick.RemoveListener(Restart);
    }

    private void HandleGameClear() => ShowResult("Victory");

    private void HandleGameOver() => ShowResult("Defeat");

    private void ShowResult(string title)
    {
        if (_titleText != null) _titleText.text = title;
        if (_panel != null) _panel.SetActive(true);
        Time.timeScale = 0f;

        if (_scoreText != null) StartCoroutine(CountUpScore(GameManager.Instance.Score));
    }

    // timeScale = 0 상태에서 동작해야 하므로 unscaled time 사용.
    private IEnumerator CountUpScore(int target)
    {
        float elapsed = 0f;
        while (elapsed < k_countUpDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            int value = Mathf.RoundToInt(Mathf.Lerp(0f, target, elapsed / k_countUpDuration));
            _scoreText.text = value.ToString();
            yield return null;
        }
        _scoreText.text = target.ToString();
    }

    private void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(k_homeSceneName);
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
