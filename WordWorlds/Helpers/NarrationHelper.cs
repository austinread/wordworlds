using System.Text;
using Terminal.Gui;
using WordWorldsXML;
using WordWorldsXML.Models;

namespace WordWorlds.Helpers;

public class NarrationHelper
{
    public Label Narrator {get;private set;}
    public Label HintNorth {get;private set;}
    public Label HintNorthEast {get;private set;}
    public Label HintEast {get;private set;}
    public Label HintSouthEast {get;private set;}
    public Label HintSouth {get;private set;}
    public Label HintSouthWest {get;private set;}
    public Label HintWest {get;private set;}
    public Label HintNorthWest {get;private set;}

    private int hintSize = 25;

    public NarrationHelper()
    {
        Narrator = new Label()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Width = Dim.Percent(50),
            Height = Dim.Percent(50),
            TextAlignment = TextAlignment.Centered,
            VerticalTextAlignment = VerticalTextAlignment.Middle
        };

        #region Hints
        HintNorth = new Label()
        {
            X = Pos.Center(),
            Y = Pos.Percent(0),
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Centered,
            VerticalTextAlignment = VerticalTextAlignment.Top
        };
        HintNorthEast = new Label()
        {
            Y = Pos.Percent(0),
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Right,
            VerticalTextAlignment = VerticalTextAlignment.Top
        };
        HintEast = new Label()
        {
            Y = Pos.Center(),
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Right,
            VerticalTextAlignment = VerticalTextAlignment.Middle
        };
        HintSouthEast = new Label()
        {
            Y = Pos.AnchorEnd()-3,
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Right,
            VerticalTextAlignment = VerticalTextAlignment.Bottom
        };
        HintSouth = new Label()
        {
            X = Pos.Center(),
            Y = Pos.AnchorEnd()-3,
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Centered,
            VerticalTextAlignment = VerticalTextAlignment.Bottom
        };
        HintSouthWest = new Label()
        {
            X = Pos.Percent(0),
            Y = Pos.AnchorEnd()-3,
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Left,
            VerticalTextAlignment = VerticalTextAlignment.Bottom
        };
        HintWest = new Label()
        {
            X = Pos.Percent(0),
            Y = Pos.Center(),
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Left,
            VerticalTextAlignment = VerticalTextAlignment.Middle
        };
        HintNorthWest = new Label()
        {
            X = Pos.Percent(0),
            Y = Pos.Percent(0),
            Width = Dim.Sized(hintSize),
            TextAlignment = TextAlignment.Left,
            VerticalTextAlignment = VerticalTextAlignment.Top
        };
        #endregion
    }

    public void Narrate(string content, string? title = null, TextAlignment alignment = TextAlignment.Centered)
    {
        Narrator.Text = "";
        HintNorth.Text = "";
        HintNorthEast.Text = "";
        HintEast.Text = "";
        HintSouthEast.Text = "";
        HintSouth.Text = "";
        HintSouthWest.Text = "";
        HintWest.Text = "";
        HintNorthWest.Text = "";

        var sb = new StringBuilder();
        if (title != null)
        {
            sb.Append(title + "\n");
            sb.Append(new string('-', title.Length) + "\n");
        }
        sb.Append(content);

        Narrator.TextAlignment = alignment;
        Narrator.Text = sb.ToString();
    }

    public void NarrateWithRoomHints()
    {
        var currentRoom = ObjectManager.Instance.CurrentRoom;

        Narrate(currentRoom.Description, currentRoom.Name);
        HintNorth.Text = GetHint(currentRoom.NeighboringIDs[0], (x) => {return x + "\n↑";});
        HintNorthEast.Text = GetHint(currentRoom.NeighboringIDs[1], (x) => {return x + "\n↗";});
        HintEast.Text = GetHint(currentRoom.NeighboringIDs[2], (x) => {return "→ " + x;});
        HintSouthEast.Text = GetHint(currentRoom.NeighboringIDs[3], (x) => {return "↘\n" + x;});
        HintSouth.Text = GetHint(currentRoom.NeighboringIDs[4], (x) => {return "↓\n" + x;});
        HintSouthWest.Text = GetHint(currentRoom.NeighboringIDs[5], (x) => {return "↙\n"  + x;});
        HintWest.Text = GetHint(currentRoom.NeighboringIDs[6], (x) => {return x + " ←";});
        HintNorthWest.Text = GetHint(currentRoom.NeighboringIDs[7], (x) => {return x + "\n↖";});

        //Wierd fuckery to align the right-side hints
        HintNorthEast.X = Pos.AnchorEnd() - HintNorthEast.Text.Length+3;
        HintEast.X = Pos.AnchorEnd() - HintEast.Text.Length;
        HintSouthEast.X = Pos.AnchorEnd() - HintSouthEast.Text.Length+3;
    }

    /// <summary>
    /// Builds hint text for a given direction, taking into account discovery value and zone linking
    /// </summary>
    /// <param name="neighbor">Name of neighboring room</param>
    /// <param name="formatter">input: room text, output: hint text</param>
    /// <returns></returns>
    private string GetHint(string neighbor, Func<string, string> formatter)
    {
        var context = ObjectManager.Instance;
        
        if (neighbor != String.Empty)
        {
            Room? linkedRoom;
            if (neighbor.Contains("|"))
            {
                int indexOfDelimiter = neighbor.IndexOf(Constants.ZONE_ROOM_DELIMITER);
                string zone = neighbor.Substring(0,indexOfDelimiter);
                string room = neighbor.Substring(indexOfDelimiter+1);
                
                Zone neighborZone = context.LoadZone(zone);
                linkedRoom = neighborZone.Rooms.Where(r => r.Name == room).FirstOrDefault();
                neighbor = room;
            }
            else
            {
                linkedRoom = context.LoadedZone.Rooms.Where(r => r.Name == neighbor).FirstOrDefault();
            }
            if (linkedRoom == null)
                return "ERR";
            else if(!linkedRoom.Discovered)
                return formatter("???");
            else
                return formatter(neighbor);
        }
        else
        {
            return "";
        }
    }
}