using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/StatIconSet", fileName = "StatIconSet")]
public class StatIconSet : ScriptableObject
{
    [System.Serializable]
    private class Entry
    {
        public StatType Type;
        public Sprite Icon;
    }

    [SerializeField] private List<Entry> _entries = new();

    public Sprite GetIcon(StatType type)
    {
        foreach (var e in _entries)
        {
            if (e.Type == type) return e.Icon;
        }
        return null;
    }
}
