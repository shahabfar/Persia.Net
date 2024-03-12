using Persia.Net.Words;
using static Persia.Net.Constants.PersianCalendarConstants;

namespace Persia.Net.DateTimes;

public class PersianDateTime(int year, int month, int day)
{
    /// <summary>
    /// Gets the year component of the date.
    /// </summary>
    public int Year { get; } = year;

    /// <summary>
    /// Gets the month component of the date.
    /// </summary>
    public int Month { get; } = month;

    /// <summary>
    /// Gets the day component of the date.
    /// </summary>
    public int Day { get; } = day;

    /// <summary>
    /// Gets the hour component of the time.
    /// </summary>
    public int Hour { get; private set; }

    /// <summary>
    /// Gets the minute component of the time.
    /// </summary>
    public int Minute { get; private set; }

    /// <summary>
    /// Gets the second component of the time.
    /// </summary>
    public int Second { get; private set; }

    /// <summary>
    /// Gets the millisecond component of the time.
    /// </summary>
    public int Millisecond { get; private set; }

    /// <summary>
    /// Gets the number of ticks that represent the time.
    /// </summary>
    public long Ticks { get; private set; }

    /// <summary>
    /// Gets the day number of the week. This number is zero based.
    /// </summary>
    public int DayOfWeek { get; private set; }

    /// <summary>
    /// Gets the day name of the week in Persian Calendar.
    /// </summary>
    public string DayOfWeekName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the current Persian year is a leap year.
    /// </summary>
    public bool IsLeapYear => GetLeapYearStatus();

    /// <summary>
    /// Gets the cycle number within the 2820-year Persian calendar cycle.
    /// </summary>
    public int CycleNumber { get; private set; }

    /// <summary>
    /// Gets the length of the current cycle (either 84 or 37 years) within the 2820-year cycle.
    /// </summary>
    public int CycleLength { get; private set; }


    internal PersianDateTime SetTime(TimeOnly time)
    {
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
        Millisecond = time.Millisecond;
        Ticks = time.Ticks;
        return this;
    }

    internal PersianDateTime SetDayOfWeek(int dayOfWeek)
    {
        DayOfWeek = (dayOfWeek + 1) % 7;
        DayOfWeekName = Weekdays[DayOfWeek];
        return this;
    }

    private bool GetLeapYearStatus()
    {
        // Group years into cycles of 2820 years each
        CycleNumber = Year / 2820;
        var yearInCycle = Year % 2820;

        // Determine the cycle length (84 cycles of 29 + 33 + 33 + 33 years, and 4 cycles of 29 + 33 + 33 + 37 years)
        int[] cycleLengths = [29, 33, 33, 33, 29, 33, 33, 33];
        CycleLength = cycleLengths[yearInCycle / 120] + (yearInCycle % 120 == 119 ? 37 : 0);

        // Calculate the ordinal number of the year within the cycle
        var ordinalNumber = yearInCycle % 120;

        // Check if the year is evenly divisible by 4
        var isLeapYear = ordinalNumber % 4 == 0;

        return isLeapYear;
    }

    /// <summary>
    /// Converts the date to a string in the format "yyyy/MM/dd".
    /// </summary>
    /// <returns>برای مثال, 1402/12/20.</returns>
    public new string ToString()
    {
        return $"{Year}/{Month:D2}/{Day:D2}";
    }

    /// <summary>
    /// Converts the date to a string in the format "yyyy/MM/dd" with Persian numbers.
    /// </summary>
    /// <returns>برای مثال, ۱۴۰۲/۱۲/۲۰.</returns>
    public string ToShortPersianString()
    {
        return ($"{Year}/{Month:D2}/{Day:D2}").ToPersianString();
    }

    /// <summary>
    /// Converts the date to a string in the Persian format with Year and Day in digit and Month in word.
    /// </summary>
    /// <returns>برای مثال, ۲۰ اسفند ۱۴۰۲</returns>
    public string ToPersianString()
    {
        return ($"{Day} {Months[Month - 1]} {Year}").ToPersianString(true);
    }

    /// <summary>
    /// Converts the date to a string in the Persian format with Year and Day in digit and Month in word as well as weekday name and time.
    /// </summary>
    /// <returns>برای مثال, یکشنبه ۲۰ اسفند ۱۴۰۲</returns>
    public string ToLongPersianString(bool timeVisible = false)
    {
        return timeVisible
            ? ($"{Hour:D2}:{Minute:D2} {DayOfWeekName} {Day} {Months[Month - 1]} {Year}، ساعت ").ToPersianString()
            : ($"{DayOfWeekName} {Day} {Months[Month - 1]} {Year}").ToPersianString();
    }

    /// <summary>
    /// Converts the date to a string in the Persian format with Year in digit and Day and Month in words.
    /// </summary>
    /// <returns>برای مثال, بیستم اسفند ۱۴۰۲</returns>
    public string ToPersianDayMonthString()
    {
        return ($"{Year} {Days[Day - 1]} {Months[Month - 1]}").ToPersianString();
    }

    /// <summary>
    /// Converts the date to a string in the Persian as well as week day name.
    /// </summary>
    /// <returns>برای مثال, یکشنبه ۱۴۰۲/۱۲/۲۰</returns>
    public string ToPersianWeekdayString()
    {
        return ($"{Year}/{Month:D2}/{Day:D2} " + DayOfWeekName).ToPersianString();
    }

    /// <summary>
    /// Converts the date to a string in the Persian format with Year in digit and Day and Month in words as well as weekday name.
    /// </summary>
    /// <returns>برای مثال, یکشنبه بیستم اسفند ۱۴۰۲</returns>
    public string ToLongPersianWeekday()
    {
        return $"{DayOfWeekName} {Days[Day - 1]} {Months[Month - 1]} {Year.ToPersianString()}";
    }

    /// <summary>
    /// Converts a Persian (Solar Hijri) date and time to the equivalent Gregorian (Western) DateTime.
    /// </summary>
    /// <param name="year">The Persian year.</param>
    /// <param name="month">The Persian month (1 to 12).</param>
    /// <param name="day">The Persian day of the month.</param>
    /// <param name="hour">The hour (0 to 23).</param>
    /// <param name="minute">The minute (0 to 59).</param>
    /// <param name="second">The second (0 to 59).</param>
    /// <param name="millisecond">The optional millisecond (0 to 999).</param>
    /// <returns>A DateTime object representing the converted date and time in the Gregorian calendar.</returns>
    public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
    {
        return Converter.ConvertToGregorian(year, month, day, hour, minute, second, millisecond);
    }

    /// <summary>
    /// Converts a Persian (Solar Hijri) date (year, month, and day) to the equivalent Gregorian (Western) DateTime.
    /// The current time (hour, minute, second, and millisecond) is obtained from the system clock.
    /// </summary>
    /// <param name="year">The Persian year.</param>
    /// <param name="month">The Persian month (1 to 12).</param>
    /// <param name="day">The Persian day of the month.</param>
    /// <returns>A DateTime object representing the converted date and time in the Gregorian calendar.</returns>
    public static DateTime ToDateTime(int year, int month, int day)
    {
        var timeOnly = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay);
        return Converter.ConvertToGregorian(year, month, day, timeOnly.Hour, timeOnly.Minute, timeOnly.Second, timeOnly.Millisecond);
    }

    /// <summary>
    /// Converts a Persian (Solar Hijri) date to the equivalent DateOnly (date without time) in the Gregorian calendar.
    /// </summary>
    /// <param name="year">The Persian year.</param>
    /// <param name="month">The Persian month (1 to 12).</param>
    /// <param name="day">The Persian day of the month.</param>
    /// <returns>A DateOnly object representing the converted date.</returns>
    public static DateOnly ToDateOnly(int year, int month, int day)
    {
        return DateOnly.FromDateTime(Converter.ConvertToGregorian(year, month, day, 0, 0, 0, 0));
    }

}