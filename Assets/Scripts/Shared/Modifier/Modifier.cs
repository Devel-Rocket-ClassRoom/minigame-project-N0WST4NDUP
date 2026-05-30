using System;

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

    public static bool operator ==(Modifier a, Modifier b) => a.Equals(b);
    public static bool operator !=(Modifier a, Modifier b) => !a.Equals(b);

    public override bool Equals(object obj)
    {
        if (obj is Modifier other)
        {
            return Stat == other.Stat && Op == other.Op && Value == other.Value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Stat, Op, Value);
    }
}
