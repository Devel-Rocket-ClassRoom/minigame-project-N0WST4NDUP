using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _camera;
    [SerializeField] private PanelSwap _panelSwap;

    [Header("UI Elements")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _confirmButton;

    [SerializeField] private Transform _playerMainSlotPosition;
    [SerializeField] private MainAttachableBase[] _selectableMainAttachables;

    private GameObject[] _mainPreviews;
    private int _currentSelectionIndex = 0;

    private void Awake()
    {
        if (_nextButton != null) _nextButton.onClick.AddListener(NextItem);
        if (_previousButton != null) _previousButton.onClick.AddListener(PreviousItem);
        if (_exitButton != null) _exitButton.onClick.AddListener(_panelSwap.SwitchToTitle);
        if (_confirmButton != null) _confirmButton.onClick.AddListener(ConfirmSelection);

        _mainPreviews = new GameObject[_selectableMainAttachables.Length];
    }

    private void Start()
    {
        for (int i = 0; i < _selectableMainAttachables.Length; i++)
        {
            _mainPreviews[i] = Instantiate(_selectableMainAttachables[i], _playerMainSlotPosition).gameObject;
        }
        UpdateSelection();
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

    private void NextItem()
    {
        _currentSelectionIndex = (_currentSelectionIndex + 1) % _selectableMainAttachables.Length;
        UpdateSelection();
    }

    private void PreviousItem()
    {
        _currentSelectionIndex = (_currentSelectionIndex - 1 + _selectableMainAttachables.Length) % _selectableMainAttachables.Length;
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        for (int i = 0; i < _mainPreviews.Length; i++)
        {
            _mainPreviews[i].SetActive(i == _currentSelectionIndex);
        }
    }

    private void ConfirmSelection()
    {
        var selectedMain = _selectableMainAttachables[_currentSelectionIndex];
        GameManager.Instance.PlayerConfig.SetStartingMain(selectedMain);

        _panelSwap.EnterGame();
    }
}