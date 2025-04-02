
using System.Text;
using System.Text.Json;

namespace VendingMachine;

internal sealed class Inventory
{
    private readonly string[] _validIdentifiers = ["A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4"];
    private readonly SnackSlot[] _snackSlots;

    internal SnackSlot[] SnackSlots => _snackSlots;

    internal Inventory()
    {
        IEnumerable<SnackJsonModel> jsonSnacks = GetSnacksFromInventory();
        FillSnackSlots(jsonSnacks);
        _snackSlots = [];
    }

    internal static IEnumerable<SnackJsonModel> GetSnacksFromInventory()
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

    internal void FillSnackSlots(IEnumerable<SnackJsonModel> jsonSnacks)
    {
        int index = 0;
        foreach (string identifier in _validIdentifiers)
        {
            SnackSlot snackSlot = new(identifier);
            SnackJsonModel jsonSnack = jsonSnacks.ElementAt(index);
            Snack snack = null!;
            decimal price = jsonSnack.Price;
            string label = jsonSnack.Label;

            switch (jsonSnack.Type)
            {
                case "Chips":
                    snack = new Chips(price, label);
                    break;
                case "Crackers":
                    snack = new Crackers(price, label);
                    break;
                case "Drink":
                    snack = new Drink(price, label);
                    break;
                case "Pastry":
                    snack = new Pastry(price, label);
                    break;
            }

            Snack[] snackArray = [.. Enumerable.Repeat(snack, SnackSlot.SNACK_SLOT_CAPACITY)];
            snackSlot.AddSnacks(snackArray);
            index++;
        }
    }

    internal string DisplaySnacks()
    {
        StringBuilder sb = new();

        if (SnackSlots.All(s => s.Snacks.Count == 0))
        {
            sb.AppendLine("VendTron is sold out of snacks...");
        }
        else
        {
            sb.AppendLine("SNACKS AVAILABLE...");
            foreach (SnackSlot snackSlot in SnackSlots)
            {
                if (snackSlot.Snacks.Count == 0)
                {
                    sb.Append($"{snackSlot.Identifier}\t\tSold out!");
                }
                else
                {
                    sb.AppendLine(snackSlot.ToDisplayString + $"\t\t{snackSlot.Snacks.Count}");
                }
            }
        }
        return sb.ToString();
    }
}
