namespace WordWorldsXML.Models;

public class Game
{
    public string Name {get;set;}
    public List<Zone> Zones {get;set;}
    public string InitialZoneName {get;set;}
    public Zone InitialZone => Zones.Where(z => z.Name == InitialZoneName).Single();
    public Game(string name, Zone initial)
    {
        Name = name;
        Zones = new List<Zone>();
        Zones.Add(initial);
        InitialZoneName = initial.Name;
    }
}