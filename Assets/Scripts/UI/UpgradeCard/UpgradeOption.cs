using UnityEngine;

[System.Serializable]
public class StatChangeData
{
    public StatType Type;
    public int Before;
    public int After;
}

[System.Serializable]
public class UpgradeOption
{
    public Sprite Icon;
    public string Name;
    public int Level;
    public StatChangeData[] StatChanges;
}
