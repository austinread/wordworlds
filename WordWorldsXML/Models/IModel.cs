using System.Xml.Linq;

namespace WordWorldsXML.Models;

public interface IModel<T>
{
    string Name {get;set;}
    string FileName {get;set;}
    static T ParseFromXML(XElement xml, string fileName) => throw new NotImplementedException();
}