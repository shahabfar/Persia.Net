using Persia.Net.Core;

namespace Persia.Net;

public static class DateTimeIslamicExtensions
{
    /// <summary>
    /// Converts a Gregorian DateTime to a IslamicDateTime.
    /// </summary>
    /// <param name="date">The Gregorian DateTime to convert.</param>
    /// <returns>An IslamicDateTime representing the converted date.</returns>
    public static IslamicDateTime ToIslamicDateTime(this DateTime date)
    {
        return Converter.ConvertToIslamic(date)
            .SetTime(TimeOnly.FromTimeSpan(date.TimeOfDay))
            .SetDayOfWeek((int)date.DayOfWeek);
    }
}