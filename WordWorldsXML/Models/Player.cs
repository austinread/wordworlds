using System.Text;

namespace WordWorldsXML.Models;

public class Player
{
    public string Name {get;set;} = String.Empty;
    public List<Item> Inventory {get;set;} = new List<Item>();

    public string InventorySummary => BuildInventorySummary();
    public string CharacterSummary => BuildCharacterSummary();

    public string BuildCharacterSummary()
    {
        var sb = new StringBuilder();

        sb.Append("Character Info\n");
        sb.Append("--------------\n");
        sb.Append("Name: " + Name);

        return sb.ToString();
    }
        

    private string BuildInventorySummary()
    {
        var sb = new StringBuilder();

        sb.Append("Inventory\n");
        sb.Append("---------");

        Inventory.ForEach(item => {
            sb.Append("\n- " + item.Name);
        });

        return sb.ToString();
    }
}