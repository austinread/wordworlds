using NStack;

public static class Extensions
{
    public static string ToNonNullString(this ustring ustr)
    {
        string? str = ustr.ToString();
        if (str == null)
            throw new Exception("Cannot convert null ustring to non-null string");
        else
            return str;
    }
}