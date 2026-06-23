using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultUI : MonoBehaviour
{
    private const string k_homeSceneName = "EntryPoint";
    private const float k_countUpDuration = 1.5f;
    private const string k_noClearText = "XXX";   // 클리어 실패(사망) 시 ClearTime 표기

    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _clearTimeText;
    [SerializeField] private TextMeshProUGUI _bestClearTimeText;
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
        if (_homeButton != null) _homeButton.onClick.RemoveListener(Home);
        if (_restartButton != null) _restartButton.onClick.RemoveListener(Restart);
    }

    private void HandleGameClear() => ShowResult("Victory", isClear: true);

    private void HandleGameOver() => ShowResult("Defeat", isClear: false);

    private void ShowResult(string title, bool isClear)
    {
        if (_titleText != null) _titleText.text = title;
        if (_panel != null) _panel.SetActive(true);
        Time.timeScale = 0f;

        GameManager.Instance.StopClearTimer();

        // Score: 승/패 공통 표기 (count-up)
        if (_scoreText != null) StartCoroutine(CountUpScore(GameManager.Instance.Score));

        // ClearTime: 클리어면 실제 기록, 죽으면 플레이스홀더 (DB 미반영)
        if (_clearTimeText != null)
        {
            _clearTimeText.text = isClear
                ? TimeUtil.FormatDuration(GameManager.Instance.ClearTime)
                : k_noClearText;
        }

        // BestTime: DB에서 (클리어 시 저장 후 갱신, 죽으면 조회만)
        if (_bestClearTimeText != null) _bestClearTimeText.text = "...";
        ResolveBestAsync(GameManager.Instance.ClearTime, isClear).Forget();
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

    // 베스트 클리어 타임: 클리어 시 저장(history + best) 후 갱신, 죽으면 조회만 (DB 미반영).
    private async UniTaskVoid ResolveBestAsync(float clearTime, bool isClear)
    {
        bool isNewBest = false;
        long bestMs;

        if (isClear)
        {
            (bool success, bool newBest, long best) = await RecordManager.Instance.SaveClearTimeAsync(clearTime);
            isNewBest = success && newBest;
            bestMs = best;
        }
        else
        {
            bestMs = await RecordManager.Instance.LoadBestMsAsync();
        }

        // 비동기 대기 중 결과창이 파괴됐을 수 있으므로 Unity null 체크로 방어
        if (_bestClearTimeText != null)
        {
            string bestStr = bestMs >= 0 ? TimeUtil.FormatDuration(bestMs / 1000f) : "--:--.--";
            _bestClearTimeText.text = isNewBest ? $"{bestStr} (신기록!)" : bestStr;
        }
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
