using System;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Level Curve")]
    [SerializeField] private int _baseXp = 10;
    [SerializeField] private int _stepXp = 3;

    public long TotalXp { get; private set; }
    public event Action OnXPChanged;
    public event Action OnLevelUp;

    public void AddXp(int amount)
    {
        int prevLevel = Resolve(TotalXp).level;
        TotalXp += amount;
        int newLevel = Resolve(TotalXp).level;

        OnXPChanged?.Invoke();
        for (int lv = prevLevel + 1; lv <= newLevel; lv++)
        {
            OnLevelUp?.Invoke();
        }
    }

    public (int level, long current, long max) Resolve(long total)
    {
        int level = 1;
        long need = MaxXpForLevel(level);
        while (total >= need)
        {
            total -= need;
            level++;
            need = MaxXpForLevel(level);
        }
        return (level, total, need);
    }

    private long MaxXpForLevel(int level) => _baseXp + (level - 1) * _stepXp;
}
