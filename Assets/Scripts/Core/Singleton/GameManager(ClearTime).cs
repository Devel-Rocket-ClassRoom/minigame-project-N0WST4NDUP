using UnityEngine;

// 현재 런의 클리어 타임(경과 시간) 측정. 스코어 기록 코드와 별개로 추가한 트래커.
// 베스트/히스토리는 DB(RecordManager)가 담당하므로 여기서는 현재 런 측정만 한다.
public partial class GameManager : MonoBehaviour
{
    // 확정된 현재 런 클리어 타임(초). StopClearTimer 시점에 캡처된다.
    public float ClearTime { get; private set; }

    private float _startTime;

    public void StartClearTimer() => _startTime = Time.time;

    // Time.time은 timeScale의 영향을 받으므로(일시정지 구간 자동 제외) snapshot 차이로 측정.
    public void StopClearTimer() => ClearTime = Time.time - _startTime;
}
