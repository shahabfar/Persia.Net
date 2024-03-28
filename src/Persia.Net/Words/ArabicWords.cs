namespace Persia.Net;

public static class ArabicWords
{
    public static string ToArabicNumber(this string value, bool enableRLE = false)
    {
        var strOut = string.Empty;
        var nLen = value.Length;
        if (nLen == 0)
            return value;

        for (var i = 0; i < nLen; i++)
        {
            var ch = value[i];
            if ((48 <= ch) && (ch <= 57))
                ch = (char)(ch + 1584);

            strOut += ch;
        }

        return enableRLE ? $"{PersianWords.RleChar}{strOut}{PersianWords.PopDirectionalFormatting}" : strOut;
    }
}