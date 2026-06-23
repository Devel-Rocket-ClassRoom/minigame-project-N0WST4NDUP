using System;

// records/{uid}/history/{pushId} 항목. clearTimeMs = 클리어 소요 시간(ms, 낮을수록 빠름).
[Serializable]
public class ClearTimeRecord
{
    public long clearTimeMs;
    public long timestamp;

    public ClearTimeRecord() { }

    public ClearTimeRecord(long clearTimeMs, long timestamp)
    {
        this.clearTimeMs = clearTimeMs;
        this.timestamp = timestamp;
    }

    public float GetSeconds() => clearTimeMs / 1000f;
    public string GetDateString() => TimeUtil.ToDateString(timestamp);

    public string ToJson() => UnityEngine.JsonUtility.ToJson(this);
    public static ClearTimeRecord FromJson(string json) => UnityEngine.JsonUtility.FromJson<ClearTimeRecord>(json);
}
