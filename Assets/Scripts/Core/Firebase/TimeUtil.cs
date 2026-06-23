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
}
