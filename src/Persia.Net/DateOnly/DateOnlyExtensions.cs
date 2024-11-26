using Persia.Net.Core;

namespace Persia.Net;

public static class DateOnlyExtensions
{
    /// <summary>
    /// Converts a <see cref="DateOnly"/> object to a <see cref="PersianDateTime"/> object.
    /// </summary>
    /// <param name="dateOnly">The <see cref="DateOnly"/> object to convert.</param>
    /// <returns>A <see cref="PersianDateTime"/> object that represents the converted date from the <see cref="DateOnly"/> object.</returns>
    public static PersianDateTime ToPersianDateTime(this DateOnly dateOnly)
    {
        var date = dateOnly.ToDateTime(TimeOnly.MinValue);

        return Converter.ConvertToPersian(date)
            .SetDayOfWeek((int)date.DayOfWeek);
    }

    /// <summary>
    /// Converts the date difference between the current date and the provided date into a human-readable format.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the date difference from.</param>
    /// <param name="partsToPrint">The number of time parts to include in the output string (e.g., if 3, it might print years, months, and days).</param>
    /// <returns>بعنوان مثال: ۱۱ ماه و ۳۶۲ روز پیش</returns>
    public static string? HumanizePassedPersianDateTime(this DateOnly dateOnly, int partsToPrint = 3)
    {
        var dtPersianNow = ToPersianDateTime(DateOnly.FromDateTime(DateTime.Now));
        var dtPersian = ToPersianDateTime(dateOnly);

        var years = dtPersianNow.Year - dtPersian.Year;
        var months = dtPersianNow.Month - dtPersian.Month;
        var days = dtPersianNow.DayOfYear - dtPersian.DayOfYear;

        if (days < 0)
        {
            months--;
            days += 30; // Approximation, can be 29, 30 or 31 depending on the month
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        var result = "";

        if (years > 0 && partsToPrint > 0)
        {
            result = $"{years} سال";
            partsToPrint--;
        }
        if (months > 0 && partsToPrint > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";
            result += $"{months} ماه";
            partsToPrint--;
        }
        if (days > 1 && partsToPrint > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";
            result += $"{days} روز";
        }

        return (result + " پیش").ToPersianString(true);
    }

    /// <summary>
    /// Converts the date difference between the current date and the provided date into a human-readable format in Persian.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the time difference from.</param>
    /// <param name="timeUnit">The unit of time to be used for the calculation. It calculates the total of the given unit time and prints only that unit.</param>
    /// <returns>بعنوان مثال: ۲ روز پیش</returns>
    public static string? HumanizePassedPersianDateTime(this DateOnly dateOnly, TimeUnit timeUnit)
    {
        var now = DateTime.Now;
        var dtPersianNow = ToPersianDateTime(DateOnly.FromDateTime(now));
        var dtPersian = ToPersianDateTime(dateOnly);

        var years = dtPersianNow.Year - dtPersian.Year;
        var months = years * 12 + dtPersianNow.Month - dtPersian.Month;
        var days = (int)(now - dateOnly.ToDateTime(TimeOnly.MinValue)).TotalDays;

        var result = "";

        if (timeUnit == TimeUnit.Years)
            result = $"{years} سال";
        else if (timeUnit == TimeUnit.Months)
            result = $"{months} ماه";
        else if (timeUnit == TimeUnit.Days)
            result = $"{days} روز";

        return (result + " پیش").ToPersianString(true);
    }

    /// <summary>
    /// Calculates the next day from the given date and returns it as a <see cref="PersianDateTime"/>.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the next day from.</param>
    /// <returns>A <see cref="PersianDateTime"/> representing the next day.</returns>
    public static PersianDateTime NextPersianDay(this DateOnly dateOnly)
    {
        return Converter.ConvertToPersian(dateOnly.AddDays(1))
            .SetDayOfWeek((int)dateOnly.DayOfWeek);
    }

    /// <summary>
    /// Calculates the Previous day from the given date and returns it as a PersianDateTime.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the Previous day from.</param>
    /// <returns>A <see cref="PersianDateTime"/> representing the Previous day.</returns>
    public static PersianDateTime PreviousPersianDay(this DateOnly dateOnly)
    {
        return Converter.ConvertToPersian(dateOnly.AddDays(-1))
            .SetDayOfWeek((int)dateOnly.DayOfWeek);
    }

    /// <summary>
    /// Calculates the dates of the current Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the current Persian week.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the current Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the week.
    /// </remarks>
    public static PersianDateTime[] CurrentPersianWeek(this DateOnly dateOnly)
    {
        // Here the week starts with Saturday (DayOfWeek.Saturday is 6 in .NET)
        var startOfWeek = dateOnly.AddDays(-((int)dateOnly.DayOfWeek + 1) % 7);
        var week = new PersianDateTime[7];

        for (var i = 0; i < 7; i++)
            week[i] = startOfWeek.AddDays(i).ToPersianDateTime();

        return week;
    }

    /// <summary>
    /// Calculates the dates of the next Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the next Persian week.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the next Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the next week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the next week.
    /// </remarks>
    public static PersianDateTime[] NextPersianWeek(this DateOnly dateOnly)
    {
        // Here the week starts with Saturday (DayOfWeek.Saturday is 6 in .NET)
        var startOfNextWeek = dateOnly.AddDays(-((int)dateOnly.DayOfWeek + 1) % 7 + 7);
        var week = new PersianDateTime[7];

        for (var i = 0; i < 7; i++)
            week[i] = startOfNextWeek.AddDays(i).ToPersianDateTime();

        return week;
    }

    /// <summary>
    /// Calculates the dates of the Previous Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the Previous Persian week.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the Previous Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the Previous week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the Previous week.
    /// </remarks>
    public static PersianDateTime[] PreviousPersianWeek(this DateOnly dateOnly)
    {
        // Here the week starts with Saturday (DayOfWeek.Saturday is 6 in .NET)
        var startOfNextWeek = dateOnly.AddDays(-((int)dateOnly.DayOfWeek + 1) % 7 - 7);
        var week = new PersianDateTime[7];

        for (var i = 0; i < 7; i++)
            week[i] = startOfNextWeek.AddDays(i).ToPersianDateTime();

        return week;
    }

    /// <summary>
    /// Calculates the dates of the current Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the current Persian month.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the current Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the month, starting from the first day of the month.
    /// </remarks>
    public static PersianDateTime[] CurrentPersianMonth(this DateOnly dateOnly)
    {
        var dtPersian = ToPersianDateTime(dateOnly);

        // Create an array to hold the days of the current month
        var month = new PersianDateTime[dtPersian.DaysInMonth];

        for (var i = 0; i < dtPersian.DaysInMonth; i++)
            month[i] = new PersianDateTime(dtPersian.Year, dtPersian.Month, i + 1, TimeOnly.MinValue);

        return month;
    }

    /// <summary>
    /// Calculates the dates of the next Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the next Persian month.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the next Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the next month, starting from the first day of the next month.
    /// </remarks>
    public static PersianDateTime[] NextPersianMonth(this DateOnly dateOnly)
    {
        var dtPersian = dateOnly.ToPersianDateTime();

        // Add one month to the PersianDateTime
        dtPersian = dtPersian.Month + 1 == 13
            ? new PersianDateTime(dtPersian.Year + 1, 1, 1, TimeOnly.MinValue)
            : new PersianDateTime(dtPersian.Year, dtPersian.Month + 1, 1, TimeOnly.MinValue);

        // Create an array to hold the days of the next month
        var nextMonth = new PersianDateTime[dtPersian.DaysInMonth];

        for (var i = 0; i < dtPersian.DaysInMonth; i++)
            nextMonth[i] = new PersianDateTime(dtPersian.Year, dtPersian.Month, i + 1, TimeOnly.MinValue);

        return nextMonth;
    }

    /// <summary>
    /// Calculates the dates of the Previous Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the Previous Persian month.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the Previous Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the Previous month, starting from the first day of the Previous month.
    /// </remarks>
    public static PersianDateTime[] PreviousPersianMonth(this DateOnly dateOnly)
    {
        // Convert the date to PersianDateTime
        var dtPersian = dateOnly.ToPersianDateTime();

        // Subtract one month from the PersianDateTime
        dtPersian = dtPersian.Month - 1 == 0
            ? new PersianDateTime(dtPersian.Year - 1, 12, 1, TimeOnly.MinValue)
            : new PersianDateTime(dtPersian.Year, dtPersian.Month - 1, 1, TimeOnly.MinValue);

        // Create an array to hold the days of the Previous month
        var nextMonth = new PersianDateTime[dtPersian.DaysInMonth];

        for (var i = 0; i < dtPersian.DaysInMonth; i++)
            nextMonth[i] = new PersianDateTime(dtPersian.Year, dtPersian.Month, i + 1, TimeOnly.MinValue);

        return nextMonth;
    }
}