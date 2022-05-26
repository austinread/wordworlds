using WordWorldsXML.Models;

namespace WordWorldsXML;

public class Loader
{
    public static Player LoadPlayer()
    {
        Player player = new Player{ Name = "Biggus Dickus" };
        return player;
    }
    public static Zone LoadInitialZone()
    {
        //Here be test data
        List<Item> startItems = new List<Item>{ new Item { Name = "Desk", Description = "A Mid-Century classic masterpiece, ruined by terrible decor. " }};
        List<NPC> startNPCs = new List<NPC>{ new NPC { Name = "Dog", Description = "What a good pup." }};

        Room office = new Room("Office")
        { 
            Description = "Your office is tastefully decorated with empty space, a desk, and a dog.",
            Items = startItems,
            NPCs = startNPCs,
        };
        office.NeighboringIDs[6] = "Kitchen";

        Room kitchen = new Room("Kitchen")
        { 
            Description = "Your kitchen contains nothing but a hole in the ground.  It stretches so far down, you cannot see the bottom."
        };
        kitchen.NeighboringIDs[2] = "Office";

        var zone = new Zone(office);
        zone.Rooms.Add(kitchen);

        return zone;
    }
}