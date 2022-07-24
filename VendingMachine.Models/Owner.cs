namespace VendingMachine.Models
{
    public class Owner
    {
        private const string INVENTORY_FILE_NAME = "Inventory.txt";
        private const string INVENTORY_FILE_PATH = @"..\..\..\..\Inventory";
        
        public static void TurnOnVendingMachine(Vendomatic vm)
        {
            vm.IsOn = true;
            UpdateVendingMachineInventory(vm);
        }

        public static void TurnOffVendingMachine(Vendomatic vm)
        {
            vm.IsOn = false;
        }        

        public static bool UpdateVendingMachineInventory(Vendomatic vm)
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

                            vm.Products.Add(lineArray[0], new List<Product>());  // throws exception if key already exists

                            while (vm.Products[lineArray[0]].Count < Product.INITIAL_STARTING_QUANTITY)
                            {
                                vm.Products[lineArray[0]].Add(productToAddToInventory);
                            }

                        }
                    }
                }

                success = true;
            }

            catch (IOException ex)
            {
                success = false;
            }
            catch (Exception ex)
            {
                // TODO: Exception handling
            }

            return success;
        }
    }
}