namespace Persia.Net;

public partial class PersianDateTime : IEquatable<PersianDateTime>, IComparable<PersianDateTime>
{
    public bool Equals(PersianDateTime? other)
    {
        if (other is null)
            return false;

        return Year == other.Year && Month == other.Month && Day == other.Day;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((PersianDateTime)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Year;
            hashCode = (hashCode * 397) ^ Month;
            hashCode = (hashCode * 397) ^ Day;
            return hashCode;
        }
    }

    public static bool operator ==(PersianDateTime left, PersianDateTime right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(PersianDateTime left, PersianDateTime right)
    {
        return !(left == right);
    }

    public static bool operator >(PersianDateTime left, PersianDateTime right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            throw new ArgumentNullException();

        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(PersianDateTime left, PersianDateTime right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            throw new ArgumentNullException();

        return left.CompareTo(right) >= 0;
    }

    public static bool operator <(PersianDateTime left, PersianDateTime right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            throw new ArgumentNullException();

        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(PersianDateTime left, PersianDateTime right)
    {
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            throw new ArgumentNullException();

        return left.CompareTo(right) <= 0;
    }

    public int CompareTo(PersianDateTime? other)
    {
        if (other is null)
            return 1; // All instances are greater than null

        if (Year != other.Year)
            return Year.CompareTo(other.Year);

        if (Month != other.Month)
            return Month.CompareTo(other.Month);

        if (Day != other.Day)
            return Day.CompareTo(other.Day);

        // Compare time if needed
        var thisTime = new TimeSpan(Hour, Minute, Second, Millisecond);
        var otherTime = new TimeSpan(other.Hour, other.Minute, other.Second, other.Millisecond);

        return thisTime.CompareTo(otherTime);
    }
}