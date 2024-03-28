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
        public void Test_ConvertToPersianDateToString_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedNonNullableDateString = date.ToPersianDateTime().ToString();

            // Assert
            Assert.Equal("1402/12/20", convertedNonNullableDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToShortPersianString_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToShortPersianString();

            // Assert
            Assert.Equal("۱۴۰۲/۱۲/۲۰", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianDayMonthString_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianDayMonthString();

            // Assert
            Assert.Equal("۱۴۰۲ بیستم اسفند", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianStringWithWeekDay_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianWeekdayString();

            // Assert
            Assert.Equal("۱۴۰۲/۱۲/۲۰ یکشنبه", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianDayMonthStringWithWeekday_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 10);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToLongPersianOrdinalWords();

            // Assert
            Assert.Equal("یکشنبه بیستم اسفند ۱۴۰۲", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToPersianMonthString_ReturnCorrectString()
        {
            // Arrange
            var date = new DateTime(2024, 03, 22);

            // Act
            var convertedDateString = date.ToPersianDateTime().ToPersianString();

            // Assert
            Assert.Equal("‫۳ فروردین ۱۴۰۳‬", convertedDateString);
        }

        [Fact]
        public void Test_ConvertToPersianDateToLongPersianString_ReturnCorrectString()
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
        public void Test_GetDayOfYearAndDaysRemainingInYear_ReturnCorrectNumbers()
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
        //    var date = new DateTime(2023, 12, 21);

        //    // Act
        //    //var humanizedPersian = date.HumanizePersianDateTimePassed(6);
        //    var humanizedPersian = date.HumanizePassedPersianDateTime(TimeUnit.Days);

        //    // Assert
        //    Assert.Equal("‫۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش‬", humanizedPersian);
        //}

        [Fact]
        public void Test_GetNextCurrentPreviousDayWeekMonth_ReturnCorrectDate()
        {
            // Arrange
            var date = new DateTime(2024, 03, 19);
            DateTime? nullableDate = new DateTime(2024, 03, 19);

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

        [Fact]
        public void Test_CompareTwoPersianDateTimeInstances_ReturnCorrectComparison()
        {
            // Arrange
            var dtPersian1 = new DateTime(2024, 03, 19).ToPersianDateTime();
            var dtPersian2 = new DateTime(2024, 03, 22).ToPersianDateTime();
            
            // Act
            var isEqual = dtPersian1 == dtPersian2;
            var isNotEqual = dtPersian1 != dtPersian2;
            var isGreater = dtPersian1 > dtPersian2;
            var isSmaller = dtPersian1 < dtPersian2;
            var isGreaterOrEqual = dtPersian1 >= dtPersian2;
            var isSmallerOrEqual = dtPersian1 <= dtPersian2;

            // Assert
            Assert.False(isEqual);
            Assert.True(isNotEqual);
            Assert.False(isGreater);
            Assert.True(isSmaller);
            Assert.False(isGreaterOrEqual);
            Assert.True(isSmallerOrEqual);
        }

        [Fact]
        public void Test_AddDaysAddMonths_ReturnCorrectDate()
        {
            // Arrange
            var dtPersian = new DateTime(2024, 03, 19).ToPersianDateTime();

            // Act
            var addedDays = dtPersian.AddDays(10);
            var addedMonths = dtPersian.AddMonths(23);

            // Assert
            Assert.Equal(new PersianDateTime(1403, 01, 10), addedDays);
            Assert.Equal(new PersianDateTime(1404, 11, 29), addedMonths);
        }

        [Fact]
        public void Parse_ValidDate_ReturnsCorrectPersianDateTime()
        {
            // Arrange
            var date = "۱۴۰۲/۱۲/۲۰";// "1402/12/20";

            // Act
            var dtPersian = PersianDateTime.Parse(date);
            var success = PersianDateTime.TryParse(date, out var result);
            var failure = PersianDateTime.TryParse("invalid date", out var result2);

            // Assert
            Assert.Equal(1402, dtPersian.Year);
            Assert.Equal(12, dtPersian.Month);
            Assert.Equal(20, dtPersian.Day);

            Assert.Throws<FormatException>(() => PersianDateTime.Parse("invalid date"));
            Assert.True(success);
            Assert.Equal(1402, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(20, result.Day);
            Assert.False(failure);
            Assert.Null(result2);
        }

        [Fact]
        public void Test_ConvertPersianToIslamicDate_ReturnCorrectDate()
        {
            // Arrange
            var dtPersian = new PersianDateTime(1403, 01, 09);

            // Act
            var convertedDate = dtPersian.ToIslamicDateTime();

            // Assert
            Assert.Equal(18, convertedDate.Day);
            Assert.Equal(09, convertedDate.Month);
            Assert.Equal(1445, convertedDate.Year);
        }
    }
}