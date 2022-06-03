namespace WordWorldsXML.Models;

public class Room
{
    //Assumed to be unique
    public string Name {get;set;}
    public string Description {get;set;} = String.Empty;
    public bool Discovered {get;set;} = true;

    //Clockwise: 0=N, 1=NE, etc...
    public string[] NeighboringIDs {get;set;}  = new string[8]{String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty};
    public List<Item> Items {get;set;} = new List<Item>();
    public List<NPC> NPCs {get;set;} = new List<NPC>();

    public Room(string name)
    {
        Name = name;
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