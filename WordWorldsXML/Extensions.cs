using System.Xml.Linq;

public static class Extensions
{
    public static string GetAttribute(this XElement element, string attribute)
    {
        string? val = (string?)element.Attribute(attribute);
        return (val == null) ? "" : val;
    }
}