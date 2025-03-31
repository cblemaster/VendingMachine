
using System.Text.Json;
using VendingMachine.Snacks;

namespace VendingMachine;

internal sealed class LoadSnacks
{
    internal string ReadInventoryToString()
    {
        string currentDirectory = Environment.CurrentDirectory;
        string fullInventoryPath = Path.Combine(currentDirectory, @"..\..\..\", "inventory.json");  // TODO: magic string

        using StreamReader sr = new(fullInventoryPath);
        return sr.ReadToEnd();
    }

    internal IEnumerable<SnackJsonModel> DeserializeInventoryString(string json)
    {
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<IEnumerable<SnackJsonModel>>(json, options) ?? [];
    }
}
