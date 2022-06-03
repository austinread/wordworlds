namespace WordWorldsXML;

public class Directions 
{
    private static Direction North => new Direction
    {
        CompassDirection = 0,
        Name = "NORTH",
        ShortName = "N"
    };
    private static Direction NorthEast => new Direction
    {
        CompassDirection = 1,
        Name = "NORTHEAST",
        ShortName = "NE"
    };
    private static Direction East => new Direction
    {
        CompassDirection = 2,
        Name = "EAST",
        ShortName = "E"
    };
    private static Direction SouthEast => new Direction
    {
        CompassDirection = 3,
        Name = "SOUTHEAST",
        ShortName = "SE"
    };
    private static Direction South => new Direction
    {
        CompassDirection = 4,
        Name = "SOUTH",
        ShortName = "S"
    };
    private static Direction SouthWest => new Direction
    {
        CompassDirection = 5,
        Name = "SOUTHWEST",
        ShortName = "S"
    };
    private static Direction West => new Direction
    {
        CompassDirection = 6,
        Name = "WEST",
        ShortName = "W"
    };
    private static Direction NorthWest => new Direction
    {
        CompassDirection = 7,
        Name = "NORTHWEST",
        ShortName = "NW"
    };

    private static List<Direction> All => new List<Direction> { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    public static bool Match(string input, out int direction)
    {
        direction = 0;
        var match = All.Where(d => d.Name == input.ToUpper() || d.ShortName == input.ToUpper()).FirstOrDefault();
        if (match != null)
            direction = match.CompassDirection;
        
        return match != null;
    }

    private class Direction
    {
        public int CompassDirection;    //0-8
        public string Name = "";
        public string ShortName = "";
    }
}
