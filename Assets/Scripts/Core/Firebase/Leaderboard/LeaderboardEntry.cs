using System;

// leaderboard/{uid} 항목. clearTimeMs 가 낮을수록 상위(빠른 클리어).
[Serializable]
public class LeaderboardEntry
{
    public string userId;
    public string nickname;
    public long clearTimeMs;
    public long timestamp;

    public LeaderboardEntry() { }

    public LeaderboardEntry(string userId, string nickname, long clearTimeMs, long timestamp)
    {
        this.userId = userId;
        this.nickname = nickname;
        this.clearTimeMs = clearTimeMs;
        this.timestamp = timestamp;
    }

    public float GetSeconds() => clearTimeMs / 1000f;

    public string ToJson() => UnityEngine.JsonUtility.ToJson(this);
    public static LeaderboardEntry FromJson(string json) => UnityEngine.JsonUtility.FromJson<LeaderboardEntry>(json);
}
