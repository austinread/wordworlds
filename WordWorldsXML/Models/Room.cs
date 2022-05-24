namespace WordWorldsXML.Models;

public class Room
{
    public string Description {get;set;} = String.Empty;
    public bool Initial {get;set;} = false;

    public List<Item> Items {get;set;} = new List<Item>();
    public List<NPC> NPCs {get;set;} = new List<NPC>();


    public Item? GetItemByName(string name)
    {
        Item? matchingItem = Items.Where(i => i.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        return matchingItem;
    }

    public string GetChildDescriptionByName(string name)
    {
        Item? matchingItem = Items.Where(i => i.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        if (matchingItem != null)
            return matchingItem.Description;

        NPC? matchingNPC = NPCs.Where(n => n.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        if (matchingNPC != null)
            return matchingNPC.Description;

        return "There is nothing here by that name.";
    }
}