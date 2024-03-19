namespace Persia.Net.DateTimes;

public static class PersianDateTimeExtensions
{
    /// <summary>
    /// Adds the specified number of days to the value of this instance.
    /// </summary>
    /// <param name="date">The PersianDateTime instance.</param>
    /// <param name="days">The number of days to add.</param>
    /// <returns>
    /// A PersianDateTime that is the sum of the date and days.
    /// </returns>
    public static PersianDateTime AddDays(this PersianDateTime date, int days)
    {
        return date.ToDateTime().AddDays(days).ToPersianDateTime();
    }

    /// <summary>
    /// Adds the specified number of months to the value of this instance.
    /// </summary>
    /// <param name="date">The PersianDateTime instance.</param>
    /// <param name="months">The number of months to add.</param>
    /// <returns>
    /// A PersianDateTime that is the sum of the date and months.
    /// </returns>
    public static PersianDateTime AddMonths(this PersianDateTime date, int months)
    {
        var totalMonths = date.Month + months;
        var year = date.Year + totalMonths / 12;
        var month = totalMonths % 12;

        if (month == 0)
        {
            year -= 1;
            month = 12;
        }

        return new PersianDateTime(year, month, date.Day, new TimeOnly(date.Hour, date.Minute, date.Second, date.Millisecond));
    }

    /// <summary>
    /// Converts the value of this instance to a DateTime.
    /// </summary>
    /// <param name="date">The PersianDateTime instance.</param>
    /// <returns>
    /// A DateTime that is equivalent to the date and time represented by this instance.
    /// </returns>
    public static DateTime ToDateTime(this PersianDateTime date)
    {
        return Converter.ConvertToGregorian(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
    }

    /// <summary>
    /// Converts the value of this instance to a DateOnly.
    /// </summary>
    /// <param name="date">The PersianDateTime instance.</param>
    /// <returns>
    /// A DateOnly that is equivalent to the date represented by this instance.
    /// </returns>
    public static DateOnly ToDateOnly(this PersianDateTime date)
    {
        return DateOnly.FromDateTime(Converter.ConvertToGregorian(date.Year, date.Month, date.Day, 0, 0, 0));
    }
}