using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class Item: IModel<Item>
{
    public string Name {get;set;} = String.Empty;
    public string FileName {get;set;} = String.Empty;
    public string Description {get;set;} = String.Empty;
    public bool Takeable {get;set;} = false;

    public static Item ParseFromXML(XElement xml, string fileName)
    {
        Item item = new Item();

        item.FileName = fileName;
        item.Name = xml.GetAttribute("Name");
        item.Description = xml.GetAttribute("Description");
        if (xml.Attribute("Takeable") != null)
            item.Takeable = xml.GetBoolean("Takeable");

        return item;
    }
}