﻿
using System.Text;
using System.Text.Json;
using VendingMachine.Domain.Snacks;

namespace VendingMachine.Domain.Inventory;

internal sealed class Inventory
{
    private const string INVENTORY_FILE_NAME = "inventory.json";
    private readonly string[] _validIdentifiers = ["A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4"];
    private SnackSlot[] _snackSlots = [];

    internal IReadOnlyCollection<SnackSlot> SnackSlots => _snackSlots.AsReadOnly();

    internal Inventory() => FillSnackSlots();

    private void FillSnackSlots()
    {
        List<SnackSlot> snackSlots = [];
        IEnumerable<SnackJsonModel> jsonSnacks = GetSnacksFromInventory();

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
            snackSlots.Add(snackSlot);
            index++;
        }
        _snackSlots = [.. snackSlots];

        static IEnumerable<SnackJsonModel> GetSnacksFromInventory()
        {
            return JsonSerializer.Deserialize<IEnumerable<SnackJsonModel>>(ReadInventoryToString(), Options()) ?? [];

            static JsonSerializerOptions Options() => new() { PropertyNameCaseInsensitive = true };
            static string ReadInventoryToString()
            {
                string currentDirectory = Environment.CurrentDirectory;
                string fullInventoryPath = Path.Combine(currentDirectory, @"..\..\..\", INVENTORY_FILE_NAME);

                using StreamReader sr = new(fullInventoryPath);
                return sr.ReadToEnd();
            }
        }
    }

    internal string DisplaySnacks()
    {
        StringBuilder sb = new();

        if (SnackSlots.All(s => s.Snacks.Count == 0))
        {
            sb = sb.AppendLine("VendTron is sold out of snacks...");
        }
        else
        {
            sb = sb.AppendLine("SNACKS AVAILABLE...");
            foreach (SnackSlot snackSlot in SnackSlots)
            {
                sb = snackSlot.Snacks.Count == 0
                    ? sb.AppendLine($"{snackSlot.Identifier}, Sold out!")
                    : sb.AppendLine($"{snackSlot.ToString()}, Qty: {snackSlot.Snacks.Count}");
            }
        }
        return sb.ToString();
    }
}
