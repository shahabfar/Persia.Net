namespace Persia.Net.DateTimes;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a Gregorian DateTime to a PersianDateTime.
    /// </summary>
    /// <param name="date">The Gregorian DateTime to convert (nullable).</param>
    /// <returns>A PersianDateTime representing the converted date.</returns>
    public static PersianDateTime ToPersianDateTime(this DateTime? date)
    {
        if (!date.HasValue)
            throw new ArgumentNullException(nameof(date));

        return Converter.ConvertToPersian(date.Value)
            .SetTime(TimeOnly.FromTimeSpan(date.Value.TimeOfDay))
            .SetDayOfWeek((int)date.Value.DayOfWeek);
    }

    /// <summary>
    /// Converts a Gregorian DateTime to a PersianDateTime.
    /// </summary>
    /// <param name="date">The Gregorian DateTime to convert.</param>
    /// <returns>A PersianDateTime representing the converted date.</returns>
    public static PersianDateTime ToPersianDateTime(this DateTime date)
    {
        return Converter.ConvertToPersian(date)
            .SetTime(TimeOnly.FromTimeSpan(date.TimeOfDay))
            .SetDayOfWeek((int)date.DayOfWeek);
    }

}