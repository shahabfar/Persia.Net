namespace Persia.Net.Core;

using static CalendarConstants;

internal static class Converter
{
    /// <summary>
    ///     Converts a Gregorian date to a Persian date.
    /// </summary>
    /// <param name="date">The Gregorian date to be converted.</param>
    /// <returns>A PersianDateTime object representing the converted Persian date.</returns>
    internal static PersianDateTime ConvertToPersian(DateTime date)
    {
        return ConvertToPersian(new DateOnly(date.Year, date.Month, date.Day));
    }

    /// <summary>
    ///     Converts a Gregorian date to a Persian date.
    /// </summary>
    /// <param name="date">The Gregorian date to be converted.</param>
    /// <returns>A PersianDateTime object representing the converted Persian date.</returns>
    internal static PersianDateTime ConvertToPersian(DateOnly date)
    {
        // Convert the Gregorian date to Julian Day Number
        var jd = GregorianToJulianDay(date.Year, date.Month, date.Day);

        // Convert the Julian Day Number to a Persian date
        var persianDate = JulianDayToPersian(jd);

        // Get the progress of the year for the Persian date
        var (dayOfYear, daysRemainingInYear) = GetYearProgress(persianDate.Year, date);

        persianDate.SetYearProgress(dayOfYear, daysRemainingInYear);
        var daysInMonth = GetDaysInPersianMonth(persianDate.Year, persianDate.Month);
        persianDate.SetDaysInMonth(daysInMonth);
        var ticks = CalculatePersianTicks(persianDate);
        persianDate.SetTicks(ticks);
        return persianDate;
    }

    internal static DateTime ConvertToGregorian(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
    {
        var maxDays = GetDaysInPersianMonth(year, month);
        if (day > maxDays)
            throw new ArgumentException($"Day {day} is invalid for month {month} in year {year} (max {maxDays} days).");

        //var jd = persian_to_jd(year, month, day);
        var jd = PersianToJulianDay(year, month, day);
        var dateOnly = JulianDayToGregorian(jd);
        return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, hour, minute, second, millisecond);
    }

    /// <summary>
    ///     Converts a Gregorian date to a Julian Day number.
    /// </summary>
    /// <param name="year">The year part of the Gregorian date.</param>
    /// <param name="month">The month part of the Gregorian date.</param>
    /// <param name="day">The day part of the Gregorian date.</param>
    /// <returns>The Julian Day number corresponding to the input Gregorian date.</returns>
    private static double GregorianToJulianDay(int year, int month, int day)
    {
        double yearsBefore = year - 1;
        var leapYears = Math.Floor(yearsBefore / LeapYearFrequency) - Math.Floor(yearsBefore / Century) + Math.Floor(yearsBefore / LeapYearCycle);
        var monthFactor = Math.Floor((MonthFactorMultiplier * month - MonthFactorSubtractor) / 12);
        var leapCorrection = month <= 2 ? 0 : IsGregorianLeapYear(year) ? -1 : -2;
        var julianDay = GregorianEpoch - 1 + DaysInYear * yearsBefore + leapYears + monthFactor + leapCorrection + day;
        return julianDay;
    }

    /// <summary>
    ///     Converts a Julian Day number to a Persian date.
    /// </summary>
    /// <param name="julianDay">The Julian Day number to be converted.</param>
    /// <returns>A PersianDateOnly object representing the converted Persian date.</returns>
    private static PersianDateTime JulianDayToPersian(double julianDay)
    {
        // shift to midnight and strip fraction
        var Z = Math.Floor(julianDay + 0.5);

        // days since the Persian civil epoch (midnight 1 Farvardin 1, JDN 1948319.5)
        var days = Z - 1_948_319.5;

        // how many complete 33-year cycles?
        var cycles = (long)(days / (33 * DaysInYear + 8));
        days -= cycles * (33 * DaysInYear + 8);

        // break out the year‐within‐cycle
        //    We need the smallest y such that days < y*365 + leapDaysBefore(y)
        var yearInCycle = 0;
        for (var i = 0; i < 33; i++)
        {
            var leapDays = LeapIndices.Count(r => r <= i);
            if (i * DaysInYear + leapDays > days)
                break;
            yearInCycle = i;
        }

        days -= yearInCycle * DaysInYear + LeapIndices.Count(r => r <= yearInCycle);

        var year = (int)(cycles * 33 + yearInCycle + 1);

        // now days is the day‐of‐year (0‐based)
        var dayOfYear = (int)days;   // 0 … 365

        // figure out month and day
        var month = dayOfYear < DaysInHalfYear
            ? dayOfYear / DaysInFirstHalfMonth + 1
            : (dayOfYear - DaysInHalfYear) / DaysInSecondHalfMonth + 7;

        var day = dayOfYear
                  - (month <= FirstHalfMonths
                      ? (month - 1) * DaysInFirstHalfMonth
                      : FirstHalfMonths * DaysInFirstHalfMonth + (month - 7) * DaysInSecondHalfMonth)
                  + 1;

        return new PersianDateTime(year, month, day);
    }

    private static readonly int[] LeapIndices = [1, 5, 9, 13, 17, 22, 26, 30];

    /// <summary>
    ///     Converts a Persian date to a Julian Day number.
    /// </summary>
    /// <param name="year">The year part of the Persian date.</param>
    /// <param name="month">The month part of the Persian date.</param>
    /// <param name="day">The day part of the Persian date.</param>
    /// <returns>The Julian Day number corresponding to the input Persian date.</returns>
    private static double PersianToJulianDay(int year, int month, int day)
    {
        long y = year - 1;                    //  year 1 starts the count
        var cycles33 = y / 33;                      //  complete 33‑year cycles
        var yearInCycle = (int)(y % 33);               //  0 … 32 inside the cycle

        //  8 leap years in each 33‑year cycle
        var days = cycles33 * (33 * DaysInYear + 8);

        // days in the incomplete cycle before this year
        var leapDaysInCycle = LeapIndices.TakeWhile(idx => idx <= yearInCycle).Count();
        days += yearInCycle * DaysInYear + leapDaysInCycle;

        // days contributed by complete months before <month> 
        if (month <= FirstHalfMonths)
            days += (month - 1) * DaysInFirstHalfMonth;         // Farvardin … Shahrivar
        else
            days += FirstHalfMonths * DaysInFirstHalfMonth + (month - 7) * DaysInSecondHalfMonth;     // Mehr … Esfand 

        // add the day number and shift to the epoch 
        days += day - 1;

        // Persian civil epoch: midnight that begins 1 Farvardin 1  (JD 1948319.5)
        return days + 1_948_319.5;
    }

    /// <summary>
    ///     Determines if a year in the Gregorian calendar is a leap year.
    /// </summary>
    /// <param name="year">The year to check.</param>
    /// <returns>True if the year is a leap year in the Gregorian calendar; otherwise, false.</returns>
    private static bool IsGregorianLeapYear(int year)
    {
        return year % LeapYearFrequency == 0 && !(year % Century == 0 && year % LeapYearCycle != 0);
    }

    /// <summary>
    ///     Converts a Julian Day number to a Gregorian date.
    /// </summary>
    /// <param name="julianDay">The Julian Day number to be converted.</param>
    /// <returns>A DateOnly object representing the converted Gregorian date.</returns>
    private static DateOnly JulianDayToGregorian(double julianDay)
    {
        // shift to midnight and strip the fraction
        var Z = Math.Floor(julianDay + 0.5);
        var A = Z;

        var alpha = Math.Floor((A - GregorianReformPivot) / DaysPerGregorianCentury);
        A += 1 + alpha - Math.Floor(alpha / 4);

        var B = A + JulianConversionBaseOffset;
        var C = Math.Floor((B - JulianYearOffset) / JulianYearDays);
        var D = Math.Floor(JulianYearDays * C);
        var E = Math.Floor((B - D) / MonthExtractionFactor);

        var day = (int)(B - D - Math.Floor(MonthExtractionFactor * E));
        var month = (int)(E < 14 ? E - 1 : E - 13);
        var year = (int)(month > 2 ? C - YearOffsetAfterFebruary : C - YearOffsetBeforeMarch);

        return new DateOnly(year, month, day);
    }

    /// <summary>
    /// Calculates the progress of the year in the Persian calendar for a given date.
    /// </summary>
    /// <param name="year">The year in the Persian calendar.</param>
    /// <param name="date">The date for which to calculate the year progress.</param>
    /// <returns>A tuple containing the day of the year and the number of days remaining in the year.</returns>
    private static (int DayOfYear, int DaysRemainingInYear) GetYearProgress(int year, DateOnly date)
    {
        // Calculate the JDN (Julian Day Number) for the start and end of the year
        var jdnStartOfYear = PersianToJulianDay(year, 1, 1);
        var jdnEndOfYear = PersianToJulianDay(year + 1, 1, 1) - 1;

        // Calculate the JDN for the given date
        var jdn = GregorianToJulianDay(date.Year, date.Month, date.Day);

        var DayOfYear = (int)(jdn - jdnStartOfYear + 1);
        var DaysRemainingInYear = (int)(jdnEndOfYear - jdn);

        return (DayOfYear, DaysRemainingInYear);
    }

    /// <summary>
    /// Calculates the number of days in a given month of a given year in the Persian calendar.
    /// </summary>
    /// <param name="year">The year in the Persian calendar.</param>
    /// <param name="month">The month in the Persian calendar.</param>
    /// <returns>The number of days in the specified month of the specified year in the Persian calendar.</returns>
    private static int GetDaysInPersianMonth(int year, int month)
    {
        if (month is < 1 or > 12)
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12.");

        return month <= 6 ? 31 :
            month <= 11 ? 30 :
            IsPersianLeapYear(year) ? 30 : 29;
    }

    private static bool IsPersianLeapYear(int year)
    {
        if (year <= 0)
            year--;

        // Calculate the position within the 33-year cycle
        var positionInCycle = year % 33;

        // Define the standard leap year positions within the cycle
        int[] leapYearPositions = [1, 5, 9, 13, 17, 22, 26, 30];

        // Check if the position corresponds to a leap year
        return leapYearPositions.Contains(positionInCycle);
    }

    /// <summary>
    /// Calculates the number of ticks for a given Persian date.
    /// </summary>
    /// <param name="dtPersian">The Persian date for which to calculate the ticks.</param>
    /// <returns>The number of ticks that represent the date and time of the specified Persian date.</returns>
    private static long CalculatePersianTicks(PersianDateTime dtPersian)
    {
        // Convert the Persian date to a Julian Day number
        var julianDay = PersianToJulianDay(dtPersian.Year, dtPersian.Month, dtPersian.Day);

        // Calculate the number of days since the start of the Persian calendar
        var daysSincePersianEpoch = julianDay - PersianEpoch;

        // Convert the number of days to ticks (1 day = 24 hours = 1440 minutes = 86400 seconds = 864000000000 ticks)
        var ticks = (long)(daysSincePersianEpoch * 864000000000);

        return ticks;
    }

    /// <summary>
    /// Converts an Islamic date to a Julian day number.
    /// </summary>
    /// <param name="year">The Islamic year.</param>
    /// <param name="month">The Islamic month.</param>
    /// <param name="day">The Islamic day.</param>
    /// <returns>The Julian day number corresponding to the given Islamic date.</returns>
    private static double IslamicToJulianDay(int year, int month, int day)
    {
        return day
            + Math.Ceiling(MonthAdjustment * (month - 1))
            + (year - 1) * IslamicDaysInYear
            + Math.Floor((IslamicLeapYearOffset + IslamicLeapYearMultiplier * year) / (double)IslamicLeapYearCycle)
            + IslamicEpoch - 1;
    }

    /// <summary>
    /// Converts a Julian day number to an Islamic date.
    /// </summary>
    /// <param name="julianDay">The Julian day number.</param>
    /// <returns>A tuple containing the Islamic year, month, and day.</returns>
    private static IslamicDateTime JulianDayToIslamic(double julianDay)
    {
        julianDay = Math.Floor(julianDay) + HalfDayOffset;
        var year = (int)Math.Floor((DaysInMonth * (julianDay - IslamicEpoch) + EpochOffset) / YearLength);
        var month = (int)Math.Min(12, Math.Ceiling((julianDay - (MonthLength + IslamicToJulianDay(year, 1, 1))) / MonthAdjustment) + 1);
        var day = (int)(julianDay - IslamicToJulianDay(year, month, 1) + 1);

        return new IslamicDateTime(year, month, day);
    }

    /// <summary>
    ///     Converts a Gregorian date to a Islamic (Hijri) date.
    /// </summary>
    /// <param name="date">The Gregorian date to be converted.</param>
    /// <returns>An IslamicDateTime object representing the converted Islamic date.</returns>
    public static IslamicDateTime ConvertToIslamic(DateTime date)
    {
        var jd = GregorianToJulianDay(date.Year, date.Month, date.Day);
        var dtIslamic = JulianDayToIslamic(jd);

        return dtIslamic;
    }

    /// <summary>
    /// Converts a date in the Islamic calendar to its equivalent in the Gregorian calendar.
    /// </summary>
    /// <param name="year">The year in the Islamic calendar.</param>
    /// <param name="month">The month in the Islamic calendar.</param>
    /// <param name="day">The day in the Islamic calendar.</param>
    /// <param name="hour">The hour of the day.</param>
    /// <param name="minute">The minute of the hour.</param>
    /// <param name="second">The second of the minute.</param>
    /// <param name="millisecond">The millisecond of the second. Defaults to 0 if not provided.</param>
    /// <returns>
    /// A DateTime object that represents the equivalent date and time in the Gregorian calendar.
    /// </returns>
    internal static DateTime ConvertIslamicToGregorian(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
    {
        var jd = IslamicToJulianDay(year, month, day);
        var dateOnly = JulianDayToGregorian(jd);
        var date = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, hour, minute, second, millisecond);
        return date;
    }

    /// <summary>
    /// Converts a Persian (Solar Hijri) date to the equivalent Islamic (Lunar Hijri) date.
    /// </summary>
    /// <param name="year">The year in the Persian calendar.</param>
    /// <param name="month">The month in the Persian calendar (1 to 12).</param>
    /// <param name="day">The day in the Persian calendar.</param>
    /// <returns>
    /// An <see cref="IslamicDateTime"/> object representing the equivalent date in the Islamic calendar.
    /// </returns>
    internal static IslamicDateTime ConvertPersianToIslamic(int year, int month, int day)
    {
        //var jd = persian_to_jd(year, month, day);
        var jd = PersianToJulianDay(year, month, day);
        var dtIslamic = JulianDayToIslamic(jd);

        return dtIslamic;
    }

    /// <summary>
    /// Converts an Islamic (Lunar Hijri) date to a Persian (Solar Hijri) date.
    /// </summary>
    /// <param name="year">The year in the Islamic calendar.</param>
    /// <param name="month">The month in the Islamic calendar (1 to 12).</param>
    /// <param name="day">The day in the Islamic calendar.</param>
    /// <returns>
    /// A <see cref="PersianDateTime"/> object representing the equivalent date in the Persian calendar.
    /// </returns>
    internal static PersianDateTime ConvertIslamicToPersian(int year, int month, int day)
    {
        var jd = IslamicToJulianDay(year, month, day);
        var dtPersian = JulianDayToPersian(jd);

        return dtPersian;
    }

    /// <summary>
    /// Calculates the number of days in a given month of a given year in the Islamic calendar.
    /// </summary>
    /// <param name="year">The year in the Islamic calendar.</param>
    /// <param name="month">The month in the Islamic calendar.</param>
    /// <returns>The number of days in the specified month of the specified year in the Islamic calendar.</returns>
    internal static int GetDaysInIslamicMonth(int year, int month)
    {
        // Calculate the Julian day of the first day of the current month
        var firstDayOfCurrentMonth = IslamicToJulianDay(year, month, 1);

        // Calculate the Julian day of the first day of the next month
        var firstDayOfNextMonth = month == 12 ? IslamicToJulianDay(year + 1, 1, 1) : IslamicToJulianDay(year, month + 1, 1);

        return (int)(firstDayOfNextMonth - firstDayOfCurrentMonth);
    }
}