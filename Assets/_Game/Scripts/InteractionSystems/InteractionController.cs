using System;
using _Game.Scripts.PlayerSystems;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.InteractionSystems
{
    public class InteractionController : IDisposable
    {
        private AbstractInteractable _currentAbstractInteractable;

        private readonly InputSystem_Actions _inputSystemActions;
        private readonly PlayerModel _playerModel;
        private readonly EventBus _eventBus;
        
        public InteractionController(InputSystem_Actions inputSystemActions, PlayerModel playerModel, EventBus eventBus)
        {
            _inputSystemActions = inputSystemActions;
            _playerModel = playerModel;
            _eventBus = eventBus;
            
            _playerModel.CanInteract.Subscribe(OnCanInteractChanged);
            
            _eventBus.Subscribe<SetCurrentInteractableSignal, AbstractInteractable>(this, SetCurrentInteractable);
            _eventBus.Subscribe<RemoveCurrentInteractableSignal, AbstractInteractable>(this, RemoveCurrentInteractable);
        }

        private void OnCanInteractChanged(bool canInteract)
        {
            if(canInteract)
                Active();
            else
                Deactivate();
        }

        private void Active()
        {
            _inputSystemActions.Player.Interact.performed += Interact;
        }

        private  void Deactivate()
        {
            _inputSystemActions.Player.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext _)
        {
            if (!CanInteract())
            {
                return;
            }
            
            _currentAbstractInteractable.Interact();
        }

        private bool CanInteract()
        {
            if(_currentAbstractInteractable == null || !_currentAbstractInteractable.CanInteract())
                return false;
            
            return true;
        }

        private void SetCurrentInteractable(AbstractInteractable abstractInteractable)
        {
            _currentAbstractInteractable = abstractInteractable;

            _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = true;
        }

        private void RemoveCurrentInteractable(AbstractInteractable abstractInteractable)
        {
            if (_currentAbstractInteractable != abstractInteractable)
            {
                return;
            }

            _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = false;
            _currentAbstractInteractable = null;
        }
        
        public void Dispose()
        {
            _playerModel.CanInteract.Unsubscribe(OnCanInteractChanged);
            _eventBus.Unsubscribe<SetCurrentInteractableSignal>(this);
        }
    }
}