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
        private readonly SelectedItemManager _selectedItemManager;
        
        private readonly SelectedItemView _selectedItemView;
        private Inventory _inventory;

        private InventoryFactory(
            InventoryFactoryProvider inventoryFactoryProvider,
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions,
            InventoryView inventoryView,
            MergeItemConfig mergeItemConfig,
            SelectedItemView selectedItemView)
        {
            _selectedItemView = selectedItemView;
            _mergeItemConfig = mergeItemConfig;
            _inventoryView =  inventoryView;
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryFactoryProvider = inventoryFactoryProvider;
            _selectedItemManager = CreateSelectedItem();
        }

        private SelectedItemManager CreateSelectedItem()
        {
            SelectedItemModel model = new SelectedItemModel();
            SelectedItemManager selectedItemManager = new SelectedItemManager(_selectedItemView, model);

            return selectedItemManager;
        }
        
        public Inventory CreateInventory(InteractionController interactionController)
        {
            //load data items
            InventoryModel inventoryModel = new InventoryModel(new List<InventoryItemModel>());
            
            _inventoryView.Construct(inventoryModel.IsOpen, inventoryModel.SelectedIndex, inventoryModel.InventoryItems);
            
            _inventory = new Inventory(inventoryModel, 
                _inventoryFactoryProvider,
                _eventBus,
                _inputSystemActions,
                interactionController, 
                _mergeItemConfig,
                _selectedItemManager);
            
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