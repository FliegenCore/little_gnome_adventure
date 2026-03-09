using System;
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
        
        private int _currentSelectedInventoryIndex;
        
        public InventoryInput(InputSystem_Actions inputSystemActions, EventBus eventBus)
        {
            _eventBus = eventBus;
            _inputSystemActions = inputSystemActions;
        }
        
        public void Enable()
        {
            _inputSystemActions.UI.Navigate.performed += Navigate;
            SelectItem();
        }

        public void Disable()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _currentSelectedInventoryIndex = 0;
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
                
            }
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
        
        private void SelectItem()
        {
            _eventBus.TriggerEvenet<SendChooseInventoryIndexSignal, int>(_currentSelectedInventoryIndex);
        }
    }
}