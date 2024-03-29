namespace Persia.Net;

public static class NullableDateOnlyIslamicExtensions
{
    /// <summary>
    /// Converts a <see cref="DateOnly"/> object to a <see cref="IslamicDateTime"/>.
    /// </summary>
    /// <param name="dateOnly">The <see cref="DateOnly"/> object to convert (nullable). If null, an <see cref="ArgumentNullException"/> is thrown</param>
    /// <returns>An <see cref="IslamicDateTime"/> representing the converted date.</returns>
    public static IslamicDateTime ToIslamicDateTime(this DateOnly? dateOnly)
    {
        if (!dateOnly.HasValue)
            throw new ArgumentNullException(nameof(dateOnly));
        return dateOnly.Value.ToIslamicDateTime();
    }
}