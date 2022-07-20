namespace VendingMachine.Models
{
    public class Owner
    {
        private const string INVENTORY_FILE_NAME = "Inventory.txt";
        private const string INVENTORY_FILE_PATH = @"..\..\..\..\Inventory";
        
        public static void TurnOnVendingMachine(Vendomatic vendingMachine)
        {
            if (vendingMachine == null) throw new ArgumentNullException(nameof(vendingMachine), "Unknown vending machine.");  // TODO: make error messages consts?
            
            vendingMachine.IsOn = true;
            UpdateVendingMachineInventory(vendingMachine);
        }

        public static void TurnOffVendingMachine(Vendomatic vendingMachine)
        {
            if (vendingMachine == null) throw new ArgumentNullException(nameof(vendingMachine), "Unknown vending machine.");
            
            vendingMachine.IsOn = false;
        }        

        public static bool UpdateVendingMachineInventory(Vendomatic vendingMachine)
        {
            if (vendingMachine == null) throw new ArgumentNullException(nameof(vendingMachine), "Unknown vending machine."); //TODO: Are exceptions being handled correctly?

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
                            Product productToAddToInventory = lineArray[3] switch
                            {
                                "Chip" => new Chip(),
                                "Candy" => new Candy(),
                                "Drink" => new Drink(),
                                "Gum" => new Gum(),
                                _ => new Product(),
                            };
                            productToAddToInventory.Name = lineArray[1].Trim();
                            productToAddToInventory.Price = Decimal.Parse(lineArray[2]);

                            vendingMachine.Products.Add(lineArray[0], new List<Product>());  // throws exception if key already exists

                            while (vendingMachine.Products[lineArray[0]].Count < Product.INITIAL_STARTING_QUANTITY)
                            {
                                vendingMachine.Products[lineArray[0]].Add(productToAddToInventory);
                            }

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