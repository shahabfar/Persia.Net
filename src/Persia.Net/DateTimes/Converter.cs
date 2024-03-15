namespace Persia.Net.DateTimes;

using static Constants.CalendarConstants;

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
        var jd = GregorianTojulianDay(date.Year, date.Month, date.Day);

        // Convert the Julian Day Number to a Persian date
        var persianDate = JulianDayToPersian(jd);

        // Get the progress of the year for the Persian date
        var (dayOfYear, daysRemainingInYear) = GetYearProgress(persianDate.Year, date);

        persianDate.SetYearProgress(dayOfYear, daysRemainingInYear);
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
    private static double GregorianTojulianDay(int year, int month, int day)
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

        var dayOfYear = wholeJulianDay - GregorianTojulianDay(year, 1, 1);
        float leapYearAdjustment = wholeJulianDay < GregorianTojulianDay(year, 3, 1) ? 0 : IsGregorianLeapYear(year) ? 1 : 2;
        var month = (int)Math.Floor(((dayOfYear + leapYearAdjustment) * 12 + MonthCalculationOffset) / MonthFactorMultiplier);
        var day = (int)(wholeJulianDay - GregorianTojulianDay(year, month, 1) + 1);

        return new DateOnly(year, month, day);
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
    private static (int Year, int Month, int Day) JulianDayToIslamic(double julianDay)
    {
        julianDay = Math.Floor(julianDay) + HalfDayOffset;
        var year = (int)Math.Floor((DaysInMonth * (julianDay - IslamicEpoch) + EpochOffset) / YearLength);
        var month = (int)Math.Min(12, Math.Ceiling((julianDay - (MonthLength + IslamicToJulianDay(year, 1, 1))) / MonthAdjustment) + 1);
        var day = (int)(julianDay - IslamicToJulianDay(year, month, 1) + 1);

        return (year, month, day);
    }

    /// <summary>
    /// Calculates the progress of the year in the Persian calendar for a given date.
    /// </summary>
    /// <param name="year">The year in the Persian calendar.</param>
    /// <param name="date">The date for which to calculate the year progress.</param>
    /// <returns>A tuple containing the day of the year and the number of days remaining in the year.</returns>
    public static (int DayOfYear, int DaysRemainingInYear) GetYearProgress(int year, DateTime date)
    {
        // Calculate the JDN (Julian Day Number) for the start and end of the year
        var jdnStartOfYear = PersianToJulianDay(year, 1, 1);
        var jdnEndOfYear = PersianToJulianDay(year + 1, 1, 1) - 1;

        // Calculate the JDN for the given date
        var jdn = GregorianTojulianDay(date.Year, date.Month, date.Day);

        var DayOfYear = (int)(jdn - jdnStartOfYear + 1);
        var DaysRemainingInYear = (int)(jdnEndOfYear - jdn);

        return (DayOfYear, DaysRemainingInYear);
    }

}