using NStack;
using Terminal.Gui;
using WordWorlds;
using WordWorldsXML;
using WordWorldsXML.Models;

Application.Init();
Player player = Loader.LoadPlayer();
Zone zone = Loader.LoadInitialZone();
Room room = zone.Rooms.Where(r => r.Initial).First();

var win = new Window("WordWorlds")
{
    X = 0,
    Y = 1,

    Width = Dim.Fill(),
    Height = Dim.Fill()
};

var menu = new MenuBar(new MenuBarItem[]{
    new MenuBarItem("_File", new MenuItem[]{
        new MenuItem("_Quit", "", () => {if (Quit()) Application.Top.Running = false;})
    })
});

static bool Quit()
{
    var choice = MessageBox.Query(50, 7, "Quit?", "Are you sure you want to quit?", "Yes", "No");
    return choice == 0;
}
var narrator = new Label(room.Description)
{
    X = Pos.Center(),
    Y = Pos.Center()
};
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
                narrator.Text = @"
                Commands:
                - [H]elp: view help menu
                - [C]haracter: view details about yourself
                - [L]ook: observe your surroundings or something or someone in them
                - [T]ake: pick up an item
                - [I]nventory: manage items you have
                ";
                break;

            case GameAction.Character:
                narrator.Text = player.CharacterSummary;
                break;

            case GameAction.Look:
                if(actionOnly)
                    narrator.Text = room.Description;
                else
                    narrator.Text = room.GetChildDescriptionByName(target.ToNonNullString());
                break;

            case GameAction.Take:
                if (actionOnly)
                {
                    narrator.Text = "What are you taking?";
                }
                else
                {
                    Item? item = room.GetItemByName(target.ToNonNullString());
                    if (item == null)
                    {
                        narrator.Text = "There is no object here by that name.";
                    }
                    else
                    {
                        player.Inventory.Add(item);
                        narrator.Text = $"You picked up the {item.Name}";
                    }
                }
                break;

            case GameAction.Inventory:
                narrator.Text = player.InventorySummary;
                break;

            case GameAction.Empty:
                break;

            case GameAction.Undefined:
            default:
                narrator.Text = "Command unrecognized, type Help to see available commands.";
                break;
        }
    }
};
win.Add(narrator, terminal);
Application.Top.Add(menu, win);
Application.Run();
Application.Shutdown();