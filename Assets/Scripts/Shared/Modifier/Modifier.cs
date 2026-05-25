public enum ModifierOp
{
    Add,
    PercentAdd
}

[System.Serializable]
public class Modifier
{
    public StatType Stat;
    public ModifierOp Op;
    public float Value;
}
