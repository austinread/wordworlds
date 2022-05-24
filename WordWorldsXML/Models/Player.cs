namespace WordWorldsXML.Models;

public class Player
{
    public string Name {get;set;} = String.Empty;
    public List<Item> Inventory {get;set;} = new List<Item>();

    public string CharacterSummary => $@"
        Character Info
        --------------
        Name: {Name}
    ";

    public string InventorySummary => BuildInventorySummary();
    private string BuildInventorySummary()
    {
        string output =
        "Inventory\n"+
        "---------";
        Inventory.ForEach(item => {
            output += "\n- " + item.Name;
        });

        return output;
    }
}