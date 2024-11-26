using System.Text;

namespace Persia.Net;

public static class PersianWords
{
    public const char RleChar = (char)0x202B;
    public const char PopDirectionalFormatting = (char)0x202C;

    public static string? ToPersianString(this object value, bool enableRLE = false)
    {
        var str = value.ToString();
        if (string.IsNullOrEmpty(str))
            return str;

        var strOut = new StringBuilder();
        foreach (var ch in str)
        {
            if (ch >= 48 && ch <= 57)
                strOut.Append((char)(ch + 1728));
            else if (ch == 46)
                strOut.Append((char)47);
            else
                strOut.Append(ch);
        }

        var res = strOut.ToString().Replace("ي", "ی").Replace("ك", "ک");
        return enableRLE ? $"{RleChar}{res}{PopDirectionalFormatting}" : res;
    }

    /// <summary>
    /// Converts the Latin numbers in the input string to their Persian equivalents.
    /// </summary>
    /// <param name="num">The input string possibly containing Latin numbers.</param>
    /// <returns>
    /// A string where Latin numbers have been replaced with their Persian equivalents.
    /// </returns>
    public static string ToPersianNumber(this string num)
    {
        if (string.IsNullOrWhiteSpace(num))
            return string.Empty;

        return string.Create(num.Length, num, (chars, context) =>
        {
            for (var i = 0; i < num.Length; i++)
            {
                chars[i] = context[i] switch
                {
                    '0' => '\u06F0', // Persian zero
                    '\u0660' => '\u06F0', // Arabic-Indic zero
                    '1' => '\u06F1', // Persian one
                    '\u0661' => '\u06F1', // Arabic-Indic one
                    '2' => '\u06F2', // Persian two
                    '\u0662' => '\u06F2', // Arabic-Indic two
                    '3' => '\u06F3', // Persian three
                    '\u0663' => '\u06F3', // Arabic-Indic three
                    '4' => '\u06F4', // Persian four
                    '\u0664' => '\u06F4', // Arabic-Indic four
                    '5' => '\u06F5', // Persian five
                    '\u0665' => '\u06F5', // Arabic-Indic five
                    '6' => '\u06F6', // Persian six
                    '\u0666' => '\u06F6', // Arabic-Indic six
                    '7' => '\u06F7', // Persian seven
                    '\u0667' => '\u06F7', // Arabic-Indic seven
                    '8' => '\u06F8', // Persian eight
                    '\u0668' => '\u06F8', // Arabic-Indic eight
                    '9' => '\u06F9', // Persian nine
                    '\u0669' => '\u06F9', // Arabic-Indic nine
                    _ => context[i]
                };
            }
        });
    }

    /// <summary>
    /// Converts the Persian numbers in the input string to their Latin equivalents.
    /// </summary>
    /// <param name="num">The input string possibly containing Persian numbers.</param>
    /// <returns>
    /// A string where Persian numbers have been replaced with their Latin equivalents.
    /// </returns>
    public static string ToLatinNumber(this string num)
    {
        if (string.IsNullOrWhiteSpace(num))
            return string.Empty;

        return string.Create(num.Length, num, (chars, context) =>
        {
            for (var i = 0; i < num.Length; i++)
            {
                chars[i] = context[i] switch
                {
                    '\u06F0' => '0', //٠
                    '\u0660' => '0', //۰
                    '\u06F1' => '1', //١
                    '\u0661' => '1', //۱
                    '\u06F2' => '2', //٢
                    '\u0662' => '2', //۲
                    '\u06F3' => '3', //٣
                    '\u0663' => '3', //۳
                    '\u06F4' => '4', //۴
                    '\u0664' => '4', //٤
                    '\u06F5' => '5', //۵
                    '\u0665' => '5', //٥
                    '\u06F6' => '6', //۶
                    '\u0666' => '6', //٦
                    '\u06F7' => '7', //٧
                    '\u0667' => '7', //۷
                    '\u06F8' => '8', //٨
                    '\u0668' => '8', //۸
                    '\u06F9' => '9', //٩
                    '\u0669' => '9', //۹
                    _ => context[i]
                };
            }
        });
    }

}