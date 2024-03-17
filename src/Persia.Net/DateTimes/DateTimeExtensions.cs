using Persia.Net.Enums;
using Persia.Net.Words;

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

        return date.Value.ToPersianDateTime();
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
            .SetDayOfWeek((int)date.DayOfWeek)
            .GetLeapYearStatus();
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format.
    /// </summary>
    /// <param name="dateTime">The date to calculate the date time difference from. If null, an ArgumentNullException is thrown.</param>
    /// <param name="partsToPrint">The number of time parts to include in the output string (e.g., if 3, it might print years, months, and days).</param>
    /// <returns>بعنوان مثال: ۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش</returns>
    public static string HumanizePersianDateTimePassed(this DateTime? dateTime, int partsToPrint = 3)
    {
        if (!dateTime.HasValue)
            throw new ArgumentNullException(nameof(dateTime));

        return dateTime.Value.HumanizePersianDateTimePassed(partsToPrint);
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format.
    /// </summary>
    /// <param name="dateTime">The date to calculate the date time difference from.</param>
    /// <param name="partsToPrint">The number of time parts to include in the output string (e.g., if 3, it might print years, months, and days).</param>
    /// <returns>بعنوان مثال: ۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش</returns>
    public static string HumanizePersianDateTimePassed(this DateTime dateTime, int partsToPrint = 3)
    {
        var now = DateTime.Now;
        var timeSpan = now - dateTime;
        var dtPersianNow = ToPersianDateTime(now);
        var dtPersian = ToPersianDateTime(dateTime);

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

        var hours = timeSpan.Hours;
        var minutes = timeSpan.Minutes;
        var seconds = timeSpan.Seconds;

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
            partsToPrint--;
        }
        if (hours > 0 && partsToPrint > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";
            result += $"{hours} ساعت";
            partsToPrint--;
        }
        if (minutes > 0 && partsToPrint > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";
            result += $"{minutes} دقیقه";
            partsToPrint--;
        }
        if (partsToPrint > 0)
        {
            if (!string.IsNullOrEmpty(result))
                result += " و ";
            result += $"{seconds} ثانیه";
        }

        return (result + " پیش").ToPersianString(true);
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format in Persian.
    /// </summary>
    /// <param name="dateTime">The date to calculate the date time difference from. If null, an ArgumentNullException is thrown.</param>
    /// <param name="timeUnit">The unit of time to be used for the calculation. It calculates the total of the given unit time and prints only that unit.</param>
    /// <returns>A string that represents the time difference in total calculated of given unit time in a human-readable format in Persian.</returns>
    public static string HumanizePersianDateTimePassed(this DateTime? dateTime, TimeUnit timeUnit)
    {
        if (!dateTime.HasValue)
            throw new ArgumentNullException(nameof(dateTime));

        return dateTime.Value.HumanizePersianDateTimePassed(timeUnit);
    }

    /// <summary>
    /// Converts the time difference between the current date and the provided date into a human-readable format in Persian.
    /// </summary>
    /// <param name="dateTime">The date to calculate the time difference from.</param>
    /// <param name="timeUnit">The unit of time to be used for the calculation. It calculates the total of the given unit time and prints only that unit.</param>
    /// <returns>A string that represents the time difference in total calculated of given unit time in a human-readable format in Persian.</returns>
    public static string HumanizePersianDateTimePassed(this DateTime dateTime, TimeUnit timeUnit)
    {
        var now = DateTime.Now;
        var dtPersianNow = ToPersianDateTime(now);
        var dtPersian = ToPersianDateTime(dateTime);

        var years = dtPersianNow.Year - dtPersian.Year;
        var months = years * 12 + dtPersianNow.Month - dtPersian.Month;
        var days = (int)(now - dateTime).TotalDays; // This is an approximation

        var result = "";

        if (timeUnit == TimeUnit.Years)
        {
            result = $"{years} سال";
        }
        else if (timeUnit == TimeUnit.Months)
        {
            result = $"{months} ماه";
        }
        else if (timeUnit == TimeUnit.Days)
        {
            result = $"{days} روز";
        }
        else if (timeUnit == TimeUnit.Hours)
        {
            result = $"{days * 24} ساعت"; // This is an approximation
        }
        else if (timeUnit == TimeUnit.Minutes)
        {
            result = $"{days * 24 * 60} دقیقه"; // This is an approximation
        }
        else if (timeUnit == TimeUnit.Seconds)
        {
            result = $"{days * 24 * 60 * 60} ثانیه"; // This is an approximation
        }

        return (result + " پیش").ToPersianString(true);
    }

}