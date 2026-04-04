using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem.Factories;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
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
        private readonly InteractionController _interactionController;
        
        private readonly List<InventoryItem> _items = new List<InventoryItem>();
        
        private InventoryItem _currentSelectedInventoryItem;

        public Inventory(InventoryModel inventoryModel, 
            InventoryFactoryProvider inventoryFactoryProvider,
            EventBus eventBus, 
            InputSystem_Actions inputSystemActions,
            InteractionController interactionController)
        {
            _interactionController    = interactionController;
            _inputSystemActions       = inputSystemActions;
            _eventBus                 = eventBus;
            _inventoryModel           = inventoryModel;   
            _inventoryFactoryProvider = inventoryFactoryProvider;
            _inventoryInput           = new InventoryInput(inputSystemActions, _eventBus);

            Initialize();
        }

        private void Initialize()
        {
            _eventBus.Subscribe<SendChooseInventoryIndexSignal, int>(this, SelectInventoryCell);
            _eventBus.Subscribe<DialogueEventSignal, string>(this, DialogueInventoryEvent);
            _eventBus.Subscribe<AddItemSignal, ItemId>(this, AddItem);
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

        public void RemoveItemById(ItemId id)
        {
            foreach (var item in _items)
            {
                if (item.AbstractInteractableModel.Id == id.ToString())
                {
                    RemoveItem(item);
                }
            }
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
            _inputSystemActions.Player.Interact.performed += UseItem;
            EnableInventory();
        }

        private void Disable()
        {
            _inventoryInput.Disable();
            _inputSystemActions.Player.Interact.performed -= UseItem;
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

            if (_items.Count > index)
            {
                _currentSelectedInventoryItem = _items[index];
            }
        }

        private void UseItem(InputAction.CallbackContext _)
        {
            if (_currentSelectedInventoryItem == null)
                return;
            
            AbstractInteractable currentInteractable = _interactionController.CurrentAbstractInteractable;

            if (currentInteractable == null)
                return;
            
            ItemId id = Enum.Parse<ItemId>(_currentSelectedInventoryItem.AbstractInteractableModel.Id);

            ACustomBehaviour customBehaviour = currentInteractable.CustomBehaviour;
            if (customBehaviour == null)
                return;

            if (customBehaviour is IItemNeeder itemNeeder)
            {
                Disable();
                itemNeeder.InteractWithItem(id);
                
                RemoveItem(_currentSelectedInventoryItem);
            }
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

        private void DialogueInventoryEvent(string message)
        {
            if (!message.Contains("inventory_"))
            {
                return;
            }
            
            string[] paraments = GetParams(message);
            
            if (paraments[0] == "add")
            {
                ItemId id = Enum.Parse<ItemId>(paraments[1]);
                AddItem(id);
            }
            else if (paraments[0] == "remove")
            {
                ItemId id = Enum.Parse<ItemId>(paraments[1]);
                RemoveItemById(id);
            }
        }
        
        private string[] GetParams(string message)
        {
            if (message.Contains("inventory_"))
            {
                message = message.Replace("inventory_", "");
                
                string[] parameters =  message.Split('_');
                
                return parameters;
            }
            
            return null;
        }

        public void Dispose()
        {
            _inputSystemActions?.Dispose();
            _currentSelectedInventoryItem?.Dispose();
            _eventBus.Unsubscribe<SendChooseInventoryIndexSignal>(this);
        }
    }

    internal class AddItemSignal
    {
    }
}