using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class Game : IModel<Game>
{
    public string Name {get;set;} = String.Empty;
    public string FileName {get;set;} = String.Empty;
    public string InitialZoneName {get;set;} = String.Empty;

    public static Game ParseFromXML(XElement xml, string fileName)
    {
        Game game = new Game();
        game.FileName = fileName;
        game.Name = xml.GetAttribute("Name");
        game.InitialZoneName = xml.GetAttribute("Start");
        return game;
    }
}