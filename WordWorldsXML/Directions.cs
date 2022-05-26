namespace WordWorldsXML;

public class Directions 
{
    public static Direction North => new Direction
    {
        CompassDirection = 0,
        Name = "NORTH",
        ShortName = "N"
    };
    public static Direction NorthEast => new Direction
    {
        CompassDirection = 1,
        Name = "NORTHEAST",
        ShortName = "NE"
    };
    public static Direction East => new Direction
    {
        CompassDirection = 2,
        Name = "EAST",
        ShortName = "E"
    };
    public static Direction SouthEast => new Direction
    {
        CompassDirection = 3,
        Name = "SOUTHEAST",
        ShortName = "SE"
    };
    public static Direction South => new Direction
    {
        CompassDirection = 4,
        Name = "SOUTH",
        ShortName = "S"
    };
    public static Direction SouthWest => new Direction
    {
        CompassDirection = 5,
        Name = "SOUTHWEST",
        ShortName = "S"
    };
    public static Direction West => new Direction
    {
        CompassDirection = 6,
        Name = "WEST",
        ShortName = "W"
    };
    public static Direction NorthWest => new Direction
    {
        CompassDirection = 7,
        Name = "NORTHWEST",
        ShortName = "NW"
    };

    public static List<Direction> All => new List<Direction> { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    public static bool Match(string input, out int direction)
    {
        direction = 0;
        var match = All.Where(d => d.Name == input.ToUpper() || d.ShortName == input.ToUpper()).FirstOrDefault();
        if (match != null)
            direction = match.CompassDirection;
        
        return match != null;
    }
}

public class Direction
{
    public int CompassDirection;    //0-8
    public string Name = "";
    public string ShortName = "";
}