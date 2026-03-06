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
        private readonly InputSystem_Actions _inputSystemActions;
        
        private InspectModel _currentInspectModel;
        private AbstractInteractable _selectedInteractable;

        public InspectInputHandler(InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
        }
        
        public void EnableInput(InspectModel inspectModel)
        {
            _currentInspectModel = inspectModel;

            if (_currentInspectModel.Interactables.Count <= 0)
                return;
            
            SelectFirst();
            
            _inputSystemActions.UI.Navigate.performed += Navigate;
            _inputSystemActions.Player.Interact.performed += InteractWithSelectedItem;
        }
        
        public void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
            _inputSystemActions.Player.Interact.performed -= InteractWithSelectedItem;
            
            if (_selectedInteractable != null)
                _selectedInteractable.AbstractInteractableModel.IsSelected.Value = false;

        }

        private void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();

            if (HasItem())
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

        private bool HasItem()
        {
            if (_currentInspectModel.Interactables.Count == 1)
                return false;
            
            return true;
        }

        private AbstractInteractable GetInteractableByDirection(Vector2 direction)
        {
            AbstractInteractable bestCandidate = null;
            float bestScore = -1f;
    
            foreach (var interactable in _currentInspectModel.Interactables)
            {
                if (_selectedInteractable == interactable)
                    continue;
        
                Vector2 toInteractable = (interactable.AbstractInteractableModel.Position - _selectedInteractable.AbstractInteractableModel.Position).normalized;
        
                float dot = Vector2.Dot(direction.normalized, toInteractable);
        
                if (dot > bestScore)
                {
                    bestScore = dot;
                    bestCandidate = interactable;
                }
            }
    
            return bestScore > 0.5f ? bestCandidate : null;
        }

        private void InteractWithSelectedItem(InputAction.CallbackContext _)
        {
            if(_selectedInteractable == null)
                return;
            
            _selectedInteractable.Interact();
        }
        
        private void SelectFirst()
        {
            _selectedInteractable = _currentInspectModel.Interactables[0];
            _selectedInteractable.AbstractInteractableModel.IsSelected.Value = true;
        }

        public void Dispose()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
        }
    }
}