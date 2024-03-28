# Persia.Net

[![Publish to NuGet](https://github.com/shahabfar/Persia.Net/actions/workflows/publish.yml/badge.svg)](https://github.com/shahabfar/Persia.Net/actions/workflows/publish.yml)

Persia.Net is a robust class library, meticulously designed to facilitate seamless conversion between Persian, Gregorian, and Arabic (Hijri) dates. Originally crafted with .NET, it has evolved to ensure compatibility with .NET 6.0, 7.0, and the latest 8.0, making it a versatile tool for a wide range of .NET applications.

![License: Apache-2.0](https://img.shields.io/badge/License-Apache_2.0-blue.svg)

## Install via NuGet

To install Persia.Net, run the following command in the Package Manager Console:

[![Nuget](https://img.shields.io/nuget/v/Persia.Net)](https://www.nuget.org/packages/Persia.Net/)

```
PM> Install-Package Persia.Net
```

You can also view the [package page](https://www.nuget.org/packages/Persia.Net/) on NuGet.


# Using Persia.Net

Persia.Net is a versatile class library designed to seamlessly convert dates between Persian, Gregorian, and Arabic (Hijri) calendars. This guide will show you how to use Persia.Net to convert dates.

## Converting Dates

You can convert both nullable and non-nullable `DateTime` objects to Persian dates using the `ToPersianDateTime()` extension method.

Here's an example:

```csharp
DateTime? nullableDate = new DateTime(2024, 03, 10);
DateTime nonNullableDate = new DateTime(2024, 03, 10);

var convertedNullableDate = nullableDate.ToPersianDateTime();
var convertedNonNullableDate = nonNullableDate.ToPersianDateTime();
```
In this example, both nullableDate and nonNullableDate are converted to Persian dates.

### Accessing Date Components
After converting a date, you can access the day, month, and year components of the Persian date like this:
```csharp
int day = convertedNullableDate.Day;
int month = convertedNullableDate.Month;
int year = convertedNullableDate.Year;
```
In this example, day, month, and year will hold the day, month, and year of the Persian date, respectively.

When you run this code, it will print:
```
Day: 20
Month: 12
Year: 1402
```
## PersianDateTime Class

The `PersianDateTime` class in the Persia.Net library is a comprehensive utility designed for handling and manipulating dates in the Persian calendar. Here's a brief overview of its capabilities:

| Property | Description |
| --- | --- |
| `Year` | Gets the year component of the date. |
| `Month` | Gets the month component of the date. |
| `Day` | Gets the day component of the date. |
| `Hour` | Gets the hour component of the time. |
| `Minute` | Gets the minute component of the time. |
| `Second` | Gets the second component of the time. |
| `Millisecond` | Gets the millisecond component of the time. |
| `Ticks` | Gets the number of ticks that represent the time. |
| `DayOfWeek` | Gets the day number of the week. This number is zero-based. |
| `DaysInMonth` | Gets the days of the month. |
| `WeekOfMonth` | Gets the week number of the month. |
| `WeekOfYear` | Gets the week number of the year. |
| `MonthName` | Gets the name of the current month. |
| `DayOfWeekName` | Gets the day name of the week in the Persian Calendar. |
| `IsLeapYear` | Gets a value indicating whether the current Persian year is a leap year. |
| `DayOfYear` | Gets the number of days that have passed since the beginning of the current year in the Persian calendar. |
| `DaysRemainingInYear` | Gets the number of days remaining until the end of the current year in the Persian calendar. |
| `Now` | Gets the current date and time in the Persian calendar. |
| `UtcNow` | Gets the current date and time in the Persian calendar in Coordinated Universal Time (UTC). |
| `Today` | Gets the current date in the Persian calendar.|


### Converting Gregorian date to Persian date

The provided code snippet is a simple example of converting a Gregorian date to a Persian date using `ToPersianDateTime()` extension method.

```csharp
var date = new DateTime(2024, 03, 10); // Define a Gregorian date

// Convert the Gregorian date to a Persian date
var convertedDateString = date.ToPersianDateTime().ToString();
Console.WriteLine(convertedDateString); 

// The result of the conversion is "1402/12/20"
```

In addition to date conversion, the `PersianDateTime` class also includes built-in string generation methods. These methods transform the converted DateTime into a human-readable Persian string, providing a more intuitive representation of the date and time in Persian. The following table outlines these methods:

| Method | Description |
| --- | --- |
| `ToShortPersianString` | Converts the date to a string in the format "yyyy/MM/dd" with Persian numbers.<br>برای مثال, ۱۴۰۲/۱۲/۲۰|
| `ToPersianString` | Converts the date to a string in the Persian format with Year and Day in digit and Month in word.<br>برای مثال, ۲۰ اسفند ۱۴۰۲|
| `ToLongPersianString` | Converts the date to a string in the Persian format with Year and Day in digit and Month in word as well as weekday name and time.<br>برای مثال, یکشنبه ۲۰ اسفند ۱۴۰۲|
| `ToPersianDayMonthString` | Converts the date to a string in the Persian format with Year in digit and Day and Month in words.<br>برای مثال, بیستم اسفند ۱۴۰۲|
| `ToPersianWeekdayString` | Converts the date to a string in Persian as well as week day name.<br>برای مثال, یکشنبه ۱۴۰۲/۱۲/۲۰|
| `ToLongPersianOrdinalWords` |Converts the date to a string in the Persian format with Year in digit and Day and Month in words as well as weekday name.<br>برای مثال, یکشنبه بیستم اسفند ۱۴۰۲|

## Converting Persian date to Gregorian DateTime
In .NET, the `PersianDateTime` class provides straightforward methods for converting Persian dates to Gregorian dates or `DateTime`. Specifically, the `ToDateTime` and `ToDateOnly` methods can be utilized for this purpose. Both of these methods are available as static and instance methods within the `PersianDateTime` class. This allows for flexible usage depending on the specific requirements of your code.

The `ToDateTime` method in the `PersianDateTime` class allows you to convert a Persian date to a Gregorian `DateTime`. This method is static and takes three parameters: `year`, `month`, and `day`.

#### Method Signature

```csharp
public static DateTime ToDateTime(int year, int month, int day)
```
#### Parameters
* `year`: The year component of the Persian date. 
* `month`: The month component of the Persian date. 
* `day`: The day component of the Persian date. 
#### Return Value
This method returns a `DateTime` object representing the equivalent Gregorian date and the current time of day.

Here’s an example of how to use this method:

```csharp
// Define a Persian date
int year = 1403;
int month = 1;
int day = 1;

// Convert the Persian date to a Gregorian DateTime
DateTime gregorianDate = PersianDateTime.ToDateTime(year, month, day);

// Output the result
Console.WriteLine(gregorianDate);
```
In the previous example, the time components (hour, minute, second, and millisecond) of the converted DateTime are derived from the system clock. However, if you wish to specify your own time values for the conversion, you can do so using the overloaded ToDateTime methods. These methods allow you to pass in the hour, minute, second, and optionally, the millisecond, along with the year, month, and day.

Here is the method signature for this overload:
```csharp
DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond = 0)
```
If you're only interested in the date portion of the converted `DateTime`, you can use the `ToDateOnly` method. This method returns a `DateOnly` object, which represents the converted Persian date without any time values. 

Here's the method signature for your reference:
```csharp
DateOnly ToDateOnly(int year, int month, int day)
```

### Human-Readable Time Difference Conversion
The `HumanizePersianDateTimePassed` method in the `PersianDateTime` class allows you to convert the time difference between the current date and a provided date into a human-readable format.

#### Method Signature

```csharp
string HumanizePersianDateTimePassed(int partsToPrint = 3)
string HumanizePassedPersianDateTime(TimeUnit timeUnit)
```
By default, this method generates a human-readable format with three components. However, if you prefer a more detailed output, you can specify up to six components. To do this, simply pass the desired number of components as an argument to this method. 

Here's an example:
```csharp
// Define a DateTime
var date = new DateTime(2023, 12, 21);

// Humanize the time difference between the current date and the provided date
var humanizedPersian = date.HumanizePersianDateTimePassed();

// Output the result
Console.WriteLine(humanizedPersian);

// The result of the conversion is "‫۱۱ ماه و ۳۶۲ روز و ۲ ساعت پیش‬"
```
Additionally, if you prefer to display the elapsed time in terms of a single time unit, you can specify this as an argument to the method. For instance, if you want to express the elapsed time solely in terms of days, you can pass TimeUnit.Days as an argument to the method.

Here’s how you can do this:
```csharp
var humanizedPersian = date.HumanizePassedPersianDateTime(TimeUnit.Days);
```
In this example, `HumanizePassedPersianDateTime` will return the elapsed time since the specified date, expressed solely in terms of days. This allows for a more focused representation of the elapsed time.
Here are the enumerable `TimeUnit` values:

 ```Years
    Months
    Days
    Hours
    Minutes
    Seconds
```
### Retrieving Detailed Week and Month data in the Persian calendar
If you're interested in obtaining information about the current week or month in the Persian calendar, this library offers specific `DateTime` extension methods designed for this purpose. These methods include:

- `CurrentPersianWeek`: Retrieves the current week.
- `NextPersianWeek`: Retrieves the next week.
- `PreviousPersianWeek`: Retrieves the previous week.
- `CurrentPersianMonth`: Retrieves the current month.
- `NextPersianMonth`: Retrieves the next month.
- `PreviousPersianMonth`: Retrieves the previous month.

These methods provide a convenient way to navigate through the Persian calendar, allowing you to easily access and manipulate date-related data.

![](https://github.com/shahabfar/persia.net/blob/master/next_persian_week.jpg)

These methods return an array of `PersianDateTime` objects, each containing detailed information about the requested week or month.

In the above image, you can see the date-related data for the next week of a given date as a `PersianDateTime` array.

## Converting Gregorian date to Islamic (Lunar Hijri) date

The provided code snippet is a simple example of converting a Gregorian date to an Islamic date using `ToIslamicDateTime()` extension method.

```csharp
var date = new DateTime(2024, 03, 26); // Define a Gregorian date

// Convert the Gregorian date to an Islamic date
var dtIslamic = date.ToIslamicDateTime(); // returns an object of IslamicDateTime class
Console.WriteLine(dtIslamic.Year); 
Console.WriteLine(dtIslamic.Month); 
Console.WriteLine(dtIslamic.Day); 
```

In addition to date conversion, the `IslamicDateTime` class also includes built-in string generation methods. These methods transform the converted DateTime into a human-readable Islamic string, providing a more intuitive representation of the date and time in Islamic. The following table outlines these methods:

| Method | Description |
| --- | --- |
| `ToString` | Converts the date to a string in the format "yyyy/MM/dd".<br>برای مثال, 1445/09/17|
| `ToShortArabicString` | Converts the date to a string in the format "yyyy/MM/dd" with Arabic numbers.<br>برای مثال, ١٤٤٥/٠٩/١٧|
| `ToArabicString` | Converts the date to a string in the Arabic format with Year and Day in digit and Month in word.<br>برای مثال, ١٧ رمضان ١٤٤٥|
| `ToLongArabicString` | Converts the date to a string in the Arabic format with Year and Day in digit and Month in word as well as weekday name.<br>برای مثال, الاربعا ١٧ رمضان ١٤٤٥ |

## Converting Persian date to Islamic (Lunar Hijri) date

The PersianDateTime class contains `ToIslamicDateTime` and `ToIslamicDateOnly` methods to convert a Persian (Solar Hijri) date to an Islamic (Lunar Hijri) date. The `ToIslamicDateTime` method also includes time conversion. These methods are existing in both static and instance methods.

The `ToIslamicDateTime` method takes seven parameters: the year, month, day, hour, minute, second, and an optional millisecond from the Persian date while, The `ToIslamicDateOnly` method takes three parameters: the year, month, and day from the Persian date.

Here’s a small sample code to demonstrate the conversion:
```csharp
var persianDate = new PersianDateTime(1403, 01, 09);

// Convert the Persian date to an Islamic date string
var islamicDate = persianDate.ToIslamicDateTime().ToString();

// Print the Islamic date
Console.WriteLine(islamicDate);
```

## Converting Islamic (Lunar Hijri) date to Persian date

The `IslamicDateTime` class contains `ToPersianDateTime` and `ToPersianDateOnly` methods to convert an Islamic (Lunar Hijri) date to a Persian (Solar Hijri) date. The `ToPersianDateTime` method also includes time conversion. These methods are existing in both static and instance methods.

The `ToPersianDateTime` method takes seven parameters: the year, month, day, hour, minute, second, and an optional millisecond from the Islamic date. It uses a Converter to convert the Islamic date to a Persian date, sets the time, and returns the resulting Persian date and time.

The `ToPersianDateOnly` method takes three parameters: the year, month, and day from the Islamic date. It uses a Converter to convert the Islamic date to a Persian date and returns the resulting Persian date.

Here’s a small sample code to demonstrate the conversion:
```csharp
var islamicDate = new IslamicDateTime(1445, 09, 18);

// Convert the Islamic date to a Persian date string
var persianDate = islamicDate.ToPersianDateOnly().ToString();

// Print the Persian date
Console.WriteLine(persianDate);
```
