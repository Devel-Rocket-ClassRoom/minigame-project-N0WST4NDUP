using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private PlayerXP _playerXP;
    [SerializeField] private ShipComponent _ship;
    [SerializeField] private ShipStats _stats;
    [SerializeField] private UpgradePool _pool;

    [Header("Card Spawn")]
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private Transform _cardGroup;
    [SerializeField] private int _cardCount = 3;

    private readonly List<CardView> _spawned = new();

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        _playerXP.OnLevelUp += HandleLevelUp;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_playerXP != null) _playerXP.OnLevelUp -= HandleLevelUp;
        if (Instance == this) Instance = null;
    }

    // 레벨업: 풀에서 뽑은 정의들을 카드로 표시.
    private void HandleLevelUp()
    {
        var picks = _pool.Pick(_cardCount, _ship, _stats);

        var options = new List<(UpgradeOption, Action)>(picks.Count);
        foreach (var def in picks)
        {
            options.Add((def.BuildDisplay(_ship, _stats), () => def.Apply(_ship, _stats)));
        }
        ShowCards(options);
    }

    // 드롭 컴포넌트 픽업: 강화/교체 등 결과를 카드로 표시.
    public void OpenComponentPickup(UpgradeDefinition droppedDef)
    {
        var outcomes = PickupOutcomes.Build(droppedDef, _ship, _stats, _pool);

        var options = new List<(UpgradeOption, Action)>(outcomes.Count);
        foreach (var outcome in outcomes)
        {
            options.Add((outcome.Display, outcome.Apply));
        }
        ShowCards(options);
    }

    // 공통 렌더 코어: (표시, 선택 동작) 쌍 목록을 카드로 띄우고 게임을 멈춘다.
    private void ShowCards(IReadOnlyList<(UpgradeOption display, Action onSelect)> options)
    {
        if (options == null || options.Count == 0) return;
        if (gameObject.activeSelf) return; // 이미 카드가 떠 있으면 무시

        gameObject.SetActive(true);
        Time.timeScale = 0f;
        ClearSpawned();

        foreach (var (display, onSelect) in options)
        {
            var card = Instantiate(_cardPrefab, _cardGroup);
            card.Bind(display, () => { onSelect(); Close(); });
            _spawned.Add(card);
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void ClearSpawned()
    {
        foreach (var c in _spawned)
        {
            if (c != null) Destroy(c.gameObject);
        }
        _spawned.Clear();
    }
}
