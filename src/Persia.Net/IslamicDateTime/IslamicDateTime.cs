using static Persia.Net.IslamicCalendarConstants;
namespace Persia.Net;


public class IslamicDateTime
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
    /// Gets the day number of the week. This number is zero based.
    /// </summary>
    public int DayOfWeek { get; private set; }

    /// <summary>
    /// Gets the days of the month.
    /// </summary>
    public int DaysInMonth { get; private set; }

    /// <summary>
    /// Gets the name of current month.
    /// </summary>
    public string MonthName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the day name of the week in Persian Calendar.
    /// </summary>
    public string DayOfWeekName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the current date and time in the Persian calendar.
    /// </summary>
    //public static PersianDateTime Now => DateTime.Now.ToPersianDateTime();

    /// <summary>
    /// Gets the current date and time in the Persian calendar in Coordinated Universal Time (UTC).
    /// </summary>
    //public static PersianDateTime UtcNow => DateTime.UtcNow.ToPersianDateTime();

    /// <summary>
    /// Gets the current date in the Persian calendar.
    /// </summary>
    //public static PersianDateTime Today => DateTime.Today.ToPersianDateTime();

    /// <summary>
    /// Initializes a new instance of the <see cref="IslamicDateTime"/> class.
    /// </summary>
    /// <param name="year">The year component of the Islamic (hijri) date.</param>
    /// <param name="month">The month component of the Islamic (hijri) date.</param>
    /// <param name="day">The day component of the Islamic (hijri) date.</param>
    /// <param name="time">The optional parameter for the time of the given day.</param>
    public IslamicDateTime(int year, int month, int day, TimeOnly time = default)
    {
        var dt = ToDateTime(year, month, day);
        Year = year;
        Month = month;
        Day = day;
        SetDayOfWeek((int)dt.DayOfWeek);
        DaysInMonth = Converter.GetDaysInIslamicMonth(year, month);
        MonthName = Months[Month - 1]; 
        DayOfWeekName = Weekdays[DayOfWeek];
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
        Millisecond = time.Millisecond;
    }

    internal IslamicDateTime(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    internal IslamicDateTime SetTime(TimeOnly time)
    {
        Hour = time.Hour;
        Minute = time.Minute;
        Second = time.Second;
        Millisecond = time.Millisecond;
        return this;
    }

    internal IslamicDateTime SetDayOfWeek(int dayOfWeek)
    {
        DayOfWeek = (dayOfWeek + 1) % 7;
        DayOfWeekName = Weekdays[DayOfWeek];
        return this;
    }

    /// <summary>
    /// Converts the date to a string in the format "yyyy/MM/dd".
    /// </summary>
    /// <returns>برای مثال, 1445/09/17.</returns>
    public new string ToString()
    {
        return $"{Year}/{Month:D2}/{Day:D2}";
    }

    /// <summary>
    /// Converts the date to a string in the format "yyyy/MM/dd" with Arabic numbers.
    /// </summary>
    /// <returns>برای مثال, ١٤٤٥/٠٩/١٧.</returns>
    public string ToShortArabicString()
    {
        return ($"{Year}/{Month:D2}/{Day:D2}").ToArabicNumber();
    }

    /// <summary>
    /// Converts the date to a string in the Arabic format with Year and Day in digit and Month in word.
    /// </summary>
    /// <returns>برای مثال, ١٧ رمضان ١٤٤٥</returns>
    public string ToArabicString()
    {
        return ($"{Day} {Months[Month - 1]} {Year}").ToArabicNumber(true);
    }

    /// <summary>
    /// Converts the date to a string in the Arabic format with Year and Day in digit and Month in word as well as weekday name.
    /// </summary>
    /// <returns>برای مثال, الاربعا ١٧ رمضان ١٤٤٥</returns>
    public string ToLongArabicString(bool timeVisible = false)
    {
        return timeVisible
            ? ($"{Hour:D2}:{Minute:D2} {DayOfWeekName} {Day} {Months[Month - 1]} {Year}، ساعت ").ToArabicNumber()
            : ($"{DayOfWeekName} {Day} {Months[Month - 1]} {Year}").ToArabicNumber();
    }

    /// <summary>
    /// Converts an Islamic (Lunar Hijri) date and time to the equivalent Gregorian (Western) DateTime.
    /// </summary>
    /// <param name="year">The Islamic (Hijri) year.</param>
    /// <param name="month">The Islamic (Hijri) month (1 to 12).</param>
    /// <param name="day">The Islamic (Hijri) day of the month.</param>
    /// <param name="hour">The hour (0 to 23).</param>
    /// <param name="minute">The minute (0 to 59).</param>
    /// <param name="second">The second (0 to 59).</param>
    /// <param name="millisecond">The optional millisecond (0 to 999).</param>
    /// <returns>A DateTime object representing the converted date and time in the Gregorian calendar.</returns>
    public static DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
    {
        return Converter.ConvertIslamicToGregorian(year, month, day, hour, minute, second, millisecond);
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
        return Converter.ConvertIslamicToGregorian(year, month, day, timeOnly.Hour, timeOnly.Minute, timeOnly.Second, timeOnly.Millisecond);
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
        return DateOnly.FromDateTime(Converter.ConvertIslamicToGregorian(year, month, day, 0, 0, 0));
    }

    /// <summary>
    /// Converts the value of this instance to a DateTime.
    /// </summary>
    /// <returns>
    /// A DateTime that is equivalent to the date and time represented by this instance.
    /// </returns>
    public DateTime ToDateTime()
    {
        return Converter.ConvertIslamicToGregorian(Year, Month, Day, Hour, Minute, Second, Millisecond);
    }

    /// <summary>
    /// Converts the value of this instance to a DateOnly.
    /// </summary>
    /// <returns>
    /// A DateOnly that is equivalent to the date represented by this instance.
    /// </returns>
    public DateOnly ToDateOnly()
    {
        return DateOnly.FromDateTime(Converter.ConvertIslamicToGregorian(Year, Month, Day, 0, 0, 0));
    }

    /// <summary>
    /// Converts the specified string representation of a date to its IslamicDateTime equivalent.
    /// </summary>
    /// <param name="date">A string containing a date to convert. Expected format is 'yyyy/MM/dd'.</param>
    /// <param name="systemClock">A boolean value that indicates whether to use the current system time. If true, the current system time is used; otherwise, the time is set to 00:00:00.</param>
    /// <returns>
    /// An IslamicDateTime equivalent to the date contained in the input string.
    /// </returns>
    /// <exception cref="System.FormatException">Thrown when the input string is not in the correct format.</exception>
    public static IslamicDateTime Parse(string date, bool systemClock = false)
    {
        var parts = date.ToLatinNumber().Split('/');
        if (parts.Length != 3)
            throw new FormatException("Invalid date format. Expected format is 'yyyy/MM/dd'.");

        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var day = int.Parse(parts[2]);

        return !systemClock
            ? new IslamicDateTime(year, month, day)
            : new IslamicDateTime(year, month, day, TimeOnly.FromDateTime(DateTime.Now));
    }

    /// <summary>
    /// Tries to convert the specified string representation of a date to its IslamicDateTime equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="date">A string containing a date to convert. Expected format is 'yyyy/MM/dd'.</param>
    /// <param name="result">When this method returns, contains the IslamicDateTime equivalent to the date contained in the input string, if the conversion succeeded, or null if the conversion failed. The conversion fails if the input string is not in the correct format, or represents a date that is not possible in the Islamic calendar. This parameter is passed uninitialized.</param>
    /// <param name="systemClock">A boolean value that indicates whether to use the current system time. If true, the current system time is used; otherwise, the time is set to 00:00:00.</param>
    /// <returns>
    /// true if the input string was converted successfully; otherwise, false.
    /// </returns>
    public static bool TryParse(string date, out IslamicDateTime result, bool systemClock = false)
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
            ? new IslamicDateTime(year, month, day)
            : new IslamicDateTime(year, month, day, TimeOnly.FromDateTime(DateTime.Now));

        return true;
    }

    /// <summary>
    /// Converts an Islamic (Lunar Hijri) date and time to the equivalent Persian (Solar Hijri) date and time.
    /// </summary>
    /// <param name="year">The Islamic year.</param>
    /// <param name="month">The Islamic month (1 to 12).</param>
    /// <param name="day">The Islamic day of the month.</param>
    /// <param name="hour">The hour (0 to 23).</param>
    /// <param name="minute">The minute (0 to 59).</param>
    /// <param name="second">The second (0 to 59).</param>
    /// <param name="millisecond">The optional millisecond (0 to 999).</param>
    /// <returns>A PersianDateTime object representing the converted date and time in the Persian calendar.</returns>
    public static PersianDateTime ToPersianDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
    {
        var dtPersian = Converter.ConvertIslamicToPersian(year, month, day);
        dtPersian.SetTime(new TimeOnly(hour, minute, second, millisecond));
        return dtPersian;
    }

    /// <summary>
    /// Converts an Islamic (Lunar Hijri) date to the equivalent Persian (Solar Hijri) date.
    /// </summary>
    /// <param name="year">The Islamic year.</param>
    /// <param name="month">The Islamic month (1 to 12).</param>
    /// <param name="day">The Islamic day of the month.</param>
    /// <returns>A PersianDateTime object representing the converted date in the Persian calendar.</returns>
    public static PersianDateTime ToPersianDateOnly(int year, int month, int day)
    {
        return Converter.ConvertIslamicToPersian(year, month, day);
    }

    /// <summary>
    /// Converts the value of this instance to an Islamic date and time.
    /// </summary>
    /// <returns>
    /// An Islamic date and time that is equivalent to the date and time represented by this instance.
    /// </returns>
    public PersianDateTime ToPersianDateTime()
    {
        var dtIslamic = Converter.ConvertIslamicToPersian(Year, Month, Day);
        dtIslamic.SetTime(new TimeOnly(Hour, Minute, Second, Millisecond));
        return dtIslamic;
    }

    /// <summary>
    /// Converts the value of this instance to an Islamic date.
    /// </summary>
    /// <returns>
    /// An Islamic date that is equivalent to the date represented by this instance.
    /// </returns>
    public PersianDateTime ToPersianDateOnly()
    {
        return Converter.ConvertIslamicToPersian(Year, Month, Day);
    }
}