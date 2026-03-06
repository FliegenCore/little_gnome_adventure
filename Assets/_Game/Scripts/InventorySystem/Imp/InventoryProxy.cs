namespace _Game.Scripts.InventorySystem
{
    public class InventoryProxy
    {
        private readonly InventoryFactory _inventoryFactory;
        
        public InventoryProxy(InventoryFactory inventoryFactory)
        {
            _inventoryFactory = inventoryFactory;
        }
        
        public void AddItem(ItemId id)
        {
            Inventory inventory = _inventoryFactory.CreateInventory();

            inventory.AddItem(id);
        }

        public void RemoveItem(InventoryItem inventoryItem)
        {
            Inventory inventory = _inventoryFactory.CreateInventory();

            inventory.RemoveItem(inventoryItem);
        }
    }
}