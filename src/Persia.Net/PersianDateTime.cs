namespace Persia.Net;

internal class PersianDateTime
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }

    public PersianDateTime(int year, int month, int day, int hour, int minute, int second)
    {
        Year = year;
        Month = month;
        Day = day;
        Hour = hour;
        Minute = minute;
        Second = second;
    }
}