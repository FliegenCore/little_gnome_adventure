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
        private AbstractInteractableModel _selectedInteractableModel;

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
        }
        
        public void DisableInput()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
        }

        private void Navigate(InputAction.CallbackContext callback)
        {
            Vector2 direction = callback.ReadValue<Vector2>();

            if (HasItem())
            {
                AbstractInteractableModel newModel = GetInteractableModelByDirection(direction);

                if (newModel != null)
                {
                    if (_selectedInteractableModel != null)
                        _selectedInteractableModel.Selected.Value = false;
                    
                    _selectedInteractableModel = newModel;
                    _selectedInteractableModel.Selected.Value = true;
                    Debug.Log($"Selected interactable model: {_selectedInteractableModel} {_selectedInteractableModel.Position}");
                }
            }
        }

        private bool HasItem()
        {
            if (_currentInspectModel.Interactables.Count == 1)
                return false;
            
            return true;
        }

        private AbstractInteractableModel GetInteractableModelByDirection(Vector2 direction)
        {
            AbstractInteractableModel bestCandidate = null;
            float bestScore = -1f;
    
            foreach (var interactable in _currentInspectModel.Interactables)
            {
                if (_selectedInteractableModel == interactable.AbstractInteractableModel)
                    continue;
        
                Vector2 toInteractable = (interactable.AbstractInteractableModel.Position - _selectedInteractableModel.Position).normalized;
        
                float dot = Vector2.Dot(direction.normalized, toInteractable);
        
                if (dot > bestScore)
                {
                    bestScore = dot;
                    bestCandidate = interactable.AbstractInteractableModel;
                }
            }
    
            return bestScore > 0.5f ? bestCandidate : null;
        }

        private void SelectFirst()
        {
            _selectedInteractableModel = _currentInspectModel.Interactables[0].AbstractInteractableModel;
            _selectedInteractableModel.Selected.Value = true;
        }

        public void Dispose()
        {
            _inputSystemActions.UI.Navigate.performed -= Navigate;
        }
    }
}