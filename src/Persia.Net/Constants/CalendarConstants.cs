namespace Persia.Net;

internal class CalendarConstants
{
    /// <summary>
    ///     The Julian Day number of the start of the Gregorian calendar, which is October 15, 1582.
    /// </summary>
    internal const double GregorianEpoch = 1721425.5;

    /// <summary>
    ///     The Julian Day number of the start of the Persian calendar, which is March 21, 622.
    /// </summary>
    internal const double PersianEpoch = 1948320.5;

    /// <summary>
    /// Pivot value for the Gregorian reform adjustment (used in alpha calculation).
    /// </summary>
    internal const double GregorianReformPivot = 1867216.25;

    /// <summary>
    ///     The Julian Day number of the start of the Islamic calendar, which is July 16, 622.
    /// </summary>
    internal const double IslamicEpoch = 1948439.5;

    /// <summary>
    ///     A small number used for floating-point comparisons to account for rounding errors.
    /// </summary>
    internal const double Tolerance = 0.00001;

    /// <summary>
    ///     The number of days in a non-leap year.
    /// </summary>
    internal const int DaysInYear = 365;

    /// <summary>
    ///     The number of years in the Gregorian calendar's leap year cycle.
    /// </summary>
    internal const double LeapYearCycle = 400;

    /// <summary>
    ///     A factor used in calculations involving months.
    /// </summary>
    internal const double MonthFactorMultiplier = 367;

    /// <summary>
    ///     A value subtracted in calculations involving months.
    /// </summary>
    internal const double MonthFactorSubtractor = 362;

    /// <summary>
    ///     The frequency of leap years in the Gregorian calendar.
    /// </summary>
    internal const double LeapYearFrequency = 4;

    /// <summary>
    ///     The number of years in a century.
    /// </summary>
    internal const double Century = 100;

    /// <summary>
    ///     The number of days at the end of a Persian calendar cycle.
    /// </summary>
    internal const double CycleEndDays = 1029982;

    /// <summary>
    ///     The number of years in a Persian calendar cycle.
    /// </summary>
    internal const double YearsInCycle = 2820;

    /// <summary>
    ///     A factor used in Persian calendar calculations involving years.
    /// </summary>
    internal const double YearFactor = 2134;

    /// <summary>
    ///     A factor used in Persian calendar calculations involving days.
    /// </summary>
    internal const double DayFactor = 2816;

    /// <summary>
    ///     The number of days in a leap year.
    /// </summary>
    internal const double DaysInLeapYear = 366;

    /// <summary>
    ///     A factor used in Persian calendar cycle calculations.
    /// </summary>
    internal const double CycleFactor = 1028522;

    /// <summary>
    ///     The number of days in the first half of a Persian year.
    /// </summary>
    internal const int DaysInHalfYear = 186;

    /// <summary>
    ///     The number of months in the first half of a Persian year.
    /// </summary>
    internal const int FirstHalfMonths = 6;

    /// <summary>
    ///     The number of days in the first half of a Persian month.
    /// </summary>
    internal const int DaysInFirstHalfMonth = 31;

    /// <summary>
    ///     The number of days in the second half of a Persian month.
    /// </summary>
    internal const int DaysInSecondHalfMonth = 30;

    /// <summary>
    /// Represents the offset used to adjust the Julian day number to start from the middle of the day.
    /// </summary>
    internal const double HalfDayOffset = 0.5;

    /// <summary>
    /// Represents the average number of days in a month in the Islamic calendar.
    /// </summary>
    internal const int DaysInMonth = 30;

    /// <summary>
    /// Represents the offset used to adjust the epoch of the Islamic calendar.
    /// </summary>
    internal const int EpochOffset = 10646;

    /// <summary>
    /// Represents the length of a year in the Islamic calendar in terms of days.
    /// </summary>
    internal const int YearLength = 10631;

    /// <summary>
    /// Represents the length of a month in the Islamic calendar in terms of days.
    /// </summary>
    internal const int MonthLength = 29;

    /// <summary>
    /// Represents the adjustment factor used to calculate the month from the Julian day number.
    /// </summary>
    internal const double MonthAdjustment = 29.5;

    /// <summary>
    /// Represents the number of days in a year in the Islamic calendar.
    /// </summary>
    internal const int IslamicDaysInYear = 354;

    /// <summary>
    /// Represents the length of the leap year cycle in the Islamic calendar.
    /// </summary>
    internal const int IslamicLeapYearCycle = 30;

    /// <summary>
    /// Represents the offset used when calculating leap years in the Islamic calendar.
    /// </summary>
    internal const int IslamicLeapYearOffset = 3;

    /// <summary>
    /// Represents the multiplier used when calculating leap years in the Islamic calendar.
    /// </summary>
    internal const int IslamicLeapYearMultiplier = 11;

    /// <summary>
    /// Average number of days in a Gregorian “century” (100 × 365.2425).
    /// </summary>
    internal const double DaysPerGregorianCentury = 36524.25;

    /// <summary>
    /// Offset to align the intermediate count so that March = month 1 in the algorithm.
    /// </summary>
    internal const int JulianConversionBaseOffset = 1524;

    /// <summary>
    /// Companion offset for extracting the year from the day count.
    /// </summary>
    internal const double JulianYearOffset = 122.1;

    /// <summary>
    /// Average length of a Julian year, used when breaking out the year from days.
    /// </summary>
    internal const double JulianYearDays = 365.25;

    /// <summary>
    /// Factor for converting the remaining days into a month index.
    /// </summary>
    internal const double MonthExtractionFactor = 30.6001;

    /// <summary>
    /// Year offset for dates in March–December when computing the final year.
    /// </summary>
    internal const int YearOffsetAfterFebruary = 4716;

    /// <summary>
    /// Year offset for dates in January–February when computing the final year.
    /// </summary>
    internal const int YearOffsetBeforeMarch = 4715;
}
