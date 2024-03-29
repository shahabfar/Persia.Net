namespace Persia.Net;

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
        // Convert the Gregorian date to Julian Day Number
        var jd = GregorianToJulianDay(date.Year, date.Month, date.Day);

        // Convert the Julian Day Number to a Persian date
        var persianDate = JulianDayToPersian(jd);

        // Get the progress of the year for the Persian date
        var (dayOfYear, daysRemainingInYear) = GetYearProgress(persianDate.Year, DateOnly.FromDateTime(date));

        persianDate.SetYearProgress(dayOfYear, daysRemainingInYear);
        var daysInMonth = GetDaysInPersianMonth(persianDate.Year, persianDate.Month);
        persianDate.SetDaysInMonth(daysInMonth);
        var ticks = CalculatePersianTicks(persianDate);
        persianDate.SetTicks(ticks);
        return persianDate;
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
        var jd = PersianToJulianDay(year, month, day);
        var dateOnly = JulianDayToGregorian(jd);
        var date = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, hour, minute, second, millisecond);
        return date;
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
        julianDay = Math.Floor(julianDay) + 0.5;

        var depoch = julianDay - PersianToJulianDay(PersianEpochYear, 1, 1);
        var completeCycles = Math.Floor(depoch / CycleDays);
        var yearsIntoCurrentCycle = depoch % CycleDays;
        var yearsInCurrentCycle = Math.Abs(yearsIntoCurrentCycle - CycleEndDays) < Tolerance
            ? YearsInCycle
            : Math.Floor((YearFactor * Math.Floor(yearsIntoCurrentCycle / DaysInLeapYear) + DayFactor * (yearsIntoCurrentCycle % DaysInLeapYear) + 2815) / CycleFactor) + Math.Floor(yearsIntoCurrentCycle / DaysInLeapYear) + 1;

        var year = (int)(yearsInCurrentCycle + YearsInCycle * completeCycles + EpochAdjustment);
        if (year <= 0)
            year--;

        var dayOfYear = julianDay - PersianToJulianDay(year, 1, 1) + 1;
        var month = dayOfYear <= DaysInHalfYear ? (int)Math.Ceiling(dayOfYear / 31) : (int)Math.Ceiling((dayOfYear - 6) / 30);
        var day = (int)(julianDay - PersianToJulianDay(year, month, 1)) + 1;

        return new PersianDateTime(year, month, day);
    }

    /// <summary>
    ///     Converts a Persian date to a Julian Day number.
    /// </summary>
    /// <param name="year">The year part of the Persian date.</param>
    /// <param name="month">The month part of the Persian date.</param>
    /// <param name="day">The day part of the Persian date.</param>
    /// <returns>The Julian Day number corresponding to the input Persian date.</returns>
    private static double PersianToJulianDay(int year, int month, int day)
    {
        float YearsSincePersianEpoch = year - (year >= 0 ? YearBeforeEpoch + 1 : YearBeforeEpoch);
        var PersianYearAdjustedToEpoch = YearBeforeEpoch + 1 + YearsSincePersianEpoch % YearsInCycle;

        return day +
               (month <= FirstHalfMonths ? (month - 1) * DaysInFirstHalfMonth : (month - 1) * DaysInSecondHalfMonth + FirstHalfToSecondHalfDayDifference)
               + Math.Floor((PersianYearAdjustedToEpoch * LeapYearFactor - LeapYearSubtractor) / DayFactor)
               + (PersianYearAdjustedToEpoch - 1) * DaysInYear + Math.Floor(YearsSincePersianEpoch / YearsInCycle) * CycleDays + (PersianEpoch - 1);
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
        var wholeJulianDay = Math.Floor(julianDay - 0.5) + 0.5;
        var daysSinceEpoch = wholeJulianDay - GregorianEpoch;
        var quadricent = Math.Floor(daysSinceEpoch / DaysIn400Years);
        var daysInCurrentLeapYearCycle = daysSinceEpoch % DaysIn400Years;
        var numberOfCompleteCenturies = Math.Floor(daysInCurrentLeapYearCycle / DaysInCentury);
        var daysInCurrentCentury = daysInCurrentLeapYearCycle % DaysInCentury;
        var numberOfCompleteLeapYearFrequencies = Math.Floor(daysInCurrentCentury / DaysIn4YearCycle);
        var daysInCurrentLeapYearFrequency = daysInCurrentCentury % DaysIn4YearCycle;
        var yearIndexInCurrentLeapYearFrequency = Math.Floor(daysInCurrentLeapYearFrequency / DaysInYear);
        var year = (int)(quadricent * LeapYearCycle + numberOfCompleteCenturies * Century + numberOfCompleteLeapYearFrequencies * LeapYearFrequency + yearIndexInCurrentLeapYearFrequency);
        if (!(Math.Abs(numberOfCompleteCenturies - 4) < Tolerance || Math.Abs(yearIndexInCurrentLeapYearFrequency - 4) < Tolerance))
            year++;

        var dayOfYear = wholeJulianDay - GregorianToJulianDay(year, 1, 1);
        float leapYearAdjustment = wholeJulianDay < GregorianToJulianDay(year, 3, 1) ? 0 : IsGregorianLeapYear(year) ? 1 : 2;
        var month = (int)Math.Floor(((dayOfYear + leapYearAdjustment) * 12 + MonthCalculationOffset) / MonthFactorMultiplier);
        var day = (int)(wholeJulianDay - GregorianToJulianDay(year, month, 1) + 1);

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
        // Calculate the Julian day of the first day of the current month
        var firstDayOfCurrentMonth = PersianToJulianDay(year, month, 1);

        // Calculate the Julian day of the first day of the next month
        var firstDayOfNextMonth = month == 12 ? PersianToJulianDay(year + 1, 1, 1) : PersianToJulianDay(year, month + 1, 1);

        return (int)(firstDayOfNextMonth - firstDayOfCurrentMonth);
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
            + Math.Floor((double)((IslamicLeapYearOffset + IslamicLeapYearMultiplier * year) / IslamicLeapYearCycle))
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
    ///     Converts a Persian date to a Islamic (Hijri) date.
    /// </summary>
    /// <param name="date">The Persian date to be converted.</param>
    /// <returns>An IslamicDateTime object representing the converted Islamic date.</returns>
    internal static IslamicDateTime ConvertPersianToIslamic(int year, int month, int day)
    {
        var jd = PersianToJulianDay(year, month, day);
        var dtIslamic = JulianDayToIslamic(jd);

        return dtIslamic;
    }

    /// <summary>
    ///     Converts an Islamic date to a Persian (Hijri) date.
    /// </summary>
    /// <param name="date">The Islamic date to be converted.</param>
    /// <returns>A PersianDateTime representing the converted Persian date.</returns>
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