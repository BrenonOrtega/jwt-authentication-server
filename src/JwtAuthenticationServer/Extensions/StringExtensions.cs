namespace Awarean.Sdk.Utils;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string @string) => string.IsNullOrEmpty(@string) || string.IsNullOrWhiteSpace(@string);
}