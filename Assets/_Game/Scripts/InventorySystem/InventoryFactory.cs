using _Game.Scripts.InventorySystem.Factories;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryFactory
    {
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;

        private InventoryFactory(InventoryFactoryProvider inventoryFactoryProvider)
        {
            _inventoryFactoryProvider = inventoryFactoryProvider; 
        }
        
        public Inventory CreateInventory()
        {
            //load data
            InventoryModel inventoryModel = new InventoryModel(null);
            
            Inventory inventory = new Inventory(inventoryModel, _inventoryFactoryProvider);
            
            return inventory;
        }
    }
}