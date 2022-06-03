namespace WordWorldsXML.Models;

public class Zone
{
    //Assumed to be unique
    public string Name {get;set;}
    public List<Room> Rooms {get;set;}
    public string InitialRoomName {get;set;}
    public Room InitialRoom => Rooms.Where(r => r.Name == InitialRoomName).Single();

    public Zone(string name, Room initial)
    {
        Name = name;
        Rooms = new List<Room>();
        Rooms.Add(initial);
        InitialRoomName = initial.Name;
    }

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