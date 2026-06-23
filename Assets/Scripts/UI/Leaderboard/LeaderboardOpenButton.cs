using UnityEngine;
using UnityEngine.UI;

// 버튼에 붙여서 씬에 배치된 리더보드 패널을 연다(SetActive). EntryPoint·클리어 팝업 양쪽에 사용.
[RequireComponent(typeof(Button))]
public class LeaderboardOpenButton : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardPanel;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Open);
    }

    private void OnDestroy()
    {
        if (_button != null) _button.onClick.RemoveListener(Open);
    }

    private void Open()
    {
        if (_leaderboardPanel != null) _leaderboardPanel.SetActive(true);
    }
}
