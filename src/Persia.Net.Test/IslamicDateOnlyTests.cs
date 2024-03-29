namespace Persia.Net.Test;

public class IslamicDateOnlyTests
{
    [Fact]
    public void Test_ConvertToIslamicDate_ReturnCorrectDate()
    {
        // Arrange
        DateOnly nonNullableDate = new DateOnly(2024, 03, 26);
        DateOnly? nullableDate = new DateOnly(2024, 03, 26);

        // Act
        var convertedNonNullableDate = nonNullableDate.ToIslamicDateTime();
        var convertedNullableDate = nullableDate.ToIslamicDateTime();

        // Assert
        Assert.Equal(16, convertedNonNullableDate.Day);
        Assert.Equal(09, convertedNonNullableDate.Month);
        Assert.Equal(1445, convertedNonNullableDate.Year);
        Assert.Equal(16, convertedNullableDate.Day);
        Assert.Equal(09, convertedNullableDate.Month);
        Assert.Equal(1445, convertedNullableDate.Year);
    }
}