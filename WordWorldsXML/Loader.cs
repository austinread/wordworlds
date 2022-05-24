using WordWorldsXML.Models;

namespace WordWorldsXML;

public static class Loader
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

        Room start = new Room
        { 
            Description = "Your office is tastefully decorated with empty space, a desk, and a dog.", 
            Initial = true,
            Items = startItems,
            NPCs = startNPCs
        };
        return new Zone(start);
    }
}