using WordWorldsXML.Models;

namespace WordWorldsXML;

public sealed class ObjectManager
{
    private static readonly ObjectManager instance = new ObjectManager();
    public static ObjectManager Instance => instance;

    public Game LoadedGame {get;private set;}
    public Zone LoadedZone {get;private set;}
    public Room CurrentRoom {get; set;}

    private ObjectManager()
    {
        LoadedGame = Loader.LoadGame();
        LoadedZone = LoadedGame.InitialZone;
        CurrentRoom = LoadedZone.InitialRoom;
    }
}