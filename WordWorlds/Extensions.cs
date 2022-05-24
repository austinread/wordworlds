using NStack;

namespace WordWorlds;

public static class Extensions
{
    public static string ToNonNullString(this ustring ustr)
    {
        string? str = ustr.ToString();
        if (str == null)
            throw new Exception();
        else
            return str;
    }
}