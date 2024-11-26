namespace Persia.Net.Test;

public class IslamicDateTimeTests
{
    [Fact]
    public void Test_ConvertToIslamicDate_ReturnCorrectDate()
    {
        // Arrange
        DateTime nonNullableDate = new DateTime(2024, 03, 26);

        // Act
        var convertedNonNullableDate = nonNullableDate.ToIslamicDateTime();

        // Assert
        Assert.Equal(16, convertedNonNullableDate.Day);
        Assert.Equal(09, convertedNonNullableDate.Month);
        Assert.Equal(1445, convertedNonNullableDate.Year);
    }

    [Fact]
    public void Test_ConvertToDateTime_ReturnCorrectDate()
    {
        // Arrange
        const int islamicYear = 1445;
        const int islamicMonth = 09;
        const int islamicDay = 17;

        // Act
        var date = IslamicDateTime.ToDateTime(islamicYear, islamicMonth, islamicDay).Date;
        var dateOnly = IslamicDateTime.ToDateOnly(islamicYear, islamicMonth, islamicDay);

        // Assert
        Assert.Equal(new DateTime(2024, 03, 27).Date, date);
        Assert.Equal(new DateOnly(2024, 03, 27), dateOnly);
    }

    [Fact]
    public void Test_ConvertToIslamicDateToString_ReturnCorrectString()
    {
        // Arrange
        var date = new DateTime(2024, 03, 27);

        // Act
        var convertedNonNullableDateString = date.ToIslamicDateTime().ToString();

        // Assert
        Assert.Equal("1445/09/17", convertedNonNullableDateString);
    }

    [Fact]
    public void Test_ConvertToIslamicDateToShortArabicString_ReturnCorrectString()
    {
        // Arrange
        var date = new DateTime(2024, 03, 27);

        // Act
        var convertedDateString = date.ToIslamicDateTime().ToShortArabicString();

        // Assert
        Assert.Equal("١٤٤٥/٠٩/١٧", convertedDateString);
    }

    [Fact]
    public void Test_ConvertToIslamicDateToArabicString_ReturnCorrectString()
    {
        // Arrange
        var date = new DateTime(2024, 03, 27);

        // Act
        var convertedDateString = date.ToIslamicDateTime().ToArabicString();

        // Assert
        Assert.Equal("‫١٧ رمضان ١٤٤٥‬", convertedDateString);
    }

    [Fact]
    public void Test_ConvertToIslamicDateToLongIslamicString_ReturnCorrectString()
    {
        // Arrange
        var date = new DateTime(2024, 03, 27);

        // Act
        var convertedDateString = date.ToIslamicDateTime().ToLongArabicString();

        // Assert
        Assert.Equal("الأربعاء ١٧ رمضان ١٤٤٥", convertedDateString);
    }

    [Fact]
    public void Parse_ValidDate_ReturnsCorrectIslamicDateTime()
    {
        // Arrange
        var date = "١٤٤٥/٠٩/١٧";// "1445/09/17";

        // Act
        var dtIslamic = IslamicDateTime.Parse(date);
        var success = IslamicDateTime.TryParse(date, out var result);
        var failure = IslamicDateTime.TryParse("invalid date", out var result2);

        // Assert
        Assert.Equal(1445, dtIslamic.Year);
        Assert.Equal(09, dtIslamic.Month);
        Assert.Equal(17, dtIslamic.Day);

        Assert.Throws<FormatException>(() => IslamicDateTime.Parse("invalid date"));
        Assert.True(success);
        Assert.Equal(1445, result?.Year);
        Assert.Equal(09, result?.Month);
        Assert.Equal(17, result?.Day);
        Assert.False(failure);
        Assert.Null(result2);
    }

    [Fact]
    public void Test_ConvertIslamicToPersianDate_ReturnCorrectDate()
    {
        // Arrange
        var dtIslamic = new IslamicDateTime(1446, 05, 23);

        // Act
        var convertedDate = dtIslamic.ToPersianDateOnly();

        // Assert
        Assert.Equal(05, convertedDate.Day);
        Assert.Equal(09, convertedDate.Month);
        Assert.Equal(1403, convertedDate.Year);
    }
}