using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerXP _playerXP;

    [Header("Card Spawn")]
    [SerializeField] private CardView _cardPrefab;
    [SerializeField] private Transform _cardGroup;
    [SerializeField] private int _cardCount = 3;

    private readonly List<CardView> _spawned = new();

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
        gameObject.SetActive(true);
        ClearSpawned();

        for (int i = 0; i < _cardCount; i++)
        {
            var card = Instantiate(_cardPrefab, _cardGroup);
            card.Bind(BuildDummyOption());
            _spawned.Add(card);
        }
    }

    private void ClearSpawned()
    {
        foreach (var c in _spawned)
        {
            if (c != null) Destroy(c.gameObject);
        }
        _spawned.Clear();
    }

    private UpgradeOption BuildDummyOption()
    {
        return new UpgradeOption
        {
            Icon = null, // TODO: 에셋 찾으면 추가
            Name = "Cannon",
            Level = 1,
            StatChanges = new[]
            {
                new StatChangeData { Type = StatType.Damage, Before = 10, After = 20 },
                new StatChangeData { Type = StatType.FireRate, Before = 10, After = 20 },
                // new StatChangeData { Type = StatType.Range, Before = 10, After = 20 },
            }
        };
    }
}
