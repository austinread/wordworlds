using System.Configuration;
using WordWorldsXML.Models;

namespace WordWorldsXML;

public sealed class ObjectManager
{
    private static readonly ObjectManager instance = new ObjectManager();
    public static ObjectManager Instance => instance;

    public Game LoadedGame {get;private set;}
    public Player LoadedPlayer {get;private set;}
    public Zone LoadedZone {get;private set;}
    public Room CurrentRoom {get; set;}

    private ObjectManager()
    {
        var appSettings = ConfigurationManager.AppSettings;
        string? dataPath = appSettings["GamePath"];
        if (dataPath == null)
            //TODO: user friendly error reporting
            throw new Exception("Game Data Path not configured");
        
        LoadedGame = Loader.LoadGame();
        LoadedPlayer = Loader.LoadPlayer(dataPath);
        LoadedZone = LoadedGame.InitialZone;
        CurrentRoom = LoadedZone.InitialRoom;
    }
}