using WordWorldsXML.Models;

namespace WordWorldsXML;

public class Loader
{
    public static Player LoadPlayer()
    {
        Player player = new Player{ Name = "Biggus Dickus" };
        return player;
    }
    public static Game LoadGame()
    {
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
            Description = "Your kitchen contains nothing but a hole in the ground.  It stretches so far down, you cannot see the bottom.",
            Discovered = false
        };
        kitchen.NeighboringIDs[2] = "Office";

        var house = new Zone("House", office);
        house.Rooms.Add(kitchen);

        var game = new Game("Test-Game 9000", house);

        return game;
    }
}