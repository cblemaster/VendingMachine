
using VendingMachine.Snacks;
using _Customer = VendingMachine.Customer.Customer;
using _Owner = VendingMachine.Owner.Owner;

namespace VendingMachine.Machine;

internal sealed class VendTron
{
    private readonly string[] _slotRows = ["A", "B", "C", "D"];
    private readonly string[] _slotColumns = ["1", "2", "3", "4"];

    internal IEnumerable<Slot> Slots { get; }

    internal VendTron(Slot[] slots)
    {
        LoadSnacks loadSnacks = new();
        IEnumerable<SnackJsonModel> jsonSnacks = loadSnacks.GetSnacksFromInventory();
        Slots = GetSlotsWithSnacks(jsonSnacks);
    }

    internal IEnumerable<Slot> GetSlotsWithSnacks(IEnumerable<SnackJsonModel> jsonSnacks)
    {
        int snackIndex = 0;
        List<Slot> slots = [];
        
        foreach (string row in _slotRows)
        {
            foreach (string column in _slotColumns)
            {
                string identifier = $"{row}{column}";
                
                SnackJsonModel jsonSnack = jsonSnacks.ElementAt(snackIndex);

                decimal price = jsonSnack.Price;
                string label = jsonSnack.Label;

                Snack snack = null!;

                switch (jsonSnack.Type) // TODO: Replace with something less brittle
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
                    case "Poptart":
                        snack = new Poptart(price, label);
                        break;
                    default:
                        break;
                }

                Slot slot = new(identifier, [.. Enumerable.Repeat<Snack>(snack, Slot.SLOT_CAPACITY)]);
                slots.Add(slot);

                snackIndex++;
            }
        }

        return slots;
    }
}
