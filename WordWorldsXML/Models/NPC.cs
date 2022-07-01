using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class NPC : IModel<NPC>
{
    public string Name {get;set;} = String.Empty;
    public string FileName {get;set;} = String.Empty;
    public string Description {get;set;} = String.Empty;

    public static NPC ParseFromXML(XElement xml, string fileName)
    {
        NPC npc = new NPC();
        npc.FileName = fileName;
        npc.Name = xml.GetAttribute("Name");
        npc.Description = xml.GetAttribute("Description");
        return npc;
    }
}