using System.Collections.Generic;
using _Game.Scripts.InventorySystem.Factories;
using Core.Common;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryFactory
    {
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly InventoryView _inventoryView;
        private readonly EventBus _eventBus;

        private InventoryFactory(InventoryFactoryProvider inventoryFactoryProvider, EventBus eventBus, InputSystem_Actions  inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryFactoryProvider = inventoryFactoryProvider; 
        }
        
        public Inventory CreateInventory()
        {
            //load data
            InventoryModel inventoryModel = new InventoryModel(new List<InventoryItemModel>());
            _inventoryView.Construct(inventoryModel.IsOpen);
            Inventory inventory = new Inventory(inventoryModel, _inventoryFactoryProvider, _eventBus, _inputSystemActions);
            
            return inventory;
        }
    }
}