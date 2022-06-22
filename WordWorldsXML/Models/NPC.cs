using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class NPC
{
    public string Name {get;set;} = String.Empty;
    public string Description {get;set;} = String.Empty;

    public static NPC ParseFromXML(XElement xml)
    {
        NPC npc = new NPC();
        npc.Name = xml.GetAttribute("Name");
        npc.Description = xml.GetAttribute("Description");
        return npc;
    }
}