using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class Game : IModel<Game>
{
    public string Name {get;set;} = String.Empty;
    public string InitialZoneName {get;set;} = String.Empty;

    public static Game ParseFromXML(XElement xml)
    {
        Game game = new Game();
        game.Name = xml.GetAttribute("Name");
        game.InitialZoneName = xml.GetAttribute("Start");
        return game;
    }
}