using NStack;
using System.Text;
using Terminal.Gui;
using WordWorlds;
using WordWorldsXML;
using WordWorldsXML.Models;

Application.Init();
Player player = Loader.LoadPlayer();
Zone zone = Loader.LoadInitialZone();
Room room = zone.Rooms.Where(r => r.Name == zone.InitialRoomName).First();

var win = new Window("WordWorlds")
{
    X = 0,
    Y = 0,

    Width = Dim.Fill(),
    Height = Dim.Fill()
};

var narrator = new Label()
{
    X = Pos.Center(),
    Y = Pos.Center(),
    Width = Dim.Percent(50),
    Height = Dim.Percent(50),
    TextAlignment = TextAlignment.Centered,
    VerticalTextAlignment = VerticalTextAlignment.Middle
};
Narrate(room.Description, room.Name);

var terminal = new TextField("")
{
    X = 0,
    Y = Pos.AnchorEnd()-1,

    Width = Dim.Fill(),
};

terminal.KeyDown += (e) => 
{
    if (e.KeyEvent.Key == Key.Enter)
    {
        GameAction action = InputParser.Parse(terminal.Text, out ustring target);
        terminal.DeleteAll();
        bool actionOnly = ustring.IsNullOrEmpty(target);

        switch(action)
        {
            case GameAction.Help:
                string helpStr = @"
Commands:
- [H]elp: view help menu
- [C]haracter: view details about yourself
- [L]ook: observe your surroundings or something or someone in them
- [M]ove: move yourself to a neighboring location
- [T]ake: pick up an item
- [I]nventory: manage items you have
                ";
                Narrate(helpStr, alignment: TextAlignment.Left);
                break;

            case GameAction.Character:
                Narrate(player.CharacterSummary, alignment: TextAlignment.Left);
                break;

            case GameAction.Look:
                if(actionOnly)
                {
                    Narrate(room.Description, room.Name);
                }
                else
                {
                    string desc = room.GetChildDescriptionByName(target.ToNonNullString(), out string caseCorrectName);
                    Narrate(desc, caseCorrectName);
                }
                break;

            case GameAction.Move:
                if (actionOnly)
                {
                    Narrate("Where are you moving?");
                }
                else
                {
                    Room? newRoom = zone.GetRoomByDirection(room, target.ToNonNullString(), out string error);
                    if(newRoom == null)
                    {
                        Narrate(error);
                    }
                    else
                    {
                        room = newRoom;
                        Narrate(room.Description, room.Name);
                    }
                }
                break;

            case GameAction.Take:
                if (actionOnly)
                {
                    Narrate("What are you taking?");
                }
                else
                {
                    Item? item = room.GetItemByName(target.ToNonNullString());
                    if (item == null)
                    {
                        Narrate("There is no object here by that name.");
                    }
                    else
                    {
                        player.Inventory.Add(item);
                        Narrate($"You picked up the {item.Name}");
                    }
                }
                break;

            case GameAction.Inventory:
                Narrate(player.InventorySummary, alignment: TextAlignment.Left);
                break;

            case GameAction.Empty:
                break;

            case GameAction.Undefined:
            default:
                Narrate("Command unrecognized, type Help to see available commands.");
                break;
        }
    }
};
win.Add(narrator, terminal);
Application.Top.Add(win);
Application.Run();
Application.Shutdown();

void Narrate(string content, string? title = null, TextAlignment alignment = TextAlignment.Centered)
{
    if (narrator == null)
        return;
    
    narrator.Text = "";

    var sb = new StringBuilder();
    if (title != null)
    {
        sb.Append(title + "\n");
        sb.Append(new string('-', title.Length) + "\n");
    }
    sb.Append(content);

    narrator.TextAlignment = alignment;
    narrator.Text = sb.ToString();
}