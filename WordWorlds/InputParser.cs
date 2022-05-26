using NStack;

namespace WordWorlds;

public class InputParser
{
    private static ustring[] Help = new ustring[] { "h", "help" };
    private static ustring[] Character = new ustring[] { "c", "character" };
    private static ustring[] Look = new ustring[] { "l", "look" };
    private static ustring[] Move = new ustring[] { "m", "move" };
    private static ustring[] Take = new ustring[] { "t", "take" };
    private static ustring[] Inventory = new ustring[] { "i", "inventory" };

    public static GameAction Parse(ustring input, out ustring target)
    {
        target = "";

        if (input.Length < 1)
            return GameAction.Empty;

        ustring[] args = input.Split(" ");
        if (args.Length > 1)
            target = args[1];

        ustring command = args[0];

        if (Help.Contains(command))
            return GameAction.Help;
        else if (Character.Contains(command))
            return GameAction.Character;
        else if (Look.Contains(command))
            return GameAction.Look;
        else if (Move.Contains(command))
            return GameAction.Move;
        else if (Take.Contains(command))
            return GameAction.Take;
        else if (Inventory.Contains(command))
            return GameAction.Inventory;
        else
            return GameAction.Undefined;
    }
}

public enum GameAction
{
    Help,
    Character,
    Look,
    Move,
    Take,
    Inventory,
    Empty,
    Undefined
}