namespace Persia.Net;

public static class StringExtensions
{
    /// <summary>
    /// Converts a string representation of a date to its PersianDateTime equivalent.
    /// </summary>
    /// <param name="date">A string containing a date to convert. Expected format is 'yyyy/MM/dd' in Gregorian.</param>
    /// <param name="useSystemTime">If true, uses the current system time; otherwise, sets time to 00:00:00.</param>
    /// <returns>A PersianDateTime equivalent to the date contained in the input string.</returns>
    /// <exception cref="FormatException">Thrown when the input string is not in the correct format.</exception>
    public static PersianDateTime ToPersianDateTime(this string date, bool useSystemTime = false)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentException("Date string cannot be null or empty.", nameof(date));

        if (!DateTime.TryParseExact(date, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
            throw new FormatException("Invalid date format. Expected format is 'yyyy/MM/dd'.");

        var time = useSystemTime ? DateTime.Now.TimeOfDay : TimeSpan.Zero;
        var dateTime = parsedDate.Add(time);

        return dateTime.ToPersianDateTime();
    }

    /// <summary>
    /// Tries to convert a string representation of a date to its PersianDateTime equivalent.
    /// </summary>
    /// <param name="date">A string containing a date to convert. Expected format is 'yyyy/MM/dd' in Gregorian.</param>
    /// <param name="result">When this method returns, contains the PersianDateTime equivalent to the date contained in the input string, if the conversion succeeded, or null if the conversion failed. This parameter is passed uninitialized.</param>
    /// <param name="useSystemTime">If true, uses the current system time; otherwise, sets time to 00:00:00.</param>
    /// <returns>true if the input string was converted successfully; otherwise, false.</returns>
    public static bool TryToPersianDateTime(this string date, out PersianDateTime? result, bool useSystemTime = false)
    {
        try
        {
            result = date.ToPersianDateTime(useSystemTime);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}
