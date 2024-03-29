using System;

namespace Persia.Net;

public static class NullableDateOnlyExtensions
{
    /// <summary>
    /// Converts a <see cref="DateOnly"/> object to a <see cref="PersianDateTime"/> object.
    /// </summary>
    /// <param name="dateOnly">The <see cref="DateOnly"/> to convert (nullable). If null, an  <see cref="ArgumentNullException"/> is thrown</param>
    /// <returns>A <see cref="PersianDateTime"/> representing the converted date.</returns>
    public static PersianDateTime ToPersianDateTime(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.ToPersianDateTime();
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the date difference from. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <param name="partsToPrint">The number of time parts to include in the output string (e.g., if 3, it might print years, months, and days).</param>
    /// <returns>بعنوان مثال: ۱۱ ماه و ۳۶۲ روز پیش</returns>
    public static string HumanizePassedPersianDateTime(this DateOnly? dateOnly, int partsToPrint = 3)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.HumanizePassedPersianDateTime();
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format in Persian.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the date time difference from. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <param name="timeUnit">The unit of time to be used for the calculation. It calculates the total of the given unit time and prints only that unit.</param>
    /// <returns>A string that represents the time difference in total calculated of given unit time in a human-readable format in Persian.</returns>
    public static string HumanizePassedPersianDateTime(this DateOnly? dateOnly, TimeUnit timeUnit)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.HumanizePassedPersianDateTime(timeUnit);
    }

    /// <summary>
    /// Calculates the next day from the given date and returns it as a <see cref="PersianDateTime"/>.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the next day from. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>A <see cref="PersianDateTime"/> representing the next day.</returns>
    public static PersianDateTime NextPersianDay(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.NextPersianDay();
    }

    /// <summary>
    /// Calculates the Previous day from the given date and returns it as a PersianDateTime.
    /// </summary>
    /// <param name="dateOnly">The date to calculate the Previous day from. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>A <see cref="PersianDateTime"/> representing the Previous day.</returns>
    public static PersianDateTime PreviousPersianDay(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.PreviousPersianDay();
    }

    /// <summary>
    /// Calculates the dates of the current Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the current Persian week. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the current Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the week.
    /// </remarks>
    public static PersianDateTime[] CurrentPersianWeek(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.CurrentPersianWeek();
    }

    /// <summary>
    /// Calculates the dates of the next Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the next Persian week. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the next Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the next week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the next week.
    /// </remarks>
    public static PersianDateTime[] NextPersianWeek(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.NextPersianWeek();
    }

    /// <summary>
    /// Calculates the dates of the Previous Persian week for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the Previous Persian week. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the Previous Persian week.</returns>
    /// <remarks>
    /// This method calculates the start of the Previous week based on the Persian calendar, where the week starts on Saturday. 
    /// It then creates a new <see cref="PersianDateTime"/> for each day of the week, starting from the start of the Previous week.
    /// </remarks>
    public static PersianDateTime[] PreviousPersianWeek(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.PreviousPersianWeek();
    }

    /// <summary>
    /// Calculates the dates of the current Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the current Persian month. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the current Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the month, starting from the first day of the month.
    /// </remarks>
    public static PersianDateTime[] CurrentPersianMonth(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.CurrentPersianMonth();
    }

    /// <summary>
    /// Calculates the dates of the next Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the next Persian month. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the next Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the next month, starting from the first day of the next month.
    /// </remarks>
    public static PersianDateTime[] NextPersianMonth(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.NextPersianMonth();
    }

    /// <summary>
    /// Calculates the dates of the Previous Persian month for a given date.
    /// </summary>
    /// <param name="dateOnly">The date for which to calculate the Previous Persian month. If null, an <see cref="ArgumentNullException"/> is thrown.</param>
    /// <returns>An array of <see cref="PersianDateTime"/> objects representing each day of the Previous Persian month.</returns>
    /// <remarks>
    /// This method first converts the given date to a Persian date. It then creates a new <see cref="PersianDateTime"/> for each day of the Previous month, starting from the first day of the Previous month.
    /// </remarks>
    public static PersianDateTime[] PreviousPersianMonth(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));

        return dateOnly.Value.PreviousPersianMonth();
    }
}