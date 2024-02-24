using VendingMachine.Models;

namespace VendingMachine.Services;

internal static class ProcessInventory
{
    private const string INVENTORY_FILE_NAME = "Inventory.txt";
    private const string INVENTORY_FILE_PATH = @"..\..\..\..\Inventory";

    internal static void UpdateVendingMachineInventory(VendingMachine.Models.Vendomatic vm)
    {
        string currentDirectory = Environment.CurrentDirectory;
        string fullInventoryFilePath = Path.Combine(currentDirectory, INVENTORY_FILE_PATH, INVENTORY_FILE_NAME);

        try
        {
            using StreamReader sr = new(fullInventoryFilePath);

            while (sr is not null && !sr.EndOfStream)
            {
                string? inventoryLine = sr.ReadLine();

                (string SlotIdentifier, string Label, decimal Price, string Type, bool IsValid) inventoryLineValues =
                    ProcessInventoryLine(inventoryLine);

                Snack snack = CreateSnack(inventoryLineValues);

                Slot slot = vm.Slots.Single(s => s.Identifier == inventoryLineValues.SlotIdentifier);

                while (slot.Snacks.Count < VendingMachine.Models.Vendomatic.SNACKS_PER_SLOT)
                {
                    slot.Snacks.Push(snack);
                }
            }
        }
        catch (IOException) { throw; }

        static Snack CreateSnack((string SlotIdentifier, string Label, decimal Price, string Type, bool IsValid) inventoryLineValues)
        {
            Snack snack = inventoryLineValues.Type switch
            {
                "Chips" => new Chips() { Label = inventoryLineValues.Label, Price = inventoryLineValues.Price },
                "Crackers" => new Crackers() { Label = inventoryLineValues.Label, Price = inventoryLineValues.Price },
                "Drink" => new Drink() { Label = inventoryLineValues.Label, Price = inventoryLineValues.Price },
                "Poptart" => new Poptart() { Label = inventoryLineValues.Label, Price = inventoryLineValues.Price },
                _ => new UnknownSnack() { Label = inventoryLineValues.Label, Price = inventoryLineValues.Price },
            };

            return snack;
        }

        static (string SlotIdentifier, string Label, decimal Price, string Type, bool IsValid) ProcessInventoryLine(string? inventoryLine)
        {
            if (inventoryLine is null || string.IsNullOrWhiteSpace(inventoryLine)) { return ("error!", "error!", 0M, "error!", false); }

            string[] dataElements = inventoryLine.Split('|');

            string slotIdentifier = dataElements[0].Trim();
            string label = dataElements[1].Trim();
            decimal price = decimal.Parse(dataElements[2].Trim());
            string type = dataElements[3].Trim();

            return (slotIdentifier, label, price, type, true);
        }
    }
}