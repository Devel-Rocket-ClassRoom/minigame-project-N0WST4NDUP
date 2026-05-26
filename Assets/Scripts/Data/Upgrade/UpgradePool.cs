using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Pool", fileName = "UpgradePool")]
public class UpgradePool : ScriptableObject
{
    [SerializeField] private UpgradeDefinition[] _definitions;

    public List<UpgradeDefinition> Pick(int count, ShipComponent ship, ShipStats stats)
    {
        var available = new List<UpgradeDefinition>();
        foreach (var d in _definitions)
        {
            if (d != null && d.IsAvailable(ship, stats)) available.Add(d);
        }

        int picks = Mathf.Min(count, available.Count);
        for (int i = 0; i < picks; i++)
        {
            int j = Random.Range(i, available.Count);
            (available[i], available[j]) = (available[j], available[i]);
        }
        available.RemoveRange(picks, available.Count - picks);
        return available;
    }
}
