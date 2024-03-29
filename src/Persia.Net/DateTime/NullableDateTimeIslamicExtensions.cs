namespace Persia.Net;

public static class NullableDateTimeIslamicExtensions
{
    /// <summary>
    /// Converts a Gregorian DateTime to a IslamicDateTime.
    /// </summary>
    /// <param name="date">The Gregorian DateTime to convert (nullable). If null, an <see cref="ArgumentNullException"/> is thrown</param>
    /// <returns>An IslamicDateTime representing the converted date.</returns>
    public static IslamicDateTime ToIslamicDateTime(this DateTime? date)
    {
        if (!date.HasValue)
            throw new ArgumentNullException(nameof(date));
        return date.Value.ToIslamicDateTime();
    }
}