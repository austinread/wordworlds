using NStack;
using Terminal.Gui;
using WordWorlds.Helpers;
using WordWorldsXML;
using WordWorldsXML.Models;


var context = ObjectManager.Instance;
context.DoInitialLoad();

Application.Init();
var narrationHelper = new NarrationHelper();

var win = new Window(context.LoadedGame.Name)
{
    X = 0,
    Y = 0,

    Width = Dim.Fill(),
    Height = Dim.Fill()
};

narrationHelper.NarrateWithRoomHints();

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
                narrationHelper.Narrate(helpStr, alignment: TextAlignment.Left);
                break;

            case GameAction.Character:
                narrationHelper.Narrate(context.LoadedPlayer.CharacterSummary, alignment: TextAlignment.Left);
                break;

            case GameAction.Look:
                if(actionOnly)
                {
                    narrationHelper.NarrateWithRoomHints();
                }
                else
                {
                    string desc = context.CurrentRoom.GetChildDescriptionByName(target.ToNonNullString(), out string caseCorrectName);
                    narrationHelper.Narrate(desc, caseCorrectName);
                }
                break;

            case GameAction.Move:
                if (actionOnly)
                {
                    narrationHelper.Narrate("Where are you moving?");
                }
                else
                {
                    Room? newRoom = context.LoadedZone.GetRoomByDirection(context.CurrentRoom, target.ToNonNullString(), out string error);
                    if(newRoom == null)
                    {
                        narrationHelper.Narrate(error);
                    }
                    else
                    {
                        context.CurrentRoom = newRoom;
                        if (!context.CurrentRoom.Discovered) context.CurrentRoom.Discovered = true;
                        narrationHelper.NarrateWithRoomHints();
                    }
                }
                break;

            case GameAction.Take:
                if (actionOnly)
                {
                    narrationHelper.Narrate("What are you taking?");
                }
                else
                {
                    Item? item = context.CurrentRoom.GetItemByName(target.ToNonNullString());
                    if (item == null)
                    {
                        narrationHelper.Narrate("There is no object here by that name.");
                    }
                    else if (!item.Takeable)
                    {
                        narrationHelper.Narrate("You can't pick that up.");
                    }
                    else
                    {
                        context.CurrentRoom.TakeItem(item);
                        narrationHelper.Narrate($"You picked up the {item.Name}");
                    }
                }
                break;

            case GameAction.Inventory:
                narrationHelper.Narrate(context.LoadedPlayer.InventorySummary, alignment: TextAlignment.Left);
                break;

            case GameAction.Empty:
                break;

            case GameAction.Undefined:
            default:
                narrationHelper.Narrate("Command unrecognized, type Help to see available commands.");
                break;
        }
    }
};
win.Add(
    narrationHelper.Narrator, 
    terminal, 
    narrationHelper.HintNorth,
    narrationHelper.HintNorthEast,
    narrationHelper.HintEast,
    narrationHelper.HintSouthEast,
    narrationHelper.HintSouth,
    narrationHelper.HintSouthWest,
    narrationHelper.HintWest,
    narrationHelper.HintNorthWest
);
Application.Top.Add(win);
Application.Run();
Application.Shutdown();