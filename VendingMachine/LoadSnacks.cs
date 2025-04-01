
using System.Text.Json;
using VendingMachine.Snacks;

namespace VendingMachine;

internal sealed class LoadSnacks
{
    internal IEnumerable<SnackJsonModel> GetSnacksFromInventory()
    {
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<IEnumerable<SnackJsonModel>>(ReadInventoryToString(), options) ?? [];

        static string ReadInventoryToString()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string fullInventoryPath = Path.Combine(currentDirectory, @"..\..\..\", "inventory.json");  // TODO: magic string

            using StreamReader sr = new(fullInventoryPath);
            return sr.ReadToEnd();
        }
    }
}
