using System;
using System.Collections.Generic;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem.Configs;
using _Game.Scripts.InventorySystem.Factories;
using _Game.Scripts.PlayerSystems;
using Core.Common;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryFactory : IDisposable
    {
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly InventoryView _inventoryView;
        private readonly EventBus _eventBus;
        private readonly MergeItemConfig _mergeItemConfig;
        
        private Inventory _inventory;

        private InventoryFactory(
            InventoryFactoryProvider inventoryFactoryProvider,
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions,
            InventoryView inventoryView,
            MergeItemConfig mergeItemConfig)
        {
            _mergeItemConfig = mergeItemConfig;
            _inventoryView =  inventoryView;
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryFactoryProvider = inventoryFactoryProvider; 
        }
        
        public Inventory CreateInventory(InteractionController interactionController)
        {
            //load data items
            InventoryModel inventoryModel = new InventoryModel(new List<InventoryItemModel>());
            
            _inventoryView.Construct(inventoryModel.IsOpen, inventoryModel.SelectedIndex);
            _inventory = new Inventory(inventoryModel, _inventoryFactoryProvider, _eventBus, _inputSystemActions, interactionController, _mergeItemConfig);
            
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