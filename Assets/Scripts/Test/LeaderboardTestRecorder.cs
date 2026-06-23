using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

// [임시 테스트용] 지정 키를 누르면 더미 클리어 타임을 RecordManager로 기록한다.
// 아무 오브젝트에나 붙이고 플레이 모드에서 키를 누르면 records/best + leaderboard 에 반영됨.
// 테스트 끝나면 삭제할 것.
public class LeaderboardTestRecorder : MonoBehaviour
{
    [SerializeField] private Key _triggerKey = Key.L;
    [SerializeField] private float _testClearMinutes = 30f;

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current[_triggerKey].wasPressedThisFrame)
        {
            RecordTestAsync().Forget();
        }
    }

    private async UniTaskVoid RecordTestAsync()
    {
        float seconds = _testClearMinutes * 60f;
        Debug.Log($"[Test] 클리어 타임 등록 시도: {_testClearMinutes}분 ({seconds:0}s)");

        var (success, isNewBest, bestMs) = await RecordManager.Instance.SaveClearTimeAsync(seconds);

        if (success)
        {
            Debug.Log($"[Test] 등록 성공! 신기록={isNewBest}, best={bestMs}ms ({TimeUtil.FormatDuration(bestMs / 1000f)})");
        }
        else
        {
            Debug.LogWarning("[Test] 등록 실패 — 로그인 상태/DB 규칙 확인. 위의 [Record]/[Leaderboard] 에러 로그 참고.");
        }
    }
}
