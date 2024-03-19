using Persia.Net.Words;
using static Persia.Net.Constants.CalendarConstants;
using static Persia.Net.Constants.PersianCalendarConstants;

namespace Persia.Net.DateTimes;

public partial class PersianDateTime
{
    /// <summary>
    /// Gets the year component of the date.
    /// </summary>
    public int Year { get; private set; }

    /// <summary>
    /// Gets the month component of the date.
    /// </summary>
    public int Month { get; private set; }

    /// <summary>
    /// Gets the day component of the date.
    /// </summary>
    public int Day { get; private set; }

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
    /// Gets the days of the month.
    /// </summary>
    public int DaysInMonth { get; private set; }

    /// <summary>
    /// Gets the week number of the month.
    /// </summary>
    public int WeekOfMonth { get; private set; }

    /// <summary>
    /// Gets the week number of the year.
    /// </summary>
    public int WeekOfYear { get; private set; }

    /// <summary>
    /// Gets the name of current month.
    /// </summary>
    public string MonthName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the day name of the week in Persian Calendar.
    /// </summary>
    public string DayOfWeekName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the current Persian year is a leap year.
    /// </summary>
    public bool IsLeapYear { get; private set; }

    /// <summary>
    /// Gets the number of days that have passed since the beginning of the current year in the Persian calendar.
    /// </summary>
    public int DayOfYear { get; private set; }

    /// <summary>
    /// Gets the number of days remaining until the end of the current year in the Persian calendar.
    /// </summary>
    public int DaysRemainingInYear { get; private set; }

    /// <summary>
    /// Gets the current date and time in the Persian calendar.
    /// </summary>
    public static PersianDateTime Now => DateTime.Now.ToPersianDateTime();

    /// <summary>
    /// Gets the current date and time in the Persian calendar in Coordinated Universal Time (UTC).
    /// </summary>
    public static PersianDateTime UtcNow => DateTime.UtcNow.ToPersianDateTime();

    /// <summary>
    /// Gets the current date in the Persian calendar.
    /// </summary>
    public static PersianDateTime Today => DateTime.Today.ToPersianDateTime();

    /// <summary>
    /// Initializes a new instance of the <see cref="PersianDateTime"/> class.
    /// </summary>
    /// <param name="year">The year component of the Persian date.</param>
    /// <param name="month">The month component of the Persian date.</param>
    /// <param name="day">The day component of the Persian date.</param>
    /// <param name="time">The optional parameter for the time of the given day.</param>
    public PersianDateTime(int year, int month, int day, TimeOnly time = default)
    {
        var dt = ToDateTime(year, month, day);
        var dtPersian = dt.ToPersianDateTime();
        Year = dtPersian.Year;
        Month = dtPersian.Month;
        Day = dtPersian.Day;
        DayOfWeek = dtPersian.DayOfWeek;
        DaysInMonth = dtPersian.DaysInMonth;
        MonthName = dtPersian.MonthName;
        DayOfWeekName = dtPersian.DayOfWeekName;
        IsLeapYear = dtPersian.IsLeapYear;
        DayOfYear = dtPersian.DayOfYear;
        DaysRemainingInYear = dtPersian.DaysRemainingInYear;
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
        Millisecond = time.Millisecond;
    }

    internal PersianDateTime(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
        WeekOfMonth = GetWeekNumberOfMonth();
        IsLeapYear = CheckLeapYear(year);
    }

    internal PersianDateTime SetTime(TimeOnly time)
    {
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
        Millisecond = time.Millisecond;
        return this;
    }

    internal PersianDateTime SetTicks(long ticks)
    {
        Ticks = ticks;
        return this;
    }

    internal PersianDateTime SetDayOfWeek(int dayOfWeek)
    {
        DayOfWeek = (dayOfWeek + 1) % 7;
        DayOfWeekName = Weekdays[DayOfWeek];
        return this;
    }

    internal PersianDateTime SetDaysInMonth(int daysInMonth)
    {
        DaysInMonth = daysInMonth;
        MonthName = Months[Month - 1];
        return this;
    }

    internal PersianDateTime SetYearProgress(int dayOfYear, int daysRemainingInYear)
    {
        DayOfYear = dayOfYear;
        DaysRemainingInYear = daysRemainingInYear;
        WeekOfYear = (dayOfYear - 1) / 7 + 1;
        return this;
    }

    private int GetWeekNumberOfMonth()
    {
        // Subtract 1 because days of the month are 1-based, not 0-based
        var dayOfMonth = Day - 1;

        // Add the day of the week. This step is necessary to handle months that start in the middle of the week
        dayOfMonth += DayOfWeek;

        // Divide by 7 and add 1 to get the week number (1-based)
        return dayOfMonth / 7 + 1;
    }

    /// <summary>
    /// Determines whether the specified year is a leap year in the Persian calendar.
    /// </summary>
    /// <param name="year">The year to check.</param>
    /// <returns>true if the specified year is a leap year; otherwise, false.</returns>
    public static bool CheckLeapYear(int year)
    {
        // Group years into cycles of 2820 years each
        var yearInCycle = year % PersianCalendarGrandCycle;

        // Calculate the ordinal number of the year within the cycle
        var ordinalNumber = yearInCycle % YearsInSmallCycle;

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

    //ToOrdinalWords
    //ToClockNotation
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
    public string ToLongPersianOrdinalWords()
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
        return DateOnly.FromDateTime(Converter.ConvertToGregorian(year, month, day, 0, 0, 0));
    }

    /// <summary>
    /// Converts the value of this instance to a DateTime.
    /// </summary>
    /// <returns>
    /// A DateTime that is equivalent to the date and time represented by this instance.
    /// </returns>
    public DateTime ToDateTime()
    {
        return Converter.ConvertToGregorian(Year, Month, Day, Hour, Minute, Second, Millisecond);
    }

    /// <summary>
    /// Converts the value of this instance to a DateOnly.
    /// </summary>
    /// <returns>
    /// A DateOnly that is equivalent to the date represented by this instance.
    /// </returns>
    public DateOnly ToDateOnly()
    {
        return DateOnly.FromDateTime(Converter.ConvertToGregorian(Year, Month, Day, 0, 0, 0));
    }

    /// <summary>
    /// Adds the specified number of days to the value of this instance.
    /// </summary>
    /// <param name="days">The number of days to add.</param>
    /// <returns>
    /// A PersianDateTime that is the sum of the date and days.
    /// </returns>
    public PersianDateTime AddDays(int days)
    {
        return ToDateTime().AddDays(days).ToPersianDateTime();
    }

    /// <summary>
    /// Adds the specified number of months to the value of this instance.
    /// </summary>
    /// <param name="months">The number of months to add.</param>
    /// <returns>
    /// A PersianDateTime that is the sum of the date and months.
    /// </returns>
    public  PersianDateTime AddMonths(int months)
    {
        var totalMonths = Month + months;
        var year = Year + totalMonths / 12;
        var month = totalMonths % 12;

        if (month == 0)
        {
            year -= 1;
            month = 12;
        }

        return new PersianDateTime(year, month, Day, new TimeOnly(Hour, Minute, Second, Millisecond));
    }

    /// <summary>
    /// Converts the specified string representation of a date to its PersianDateTime equivalent.
    /// </summary>
    /// <param name="date">A string containing a date to convert.</param>
    /// <param name="systemClock">A boolean value that indicates whether to use the current system time. If true, the current system time is used; otherwise, the time is set to 00:00:00.</param>
    /// <returns>
    /// A PersianDateTime equivalent to the date contained in the input string.
    /// </returns>
    /// <exception cref="System.FormatException">Thrown when the input string is not in the correct format.</exception>
    public static PersianDateTime Parse(string date, bool systemClock = false)
    {
        var parts = date.ToLatinNumber().Split('/');
        if (parts.Length != 3)
            throw new FormatException("Invalid date format. Expected format is 'yyyy/MM/dd'.");

        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var day = int.Parse(parts[2]);

        return !systemClock 
            ? new PersianDateTime(year, month, day) 
            : new PersianDateTime(year, month, day, TimeOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    /// Tries to convert the specified string representation of a date to its PersianDateTime equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="date">A string containing a date to convert.</param>
    /// <param name="result">When this method returns, contains the PersianDateTime equivalent to the date contained in the input string, if the conversion succeeded, or null if the conversion failed. The conversion fails if the input string is not in the correct format, or represents a date that is not possible in the Persian calendar. This parameter is passed uninitialized.</param>
    /// <param name="systemClock">A boolean value that indicates whether to use the current system time. If true, the current system time is used; otherwise, the time is set to 00:00:00.</param>
    /// <returns>
    /// true if the input string was converted successfully; otherwise, false.
    /// </returns>
    public static bool TryParse(string date, out PersianDateTime result, bool systemClock = false)
    {
        var parts = date.ToLatinNumber().Split('/');
        if (parts.Length != 3)
        {
            result = null;
            return false;
        }

        if (!int.TryParse(parts[0], out var year) ||
            !int.TryParse(parts[1], out var month) ||
            !int.TryParse(parts[2], out var day))
        {
            result = null;
            return false;
        }

        result = !systemClock
            ? new PersianDateTime(year, month, day)
            : new PersianDateTime(year, month, day, TimeOnly.FromDateTime(DateTime.Now));

        return true;
    }
}