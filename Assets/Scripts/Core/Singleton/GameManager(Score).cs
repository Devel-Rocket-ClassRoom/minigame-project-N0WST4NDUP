using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public int Score { get; private set; }

    public void AddScore(int amount)
    {
        if (amount <= 0) return;
        Score += amount;
    }

    public void ResetScore() => Score = 0;
}
