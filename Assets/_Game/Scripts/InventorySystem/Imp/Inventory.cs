using System;
using System.Collections.Generic;
using _Game.Scripts.InventorySystem.Factories;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _Game.Scripts.InventorySystem
{
    public class Inventory : IDisposable
    {
        public const float VIEW_POS = 124.75f;
        
        private readonly InventoryModel _inventoryModel;
        private readonly InventoryFactoryProvider _inventoryFactoryProvider;
        private readonly InputSystem_Actions  _inputSystemActions;
        private readonly EventBus _eventBus;
        private readonly InventoryInput _inventoryInput;
        
        private List<InventoryItem> _items = new  List<InventoryItem>();
        
        private InventoryItem _currentSelectedInventoryItem;

        public Inventory(InventoryModel inventoryModel, 
            InventoryFactoryProvider inventoryFactoryProvider,
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
            _inventoryModel = inventoryModel;   
            _inventoryFactoryProvider = inventoryFactoryProvider;
            _inventoryInput = new InventoryInput(inputSystemActions, _eventBus);

            Initialize();
        }

        public void Initialize()
        {
            _eventBus.Subscribe<SendChooseInventoryIndexSignal, int>(this, SelectInventoryCell);
        }
        
        public void EnableOpenCloseInput()
        {
            _inputSystemActions.Player.InventoryOpen.performed += SetOpen;
        }

        public void DisableOpenCloseInput()
        {
            _inputSystemActions.Player.InventoryOpen.performed -= SetOpen;
        }

        public void AddItem(ItemId id)
        {
            Debug.Log($"Add item {id}");
            
            InventoryItem inventoryItem = CreateItem(id);
            _inventoryModel.ItemModels.Add((InventoryItemModel)inventoryItem.AbstractInteractableModel);
            _items.Add(inventoryItem);
        }

        public void RemoveItem(InventoryItem inventoryItem)
        {
            _inventoryModel.ItemModels.Remove((InventoryItemModel)inventoryItem.AbstractInteractableModel);
            _items.Remove(inventoryItem);
        }
        
        private void SetOpen(InputAction.CallbackContext _)
        {
            if (!_inventoryModel.IsOpen.Value)
                Enable();
            else
                Disable();
        }
        
        private void Enable()
        {
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerInventoryState));
            _inventoryInput.Enable();
            
            EnableInventory();
        }

        private void Disable()
        {
            _inventoryInput.Disable();
            DisableInventory();
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
        }
        
        private InventoryItem CreateItem(ItemId id)
        {
            IInventoryItemFactory inventoryItemFactory = _inventoryFactoryProvider.GetItemFactory(id);

            InventoryItem inventoryItem = inventoryItemFactory.CreateItem(id, _items.Count);
            
            return inventoryItem;
        }

        private void SelectInventoryCell(int index)
        {
            _inventoryModel.SelectedIndex.Value = index;
        }
        
        private void FillItems()
        {
            foreach (InventoryItem inventoryItem in _items)
            {
                //fill here
            }
        }

        public void UseItem(int index)
        {
            _items[index].Interact();
        }

        private void EnableInventory()
        {
            _inventoryModel.SelectedIndex.Value = 0;
            _inventoryModel.IsOpen.Value = true;
        }

        private void DisableInventory()
        {
            _inventoryModel.IsOpen.Value = false;
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
        }

        public void Dispose()
        {
            _inputSystemActions?.Dispose();
            _currentSelectedInventoryItem?.Dispose();
            _eventBus.Unsubscribe<SendChooseInventoryIndexSignal>(this);
        }
    }
}