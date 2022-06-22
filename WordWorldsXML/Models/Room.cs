using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class Room : IModel<Room>
{
    //Assumed to be unique
    public string Name {get;set;} = String.Empty;
    public string Description {get;set;} = String.Empty;
    public bool Discovered {get;set;} = true;

    //Clockwise: 0=N, 1=NE, etc...
    public string[] NeighboringIDs {get;set;}  = new string[8]{String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty};
    public List<Item> Items {get;set;} = new List<Item>();
    public List<NPC> NPCs {get;set;} = new List<NPC>();

    public static Room ParseFromXML(XElement xml)
    {
        Room room = new Room();

        room.Name = xml.GetAttribute("Name");
        room.Description = xml.GetAttribute("Description");
        room.Discovered = xml.GetBoolean("Discovered");

        var neighborsXML = xml.Descendants("Neighbors").SingleOrDefault();
        if (neighborsXML != null)
        {
            foreach (var neighborXML in neighborsXML.Descendants("Neighbor"))
            {
                if (!Directions.Match(neighborXML.GetAttribute("Direction"), out int i))
                    continue;

                room.NeighboringIDs[i] = neighborXML.GetAttribute("Name");
            }
        }

        var itemsXML = xml.Descendants("Items").SingleOrDefault();
        if (itemsXML != null)
        {
            var context = ObjectManager.Instance;
            foreach (var itemXML in itemsXML.Descendants("Item"))
                room.Items.Add(context.LoadItem(itemXML.GetAttribute("Name")));
        }
        var npcsXML = xml.Descendants("NPCs").SingleOrDefault();
        if (npcsXML != null)
        {
            var context = ObjectManager.Instance;
            foreach (var npcXML in npcsXML.Descendants("NPC"))
                room.NPCs.Add(context.LoadNPC(npcXML.GetAttribute("Name")));
        }

        return room;
    }

    public Item? GetItemByName(string name)
    {
        Item? matchingItem = Items.Where(i => i.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        return matchingItem;
    }

    public string GetChildDescriptionByName(string name, out string caseCorrectName)
    {
        caseCorrectName = String.Empty;
        
        Item? matchingItem = Items.Where(i => i.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        if (matchingItem != null)
        {
            caseCorrectName = matchingItem.Name;
            return matchingItem.Description;
        }

        NPC? matchingNPC = NPCs.Where(n => n.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        if (matchingNPC != null)
        {
            caseCorrectName = matchingNPC.Name;
            return matchingNPC.Description;
        }

        return "There is nothing here by that name.";
    }
}