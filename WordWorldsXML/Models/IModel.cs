using System.Xml.Linq;

namespace WordWorldsXML.Models;

interface IModel<T>
{
    string Name {get;set;}
    static T ParseFromXML(XElement xml) => throw new NotImplementedException();
}