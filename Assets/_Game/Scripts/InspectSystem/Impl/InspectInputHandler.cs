using System;
using System.Linq;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.InspectSystem
{
    public class InspectInputHandler : IDisposable
    {
        protected readonly InputSystem_Actions _inputSystemActions;
        
        protected  InspectModel _currentInspectModel;
        protected  AbstractInteractable _selectedInteractable;

        public InspectInputHandler(InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
        }
        
        public virtual void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            if (_currentInspectModel.Interactables.Count <= 0)
                return;
            
            SelectFirst();
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
            _inputSystemActions.Player.Interact.performed += InteractWithSelectedItem;
        }
        
        public virtual void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _inputSystemActions.Player.Interact.performed -= InteractWithSelectedItem;
            
            if (_selectedInteractable != null)
                _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;
        }

        protected virtual void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();

            if (HasMoreItem())
            {
                AbstractInteractable newModel = GetInteractableByDirection(direction);

                if (newModel != null)
                {
                    if (_selectedInteractable != null)
                        _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;
                    
                    _selectedInteractable = newModel;
                    _selectedInteractable.AbstractInteractableModel.IsSelected.Value = true;
                }
            }
        }

        protected bool HasMoreItem()
        {
            if (_currentInspectModel.Interactables.Count == 1)
                return false;
            
            return true;
        }

        protected AbstractInteractable GetInteractableByDirection(Vector2 direction)
        {
            AbstractInteractable bestCandidate = null;
            float bestDistance = float.MaxValue;
    
            Vector2 currentPos = _selectedInteractable.AbstractInteractableModel.Position;
    
            foreach (var interactable in _currentInspectModel.Interactables)
            {
                if (_selectedInteractable == interactable)
                    continue;
            
                if(!interactable.AbstractInteractableModel.CanSelected.Value)
                    continue;
            
                Vector2 toTarget = interactable.AbstractInteractableModel.Position - currentPos;
        
                float projection = Vector2.Dot(direction.normalized, toTarget);
                if (projection <= 0) 
                    continue;
            
                float distance = toTarget.magnitude;
        
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestCandidate = interactable;
                }
            }
    
            return bestCandidate;
        }

        protected virtual void InteractWithSelectedItem(InputAction.CallbackContext _)
        {
            if(_selectedInteractable == null)
                return;
            
            _selectedInteractable.Interact();

            if(!_selectedInteractable.CanInteract())
                SelectFirst();
        }
        
        protected void SelectFirst()
        {
            bool hasSelectable = false;
            int i = 0;
            foreach (var interactable in _currentInspectModel.Interactables)
            {
                if (interactable.CanInteract())
                {
                    hasSelectable = true;
                    break;
                }

                i++;
            }

            if (hasSelectable)
            {
                _selectedInteractable = _currentInspectModel.Interactables[i];
                _selectedInteractable.AbstractInteractableModel.IsSelected.Value = true;
            }
            else
            {
                _selectedInteractable = null;
            }
        }

        public void Dispose()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
        }
    }
}