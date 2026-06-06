using System;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem.Configs;
using _Game.Scripts.InventorySystem.Modules;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryInput 
    {
        private const int MAX_ITEMS = 6;
        
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        private readonly MergeItemModule _mergeItemModule; 
        private readonly Inventory _inventory;
        private readonly SelectedItemModel _selectedItemModel;
        private readonly InteractionController _interactionController;
        private int _currentSelectedInventoryIndex;
        
        public InventoryInput(
            InputSystem_Actions inputSystemActions,
            EventBus eventBus,
            Inventory inventory,
            MergeItemConfig mergeItemConfig,
            SelectedItemModel selectedItemModel,
            InteractionController interactionController)
        {
            _interactionController = interactionController;
            _selectedItemModel     = selectedItemModel;
            _mergeItemModule       = new MergeItemModule(mergeItemConfig, inventory);
            _eventBus              = eventBus;
            _inputSystemActions    = inputSystemActions;
            _inventory             = inventory;
        }
        
        public void Enable()
        {
            _inputSystemActions.Player.Interact.performed += UseItem;
            _inputSystemActions.UI.Navigate.performed += Navigate;
            SelectItem();
            ShowBaseInputInfo();
        }

        public void Disable()
        {
            _mergeItemModule.Clear();
            _inputSystemActions.Player.Interact.performed -= UseItem;
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _currentSelectedInventoryIndex = 0;
            HideSelectedItem();
        }
        
        private void ShowSelectedItem()
        {
            if (_interactionController.CurrentAbstractInteractable == null
                || !_interactionController.CurrentAbstractInteractable.CanInteract())
            {
                return;
            }

            if (_inventory.SelectedInventoryItem == null)
            {
                _selectedItemModel.IsActive.Value = false;
                return;
            }
            
            _interactionController.StopUpdate();
            _selectedItemModel.Position.Value =
                _interactionController.CurrentAbstractInteractable.InteractableView.HintSelect.transform.position;
            _selectedItemModel.SpriteStorage.Sprite.Value = _inventory.SelectedInventoryItem.InventoryItemModel.SpriteStorage.Sprite.Value;
            _selectedItemModel.IsActive.Value = true;
        }

        private void HideSelectedItem()
        {
            _interactionController.StartUpdate();
            _selectedItemModel.IsActive.Value = false;
        }
        
        private void UseItem(InputAction.CallbackContext _)
        {
            if (_mergeItemModule.IsEnable)
            {
                return;
            }
            
            _eventBus.TriggerEvenet<UseItemSignal>();
        }
        
        private void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();

            float xDirection = Mathf.Abs(direction.x);
            float yDirection = Mathf.Abs(direction.y);

            if (xDirection > yDirection)
            {
                ChooseInventoryIndex(direction.x);
            }
            else
            {
                if (yDirection > 0)
                {
                    ShowMergeInputInfo();
                    _mergeItemModule.IsEnable = true;

                    if (_inventory.SelectedInventoryItem != null)
                    {
                        _mergeItemModule.SetupItemForMerge(_inventory.SelectedInventoryItem, _inventory.AddItem);
                    }
                }
                else if (yDirection < 0)
                {
                    if (_mergeItemModule.IsEnable)
                    {
                        _mergeItemModule.Clear();
                        ShowBaseInputInfo();
                        
                        return;
                    }

                    ShowDescription();
                }
            }
        }

        private void ShowDescription()
        {
            
        }
        
        private void ChooseInventoryIndex(float direction)
        {
            if (direction > 0)
            {
                if(_currentSelectedInventoryIndex < MAX_ITEMS)
                    _currentSelectedInventoryIndex++;
            }
            else
            {
                if (_currentSelectedInventoryIndex > 0)
                {
                    _currentSelectedInventoryIndex--;
                }
            }

            SelectItem();
        }

        private void ShowBaseInputInfo()
        {
            
        }
        
        private void ShowMergeInputInfo()
        {
        }
        
        private void SelectItem()
        {
            _eventBus.TriggerEvenet<SendChooseInventoryIndexSignal, int>(_currentSelectedInventoryIndex);
            ShowSelectedItem();
        }
    }
}