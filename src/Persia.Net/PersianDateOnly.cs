namespace Persia.Net;

internal class PersianDateOnly
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }

    public PersianDateOnly(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }
}