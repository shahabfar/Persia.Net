namespace Persia.Net.Test;

public class PersianDateOnlyTests
{
    [Fact]
    public void Test_ConvertToPersianDate_ReturnCorrectDate()
    {
        // Arrange
        DateOnly? nullableDate = new DateOnly(2024, 03, 29);
        DateOnly nonNullableDate = new DateOnly(2024, 03, 29);

        // Act
        var convertedNullableDate = nullableDate.ToPersianDateTime();
        var convertedNonNullableDate = nonNullableDate.ToPersianDateTime();

        // Assert
        Assert.Equal(10, convertedNullableDate.Day);
        Assert.Equal(1, convertedNullableDate.Month);
        Assert.Equal(1403, convertedNullableDate.Year);

        Assert.Equal(10, convertedNonNullableDate.Day);
        Assert.Equal(01, convertedNonNullableDate.Month);
        Assert.Equal(1403, convertedNonNullableDate.Year);
    }

    //[Fact]
    //public void Test_HumanizePersianDateTimePassed_ReturnCorrectText()
    //{
    //    // Arrange
    //    var date = new DateOnly(2022, 03, 25);

    //    // Act
    //    //var humanizedPersian = date.HumanizePassedPersianDateTime();
    //    var humanizedPersian = date.HumanizePassedPersianDateTime(TimeUnit.Days);

    //    // Assert
    //    Assert.Equal("‫۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش‬", humanizedPersian);
    //}

    [Fact]
    public void Test_GetNextCurrentPreviousDayWeekMonth_ReturnCorrectDate()
    {
        // Arrange
        var date = new DateOnly(2024, 03, 19);
        DateOnly? nullableDate = new DateOnly(2024, 03, 19);

        // Act
        var nextDay = nullableDate.NextPersianDay();
        var prevDay = date.PreviousPersianDay();
        var currentWeek = date.CurrentPersianWeek();
        var nextWeek = date.NextPersianWeek();
        var previousWeek = date.PreviousPersianWeek();
        var currentMonth = date.CurrentPersianMonth();
        var nextMonth = date.NextPersianMonth();
        var previousMonth = date.PreviousPersianMonth();

        // Assert
        Assert.Equal(new PersianDateTime(1403, 01, 01), nextDay);
        Assert.Equal(new PersianDateTime(1402, 12, 28), prevDay);
        Assert.Equal(new PersianDateTime(1403, 01, 01), currentWeek[4]);
        Assert.Equal(new PersianDateTime(1403, 01, 04), nextWeek[0]);
        Assert.Equal(new PersianDateTime(1402, 12, 25), previousWeek[6]);
        Assert.Equal(new PersianDateTime(1402, 12, 26), currentMonth[25]);
        Assert.Equal(new PersianDateTime(1403, 01, 03), nextMonth[2]);
        Assert.Equal(new PersianDateTime(1402, 11, 30), previousMonth[29]);
    }
}