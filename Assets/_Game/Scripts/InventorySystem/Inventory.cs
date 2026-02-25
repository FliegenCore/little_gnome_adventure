using System;
using System.Collections.Generic;
using _Game.Scripts.InventorySystem.Factories;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.InventorySystem
{
    public class Inventory
    {
        public const float VIEW_POS = 124.75f;
        
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        private readonly InputSystem_Actions  _inputSystemActions;
        private readonly EventBus _eventBus;
        
        private List<InventoryItem> _items = new  List<InventoryItem>();
        
        private InventoryItem _currentSelectedInventoryItem;
        private int _currentSelectedInventoryIndex = -1;

        public Inventory(InventoryModel inventoryModel, 
            InventoryFactoryProvider inventoryFactoryProvider,
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryModel = inventoryModel;   
            _inventoryFactoryProvider = inventoryFactoryProvider;
        }

        public void EnableOpenCloseInput()
        {
            _inputSystemActions.Player.InventoryOpen.performed += SetOpen;
        }

        public void DisableOpenCloseInput()
        {
            _inputSystemActions.Player.InventoryOpen.performed -= SetOpen;
        }

        private void SetOpen(InputAction.CallbackContext _)
        {
            if (!_inventoryModel.IsOpen.Value)
                Enable();
            else
                Disable();
        }

        public void Enable()
        {
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerInventoryState));
            _inputSystemActions.UI.Navigate.performed += Navigate;
            EnableInventory();
        }

        public void Disable()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            DisableInventory();
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
        }
        
        public void AddItem(ItemId id)
        {
            InventoryItem inventoryItem = CreateItem(id);
            _inventoryModel.ItemModels.Add((InventoryItemModel)inventoryItem.AbstractInteractableModel);
            _items.Add(inventoryItem);
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
            
            return inventoryItem;
        }

        private void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();

            float x = Mathf.Abs(direction.x);
            float y = Mathf.Abs(direction.y);

            if (x > y)
            {
                InventoryItem selectItem = GetInventoryItemByDirection(x);
                
                if (selectItem != null && _currentSelectedInventoryItem != selectItem)
                {
                    _currentSelectedInventoryItem = selectItem;
                    
                }
                    
            }
            else
            {
                
            }
        }
        
        private InventoryItem GetInventoryItemByDirection(float direction)
        {
            if (direction > 0)
            {
                if(_currentSelectedInventoryIndex < 6)
                    _currentSelectedInventoryIndex++;
            }
            else
            {
                if (_currentSelectedInventoryIndex > 0)
                {
                    _currentSelectedInventoryIndex--;
                }
            }
            
            if(_currentSelectedInventoryIndex != -1 && _items.Count > _currentSelectedInventoryIndex)
                return _items[_currentSelectedInventoryIndex];
            
            return null;
        }
        
        private void SelectItem(int index)
        {
            
        }

        private void EnableInventory()
        {
            _inventoryModel.IsOpen.Value = true;

            _currentSelectedInventoryIndex = 0;
            SelectItem(_currentSelectedInventoryIndex);
        }

        private void DisableInventory()
        {
            _inventoryModel.IsOpen.Value = false;
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
        }
    }
}