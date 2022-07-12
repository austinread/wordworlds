using System.Xml.Linq;
using WordWorldsXML.Models;

public static class Extensions
{
    public static string GetTruncatedFileName<T>(this IModel<T> m){
        return m.FileName.Substring(m.FileName.LastIndexOf("/")+1,m.FileName.Length-m.FileName.LastIndexOf("/")-5);
    }
    public static string GetAttribute(this XElement element, string attribute)
    {
        string? val = (string?)element.Attribute(attribute);
        return (val == null) ? "" : val;
    }
    public static bool GetBoolean(this XElement element, string attribute)
    {
        string val = GetAttribute(element, attribute);
        return val.ToUpper() == "TRUE";
    }
}