using System;

public static class Utils
{
    public static DayTime Now
    {
        get
        {
            DateTime start = new DateTime(2023, 1, 1);
            long minutesSinceStart = (long)(DateTime.UtcNow - start).TotalMinutes;
            return new DayTime(minutesSinceStart);
        }
    }
}
