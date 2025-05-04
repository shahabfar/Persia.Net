namespace Persia.Net.Test;

public class PersianDateOnlyTests
{
    [Theory]
    [InlineData(1977, 02, 08, 19, 11, 1355)]
    [InlineData(1980, 07, 20, 29, 4, 1359)]
    [InlineData(1995, 12, 31, 10, 10, 1374)]
    [InlineData(2000, 01, 01, 11, 10, 1378)]
    [InlineData(2010, 05, 15, 25, 2, 1389)]
    [InlineData(2022, 09, 23, 1, 7, 1401)]
    [InlineData(2025, 03, 19, 29, 12, 1403)]
    [InlineData(2025, 03, 20, 30, 12, 1403)]
    [InlineData(2025, 03, 21, 1, 1, 1404)]
    [InlineData(2028, 03, 20, 1, 1, 1407)] // New entry
    [InlineData(2029, 03, 19, 29, 12, 1407)] // New entry
    [InlineData(2029, 03, 20, 1, 1, 1408)] // New entry
    [InlineData(2030, 03, 20, 30, 12, 1408)] // New entry
    [InlineData(2034, 03, 20, 30, 12, 1412)] // New entry
    public void Test_ConvertToPersianDate_ReturnCorrectDate(int year, int month, int day, int expectedDay, int expectedMonth, int expectedYear)
    {
        // Arrange
        DateOnly? nullableDate = new DateOnly(year, month, day);
        DateOnly nonNullableDate = new DateOnly(year, month, day);

        // Act
        var convertedNullableDate = nullableDate.ToPersianDateTime();
        var convertedNonNullableDate = nonNullableDate.ToPersianDateTime();

        // Assert
        Assert.Equal(expectedDay, convertedNullableDate.Day);
        Assert.Equal(expectedMonth, convertedNullableDate.Month);
        Assert.Equal(expectedYear, convertedNullableDate.Year);

        Assert.Equal(expectedDay, convertedNonNullableDate.Day);
        Assert.Equal(expectedMonth, convertedNonNullableDate.Month);
        Assert.Equal(expectedYear, convertedNonNullableDate.Year);
    }
}

