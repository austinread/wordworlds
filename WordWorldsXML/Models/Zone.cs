namespace WordWorldsXML.Models;

public class Zone
{
    public Zone(Room initial)
    {
        Rooms = new List<Room>();
        Rooms.Add(initial);
    }
    public List<Room> Rooms {get;set;}
}