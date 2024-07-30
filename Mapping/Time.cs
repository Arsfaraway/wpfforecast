namespace WeathForecast.Mapping;

public static class Time
{
    private static DateTime TakeCityDataTime(int timeZone)
    {
        return DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromSeconds(timeZone)).DateTime;
    }

    private static TimeSpan DateTimeInTimeSpan(DateTime dateTime)
    {
        return dateTime.TimeOfDay;
    }

    private static TimeSpan TakeTimeSpanDiffServerAndCity(int timeZone)
    {
        return DateTime.Now - TakeCityDataTime(timeZone);
    }

    private static TimeSpan TimeSpanRounding(TimeSpan timeSpan)
    {
        return TimeSpan.FromSeconds(Math.Round(timeSpan.TotalSeconds));
    }

    public static TimeSpan TakeCityCurrentTime(int timeZone)
    {
        return TimeSpanRounding(DateTimeInTimeSpan(TakeCityDataTime(timeZone)));
    }

    public static TimeSpan TakeServerCurrentTime(int timeZone)
    {
        return TimeSpanRounding(DateTimeInTimeSpan(DateTime.Now));
    }

    public static TimeSpan TakeTimeDifferenceBetweenCityAndServer(int timeZone)
    {
        return TimeSpanRounding(TakeTimeSpanDiffServerAndCity(timeZone));
    }
}