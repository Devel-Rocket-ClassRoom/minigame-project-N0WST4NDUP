using System;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Level Curve")]
    [SerializeField] private int _baseXp = 10;
    [SerializeField] private int _stepXp = 3;

    public long TotalXp { get; private set; }
    public event Action OnXPChanged;

    public void AddXp(int amount)
    {
        TotalXp += amount;
        OnXPChanged?.Invoke();
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
