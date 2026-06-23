using TMPro;
using UnityEngine;

// 리더보드 한 행. 순위/닉네임/클리어타임 표시. 본인 행은 _highlight로 강조(선택).
public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private GameObject _highlight;

    public void Set(int rank, LeaderboardEntry entry, bool isMe)
    {
        if (_rankText != null) _rankText.text = rank.ToString();
        if (_nicknameText != null) _nicknameText.text = entry.nickname;
        if (_timeText != null) _timeText.text = TimeUtil.FormatDuration(entry.GetSeconds());
        if (_highlight != null) _highlight.SetActive(isMe);
    }
}
