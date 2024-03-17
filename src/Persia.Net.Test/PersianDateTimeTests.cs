using Persia.Net.DateTimes;

namespace Persia.Net.Test
{
    public class PersianDateTimeTests
    {
        [Fact]
        public void Test_ConvertToPersianDate_ReturnCorrectDate()
        {
            // Arrange
            DateTime? nullableDate = new DateTime(2024, 03, 10);
            DateTime nonNullableDate = new DateTime(2024, 03, 10);

            // Act
            var convertedNullableDate = nullableDate.ToPersianDateTime();
            var convertedNonNullableDate = nonNullableDate.ToPersianDateTime();

            // Assert
            Assert.Equal(20, convertedNullableDate.Day);
            Assert.Equal(12, convertedNullableDate.Month);
            Assert.Equal(1402, convertedNullableDate.Year);

            Assert.Equal(20, convertedNonNullableDate.Day);
            Assert.Equal(12, convertedNonNullableDate.Month);
            Assert.Equal(1402, convertedNonNullableDate.Year);
        }

        [Fact]
        public void Test_ConvertToPersianDateToString_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedNonNullableDateString = date.ToPersianDateTime().ToString();

            // Assert
            Assert.Equal("1402/12/20", convertedNonNullableDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianString_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToShortPersianString();

            // Assert
            Assert.Equal("۱۴۰۲/۱۲/۲۰", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianDayMonthString_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianDayMonthString();

            // Assert
            Assert.Equal("۱۴۰۲ بیستم اسفند", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianStringWithWeekDay_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianWeekdayString();

            // Assert
            Assert.Equal("۱۴۰۲/۱۲/۲۰ یکشنبه", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianDayMonthStringWithWeekday_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToLongPersianOrdinalWords();

            // Assert
            Assert.Equal("یکشنبه بیستم اسفند ۱۴۰۲", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianMonthString_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 22);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianString();

            // Assert
            Assert.Equal("‫۳ فروردین ۱۴۰۳‬", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianMonthStringWithWeekday_ReturnCorrectStirng()
        {
            // Arrange
            var date = new DateTime(2024, 03, 22);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToLongPersianString();

            // Assert
            Assert.Equal("جمعه ۳ فروردین ۱۴۰۳", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToDateTime_ReturnCorrectDate()
        {
            // Arrange
            const int persianYear = 1403;
            const int persianMonth = 01;
            const int persianDay = 01;

            // Act
            var date = PersianDateTime.ToDateTime(persianYear, persianMonth, persianDay).Date;
            var dateOnly = PersianDateTime.ToDateOnly(persianYear, persianMonth, persianDay);

            // Assert
            Assert.Equal(new DateTime(2024, 03, 20).Date, date);
            Assert.Equal(new DateOnly(2024, 03, 20), dateOnly);
        }

        [Fact]
        public void Test_IsGivenPersianYearIsLeap_ReturnCorrectDate()
        {
            // Arrange
            const int persianYear = 1403;

            // Act
            var leapStatus = PersianDateTime.CheckLeapYear(persianYear);

            // Assert
            Assert.Equal(false, leapStatus);
        }

        [Fact]
        public void Test_GetDayOfYearAndDaysRamainingInYear_ReturnCorrectNumbers()
        {
            // Arrange
            var date = new DateTime(2024, 03, 19);

            // Act
            var persianDate = date.ToPersianDateTime();

            // Assert
            Assert.Equal(365, persianDate.DayOfYear);
            Assert.Equal(0, persianDate.DaysRemainingInYear);
        }

        //[Fact]
        //public void Test_HumanizePersianDateTimePassed_ReturnCorrectText()
        //{
        //    // Arrange
        //    var date = new DateTime(2022, 03, 21);

        //    // Act
        //    //var humanizedPersian = date.HumanizePersianDateTimePassed(6);
        //    var humanizedPersian = date.HumanizePersianDateTimePassed(TimeUnit.Seconds);

        //    // Assert
        //    Assert.Equal("‫۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش‬", humanizedPersian);
        //}
    }
}