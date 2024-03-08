namespace Persia.Net;

internal static class DateTimeExtensions
{
    public static PersianDateTime ToPersianDateTime(this DateTime? date)
    {
        if (!date.HasValue)
            throw new ArgumentNullException(nameof(date));

        return Converter.ConvertToPersian(date.Value);
    }
}