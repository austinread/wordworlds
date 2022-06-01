namespace WordWorldsXML.Models;

public class Zone
{
    public Zone(Room initial)
    {
        Rooms = new List<Room>();
        Rooms.Add(initial);
        InitialRoomName = initial.Name;
    }
    public List<Room> Rooms {get;set;}
    public string InitialRoomName {get;set;}

    public Room? GetRoomByDirection(Room currentRoom, string directionStr, out string error)
    {
        error = "";
        Room? newRoom = null;
        if (Directions.Match(directionStr, out int direction))
        {
            string roomName = currentRoom.NeighboringIDs[direction];
            if (roomName == String.Empty)
                error = "There is nothing in that direction.";
            else
                newRoom = Rooms.Where(r => r.Name == roomName).First();
        }
        else
        {
            error = "Where are you going?";
        }

        return newRoom;
    }
}