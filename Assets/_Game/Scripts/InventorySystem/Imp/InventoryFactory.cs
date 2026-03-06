using System;
using System.Collections.Generic;
using _Game.Scripts.InventorySystem.Factories;
using Core.Common;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryFactory : IDisposable
    {
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly InventoryView _inventoryView;
        private readonly EventBus _eventBus;
        
        private Inventory _inventory;

        private InventoryFactory(InventoryFactoryProvider inventoryFactoryProvider, EventBus eventBus, InputSystem_Actions  inputSystemActions, InventoryView inventoryView)
        {
            _inventoryView =  inventoryView;
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryFactoryProvider = inventoryFactoryProvider; 
        }
        
        public Inventory CreateInventory()
        {
            //load data items
            InventoryModel inventoryModel = new InventoryModel(new List<InventoryItemModel>());
            
            _inventoryView.Construct(inventoryModel.IsOpen, inventoryModel.SelectedIndex);
            _inventory = new Inventory(inventoryModel, _inventoryFactoryProvider, _eventBus, _inputSystemActions);
            
            return _inventory;
        }

        public Inventory GetCachedInventory()
        {
            return _inventory;
        }

        public void Dispose()
        {
            _inputSystemActions?.Dispose();
            _inventory?.Dispose();
        }
    }
}