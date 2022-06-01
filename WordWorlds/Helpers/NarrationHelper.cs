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

    public void NarrateWithRoomHints(Room room)
    {
        Narrate(room.Description, room.Name);
        HintNorth.Text = room.NeighboringIDs[0] != String.Empty ? room.NeighboringIDs[0] + "\n^" : "";
        HintNorthEast.Text = room.NeighboringIDs[1] != String.Empty ? room.NeighboringIDs[1] + "\n^>" : "";
        HintEast.Text = room.NeighboringIDs[2] != String.Empty ? "> " + room.NeighboringIDs[2] : "";
        HintSouthEast.Text = room.NeighboringIDs[3] != String.Empty ? "V>\n" + room.NeighboringIDs[3] : "";
        HintSouth.Text = room.NeighboringIDs[4] != String.Empty ? "V\n" + room.NeighboringIDs[4] : "";
        HintSouthWest.Text = room.NeighboringIDs[5] != String.Empty ? "V<\n" + room.NeighboringIDs[5] : "";
        HintWest.Text = room.NeighboringIDs[6] != String.Empty ? room.NeighboringIDs[6] + " <" : "";
        HintNorthWest.Text = room.NeighboringIDs[7] != String.Empty ? room.NeighboringIDs[7] + "\n^<" : "";

        HintNorthEast.X = Pos.AnchorEnd() - HintNorthEast.Text.Length+3;
        HintEast.X = Pos.AnchorEnd() - HintEast.Text.Length;
        HintSouthEast.X = Pos.AnchorEnd() - HintSouthEast.Text.Length+3;
    }
}