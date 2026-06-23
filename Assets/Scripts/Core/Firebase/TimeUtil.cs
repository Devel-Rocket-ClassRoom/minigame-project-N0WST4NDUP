using System;

// Firebase 저장용 시간 유틸 (Unix epoch millis 기준).
public static class TimeUtil
{
    public static long NowUnixMillis()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public static DateTime FromUnixMillis(long millis)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(millis).LocalDateTime;
    }

    public static string ToDateString(long millis)
    {
        return FromUnixMillis(millis).ToString("yyyy-MM-dd HH:mm:ss");
    }

    // 초 단위 시간을 mm:ss.ff (분:초.센티초) 형식으로. 클리어 타임 표시·리더보드 공용.
    public static string FormatDuration(float seconds)
    {
        if (seconds < 0f) seconds = 0f;

        int totalCentis = (int)System.Math.Round(seconds * 100.0);
        int minutes = totalCentis / 6000;
        int secs = (totalCentis / 100) % 60;
        int centis = totalCentis % 100;
        return $"{minutes:00}:{secs:02}.{centis:02}";
    }
}
