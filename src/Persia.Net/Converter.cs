namespace Persia.Net;

internal static class Converter
{
    private const double GregorianEpoch = 1721425.5;
    private const double PersianEpoch = 1948320.5;
    private const double IslamicEpoch = 1948439.5;
    private const double Tolerance = 0.00001;
    private const double DaysInYear = 365;
    private const double LeapYearCycle = 400;
    private const double MonthFactorMultiplier = 367;
    private const double MonthFactorSubtractor = 362;
    private const double LeapYearFrequency = 4;
    private const double Century = 100;
    private const int PersianEpochYear = 475;
    private const double CycleDays = 1029983;
    private const double CycleEndDays = 1029982;
    private const double YearsInCycle = 2820;
    private const double YearFactor = 2134;
    private const double DayFactor = 2816;
    private const double DaysInLeapYear = 366;
    private const double CycleFactor = 1028522;
    private const double EpochAdjustment = 474;
    private const double DaysInHalfYear = 186;
    private const int YearBeforeEpoch = 473;
    private const int FirstHalfMonths = 7;
    private const int DaysInFirstHalfMonth = 31;
    private const int LeapYearFactor = 682;
    private const int LeapYearSubtractor = 110;
    private const int DaysInSecondHalfMonth = 30;
    private const int FirstHalfToSecondHalfDayDifference = 6;

    //internal static PersianDateTime ConvertToPersian(DateTime? date, int year, int month, int day, int hour, int minute, int second)
    internal static PersianDateTime ConvertToPersian(DateTime date)
    {
        var jd = GregorianTojulianDay(date.Year, date.Month, date.Day);
        var dateOnly = JulianDayToPersian(jd);
        return new PersianDateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day, date.Hour, date.Minute, date.Second);
    }

    private static double GregorianTojulianDay(int year, int month, int day)
    {
        double yearsBefore = year - 1;
        var leapYears = Math.Floor(yearsBefore / LeapYearFrequency) - Math.Floor(yearsBefore / Century) + Math.Floor(yearsBefore / LeapYearCycle);
        var monthFactor = Math.Floor((MonthFactorMultiplier * month - MonthFactorSubtractor) / 12);
        var leapCorrection = month <= 2 ? 0 : IsGregorianLeapYear(year) ? -1 : -2;
        var julianDay = GregorianEpoch - 1 + DaysInYear * yearsBefore + leapYears + monthFactor + leapCorrection + day;
        return julianDay;
    }

    private static PersianDateOnly JulianDayToPersian(double julianDay)
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

        return new PersianDateOnly(year, month, day);
    }

    private static double PersianToJulianDay(int year, int month, int day)
    {
        float YearsSincePersianEpoch = year - (year >= 0 ? YearBeforeEpoch + 1 : YearBeforeEpoch);
        var PersianYearAdjustedToEpoch = YearBeforeEpoch + 1 + YearsSincePersianEpoch % YearsInCycle;

        return day +
               (month <= FirstHalfMonths ? (month - 1) * DaysInFirstHalfMonth : (month - 1) * DaysInSecondHalfMonth + FirstHalfToSecondHalfDayDifference) + Math.Floor((PersianYearAdjustedToEpoch * LeapYearFactor - LeapYearSubtractor) / DayFactor) +
               (PersianYearAdjustedToEpoch - 1) * DaysInYear + Math.Floor(YearsSincePersianEpoch / YearsInCycle) * CycleDays + (PersianEpoch - 1);
    }

    private static bool IsGregorianLeapYear(int year)
    {
        return year % LeapYearFrequency == 0 && !(year % Century == 0 && year % LeapYearCycle != 0);
    }

    private static DateOnly JulianDayToGregorian(double julianDay)
    {
        var wholeJulianDay = Math.Floor(julianDay - 0.5) + 0.5;
        var daysSinceEpoch = wholeJulianDay - GregorianEpoch;
        var quadricent = Math.Floor(daysSinceEpoch / LeapYearCycle);
        var dqc = daysSinceEpoch % LeapYearCycle;
        var cent = Math.Floor(dqc / Century);
        var dcent = dqc % Century;
        var quad = Math.Floor(dcent / LeapYearFrequency);
        var dquad = dcent % LeapYearFrequency;
        var yindex = Math.Floor(dquad / DaysInYear);
        var year = (int)(quadricent * LeapYearCycle + cent * Century + quad * LeapYearFrequency + yindex);
        if (!(Math.Abs(cent - 4) < Tolerance || Math.Abs(yindex - 4) < Tolerance))
            year++;

        var yearday = wholeJulianDay - GregorianTojulianDay(year, 1, 1);
        float leapadj = wholeJulianDay < GregorianTojulianDay(year, 3, 1) ? 0 : IsGregorianLeapYear(year) ? 1 : 2;
        var month = (int)Math.Floor(((yearday + leapadj) * MonthFactorMultiplier + MonthFactorSubtractor) / LeapYearCycle);
        var day = (int)(wholeJulianDay - GregorianTojulianDay(year, month, 1) + 1);

        return new DateOnly(year, month, day);
    }

    private static float islamic_to_jd(int year, int month, int day)
    {
        return (float)(day + Math.Ceiling(29.5 * (month - 1)) + (year - 1) * 354 +
                       Math.Floor((double)((3 + 11 * year) / 30)) + IslamicEpoch) - 1;
    }

    private static int[] jd_to_islamic(float jd)
    {
        jd = (float)(Math.Floor(jd) + 0.5);
        var year = (int)Math.Floor((30 * (jd - IslamicEpoch) + 10646) / 10631);
        var month = (int)Math.Min(12, Math.Ceiling((jd - (29 + islamic_to_jd(year, 1, 1))) / 29.5) + 1);
        var day = (int)(jd - islamic_to_jd(year, month, 1) + 1);
        var res = new[] { year, month, day };
        return res;
    }
}