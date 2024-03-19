namespace Persia.Net.DateTimes;

public class PersianDateOnly(int year, int month, int day)
{
    public int Year { get; set; } = year;
    public int Month { get; set; } = month;
    public int Day { get; set; } = day;
}