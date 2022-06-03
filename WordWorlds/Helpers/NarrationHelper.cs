using System.Text;
using Terminal.Gui;
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

    public void NarrateWithRoomHints(Room thisRoom, Zone thisZone)
    {
        Narrate(thisRoom.Description, thisRoom.Name);
        HintNorth.Text = GetHint(thisRoom.NeighboringIDs[0], thisZone, (x) => {return x + "\n^";});
        HintNorthEast.Text = GetHint(thisRoom.NeighboringIDs[1], thisZone, (x) => {return x + "\n^>";});
        HintEast.Text = GetHint(thisRoom.NeighboringIDs[2], thisZone, (x) => {return "> " + x;});
        HintSouthEast.Text = GetHint(thisRoom.NeighboringIDs[3], thisZone, (x) => {return "V>\n" + x;});
        HintSouth.Text = GetHint(thisRoom.NeighboringIDs[4], thisZone, (x) => {return "V\n" + x;});
        HintSouthWest.Text = GetHint(thisRoom.NeighboringIDs[5], thisZone, (x) => {return "V<\n"  + x;});
        HintWest.Text = GetHint(thisRoom.NeighboringIDs[6], thisZone, (x) => {return x + " <";});
        HintNorthWest.Text = GetHint(thisRoom.NeighboringIDs[7], thisZone, (x) => {return x + "\n^<";});

        //Wierd fuckery to align the right-side hints
        HintNorthEast.X = Pos.AnchorEnd() - HintNorthEast.Text.Length+3;
        HintEast.X = Pos.AnchorEnd() - HintEast.Text.Length;
        HintSouthEast.X = Pos.AnchorEnd() - HintSouthEast.Text.Length+3;
    }

    /// <summary>
    /// Builds hint text for a given direction, taking into account discovery value and zone linking
    /// </summary>
    /// <param name="neighbor">Name of neighboring room</param>
    /// <param name="thisZone"></param>
    /// <param name="formatter">input: room text, output: hint text</param>
    /// <returns></returns>
    private string GetHint(string neighbor, Zone thisZone, Func<string, string> formatter)
    {
        if (neighbor != String.Empty)
        {
            //TODO: rooms in other zones
            Room? linkedRoom = thisZone.Rooms.Where(r => r.Name == neighbor).FirstOrDefault();
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