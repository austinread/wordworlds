using System.Xml.Linq;

namespace WordWorldsXML.Models;

public class Zone : IModel<Zone>
{
    //Assumed to be unique
    public string Name {get;set;} = String.Empty;
    public string FileName {get;set;} = String.Empty;
    public List<Room> Rooms {get;set;} = new List<Room>();
    public string InitialRoomName {get;set;} = String.Empty;
    public Room InitialRoom => Rooms.Where(r => r.Name == InitialRoomName).Single();

    public static Zone ParseFromXML(XElement xml, string fileName)
    {
        Zone zone = new Zone();
        zone.FileName = fileName;
        zone.Name = xml.GetAttribute("Name");
        zone.InitialRoomName = xml.GetAttribute("Start");
        foreach (var roomXML in xml.Descendants("Rooms")
            .Single()
            .Descendants("Room"))
        {
            zone.Rooms.Add(Room.ParseFromXML(roomXML, fileName));
        }
        return zone;
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