using System.Collections.Generic;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InventorySystem.Factories;

namespace _Game.Scripts.InventorySystem
{
    public class Inventory
    {
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        
        private List<InventoryItem> _items = new  List<InventoryItem>();

        public Inventory(InventoryModel inventoryModel, InventoryFactoryProvider inventoryFactoryProvider)
        {
            _inventoryModel = inventoryModel;   
            _inventoryFactoryProvider = inventoryFactoryProvider;
            
            //_inventoryModel.ItemModels.Subscribe(AddItem);
        }
        
        public void AddItem(ItemId id)
        {
            InventoryItem inventoryItem = CreateItem(id);
            _inventoryModel.ItemModels.Add((InventoryItemModel)inventoryItem.AbstractInteractableModel);
        }

        public void RemoveItem(InventoryItem inventoryItem)
        {
            _inventoryModel.ItemModels.Remove((InventoryItemModel)inventoryItem.AbstractInteractableModel);
            _items.Remove(inventoryItem);
        }

        private InventoryItem CreateItem(ItemId id)
        {
            IInventoryItemFactory inventoryItemFactory = _inventoryFactoryProvider.GetItemFactory(id);

            InventoryItem inventoryItem = inventoryItemFactory.CreateItem(id);
            
            _items.Add(inventoryItem);
            
            return inventoryItem;
        }
    }
}