using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
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
    }

    private void HandleLevelUp()
    {
        var picks = _pool.Pick(_cardCount, _ship, _stats);
        if (picks.Count == 0) return;

        gameObject.SetActive(true);
        Time.timeScale = 0f;
        ClearSpawned();

        foreach (var def in picks)
        {
            var card = Instantiate(_cardPrefab, _cardGroup);
            card.Bind(def.BuildDisplay(_ship, _stats), () => Select(def));
            _spawned.Add(card);
        }
    }

    private void Select(UpgradeDefinition def)
    {
        def.Apply(_ship, _stats);
        Close();
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
