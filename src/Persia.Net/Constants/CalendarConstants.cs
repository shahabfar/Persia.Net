namespace Persia.Net.Constants;

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
    internal const double DaysInYear = 365;

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
    ///     The Persian year corresponding to the Persian Epoch.
    /// </summary>
    internal const int PersianEpochYear = 475;

    /// <summary>
    ///     The number of days in a Persian calendar cycle.
    /// </summary>
    internal const double CycleDays = 1029983;

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
    ///     An adjustment made to the epoch in Persian calendar calculations.
    /// </summary>
    internal const double EpochAdjustment = 474;

    /// <summary>
    ///     The number of days in the first half of a Persian year.
    /// </summary>
    internal const double DaysInHalfYear = 186;

    /// <summary>
    ///     The year before the epoch in the Persian calendar.
    /// </summary>
    internal const int YearBeforeEpoch = 473;

    /// <summary>
    ///     The number of months in the first half of a Persian year.
    /// </summary>
    internal const int FirstHalfMonths = 7;

    /// <summary>
    ///     The number of days in the first half of a Persian month.
    /// </summary>
    internal const int DaysInFirstHalfMonth = 31;

    /// <summary>
    ///     A factor used in Persian calendar leap year calculations.
    /// </summary>
    internal const int LeapYearFactor = 682;

    /// <summary>
    ///     A value subtracted in Persian calendar leap year calculations.
    /// </summary>
    internal const int LeapYearSubtractor = 110;

    /// <summary>
    ///     The number of days in the second half of a Persian month.
    /// </summary>
    internal const int DaysInSecondHalfMonth = 30;

    /// <summary>
    ///     The difference in days between the first half and second half of a Persian month.
    /// </summary>
    internal const int FirstHalfToSecondHalfDayDifference = 6;

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
    /// The total number of days in a 400-year cycle of the Gregorian calendar.
    /// </summary>
    internal const int DaysIn400Years = 146097;

    /// <summary>
    /// The total number of days in a century (100 years) of the Gregorian calendar.
    /// </summary>
    internal const int DaysInCentury = 36524;

    /// <summary>
    /// The total number of days in a 4-year cycle of the Gregorian calendar, including one leap year.
    /// </summary>
    internal const int DaysIn4YearCycle = 1461;

    /// <summary>
    /// The offset used in the calculation of the month from a Julian day number.
    /// </summary>
    internal const int MonthCalculationOffset = 373;
}