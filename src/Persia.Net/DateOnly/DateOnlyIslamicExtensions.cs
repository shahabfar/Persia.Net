namespace Persia.Net;

public static class DateOnlyIslamicExtensions
{
    /// <summary>
    /// Converts a <see cref="DateOnly"/> object to a <see cref="IslamicDateTime"/> object.
    /// </summary>
    /// <param name="dateOnly">The Gregorian DateTime to convert.</param>
    /// <returns>An <see cref="IslamicDateTime"/> object representing the converted date.</returns>
    public static IslamicDateTime ToIslamicDateTime(this DateOnly dateOnly)
    {
        var date = dateOnly.ToDateTime(TimeOnly.MinValue);

        return Converter.ConvertToIslamic(date)
            .SetDayOfWeek((int)date.DayOfWeek);
    }
}