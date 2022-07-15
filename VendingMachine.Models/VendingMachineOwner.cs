namespace VendingMachine.Models
{
    public class VendingMachineOwner
    {
        internal const string INVENTORY_FILE_NAME = "Inventory.txt";
        internal const string INVENTORY_FILE_PATH = @"..\..\..\..\Inventory";
        
        public static void TurnOnVendingMachine(VendingMachine vendingMachine)
        {
            vendingMachine.IsOn = true;
            UpdateVendingMachineInventory(vendingMachine);
        }

        public static void TurnOffVendingMachine(VendingMachine vendingMachine) => vendingMachine.IsOn = false;
        
        public static bool UpdateVendingMachineInventory(VendingMachine vendingMachine)
        {
            //TODO: Does the inventory file need to be set to 'copy to output directory'? It seems to be working w/o this...
            bool success = false;
            string currentDirectory = Environment.CurrentDirectory;
            string fullInventoryFilePath = Path.Combine(currentDirectory, INVENTORY_FILE_PATH, INVENTORY_FILE_NAME);
            try
            {
                using StreamReader sr = new(fullInventoryFilePath);
                if (sr != null)
                {
                    while (!sr.EndOfStream)
                    {
                        string? line = sr.ReadLine();
                        if (line != null)
                        {
                            string[] lineArray = line.Split("|");
                            VendingMachineProduct productToAddToInventory = lineArray[3] switch
                            {
                                "Chip" => new Chip(),
                                "Candy" => new Candy(),
                                "Drink" => new Drink(),
                                "Gum" => new Gum(),
                                _ => new VendingMachineProduct(),
                            };
                            productToAddToInventory.Name = lineArray[1].Trim();
                            productToAddToInventory.Price = Decimal.Parse(lineArray[2]);
                            productToAddToInventory.Quantity = VendingMachineProduct.INITIAL_STARTING_QUANTITY;
                            vendingMachine.Products.Add(lineArray[0].ToUpper(), productToAddToInventory);
                        }
                    }
                }

                success = true;
            }

            catch (Exception ex)
            {
                success = false;
            }

            return success;
        }
    }
}