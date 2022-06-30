using System.Configuration;
using System.Xml.Linq;
using WordWorldsXML.Models;

namespace WordWorldsXML;

public sealed class ObjectManager
{
    private static readonly ObjectManager instance = new ObjectManager();
    public static ObjectManager Instance => instance;

    private Game? loadedGame;
    private Player? loadedPlayer;
    private Zone? loadedZone;
    private Room? currentRoom;

    private string DataPath {get;set;}
    public Game LoadedGame 
    {
        get {if (loadedGame != null) return loadedGame; else throw new Exception("No game loaded");} 
        set{loadedGame = value;}
    }
    public Player LoadedPlayer 
    {
        get {if (loadedPlayer != null) return loadedPlayer; else throw new Exception("No player loaded");} 
        set{loadedPlayer = value;}
    }
    public Zone LoadedZone 
    {
        get {if (loadedZone != null) return loadedZone; else throw new Exception("No zone loaded");} 
        set{loadedZone = value;}
    }
    public Room CurrentRoom 
    {
        get {if (currentRoom != null) return currentRoom; else throw new Exception("No room loaded");} 
        set{currentRoom = value;}
    }

    private ObjectManager()
    {
        var appSettings = ConfigurationManager.AppSettings;
        string? dataPath = appSettings["GamePath"];
        if (dataPath == null)
            //TODO: user friendly error reporting
            throw new Exception("Game Data Path not configured");
        
        DataPath = dataPath;
    }

    public void DoInitialLoad()
    {
        LoadedGame = LoadGame();
        LoadedPlayer = LoadPlayer();
        LoadedZone = LoadZone(LoadedGame.InitialZoneName);
        CurrentRoom = LoadedZone.InitialRoom;
    }

    private Player LoadPlayer()
    {
        XElement playerXML = XElement.Load(DataPath + Constants.PLAYER_FILENAME);
        return Player.ParseFromXML(playerXML);
    }
    private Game LoadGame()
    {
        XElement gameXML = XElement.Load(DataPath + Constants.GAME_FILENAME);
        return Game.ParseFromXML(gameXML);
    }
    private Zone LoadZone(string name)
    {
        XElement zoneXML = XElement.Load($"{DataPath}/{Constants.ZONES_FOLDER}/{name}.xml");
        return Zone.ParseFromXML(zoneXML);
    }
    public Item LoadItem(string name)
    {
        XElement itemXML = XElement.Load($"{DataPath}/{Constants.ITEMS_FOLDER}/{name}.xml");
        return Item.ParseFromXML(itemXML);
    }
    public NPC LoadNPC(string name)
    {
        XElement npcXML = XElement.Load($"{DataPath}/{Constants.NPCS_FOLDER}/{name}.xml");
        return NPC.ParseFromXML(npcXML);
    }
}