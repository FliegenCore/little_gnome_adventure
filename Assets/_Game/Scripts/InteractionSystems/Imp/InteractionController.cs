using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.PlayerSystems;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using EventBus = Core.Common.EventBus;

namespace _Game.Scripts.InteractionSystems
{
    public class InteractionController : IDisposable
    {
        private List<AbstractInteractable> _currentAbstractInteractables =  new List<AbstractInteractable>();
        private AbstractInteractable _currentAbstractInteractable;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly PlayerModel _playerModel;
        private readonly EventBus _eventBus;

        public AbstractInteractable CurrentAbstractInteractable => _currentAbstractInteractable;
        
        private bool _updateIsActive;
        
        public InteractionController(InputSystem_Actions inputSystemActions, PlayerModel playerModel, EventBus eventBus)
        {
            _inputSystemActions = inputSystemActions;
            _playerModel = playerModel;
            _eventBus = eventBus;
            
            _playerModel.CanInteract.Subscribe(OnCanInteractChanged);
            
            _eventBus.Subscribe<SetCurrentInteractableSignal, AbstractInteractable>(this, SetCurrentInteractable);
            _eventBus.Subscribe<RemoveCurrentInteractableSignal, AbstractInteractable>(this, RemoveCurrentInteractable);
        }

        private void SelectNearestCurrentInteractables()
        {
            AbstractInteractable nearestInteractable = _currentAbstractInteractables[0];
            Vector2 playerPos = _playerModel.Transformation.Position.Value;

            foreach (var abstractInteractable in _currentAbstractInteractables)
            {
                if(nearestInteractable == abstractInteractable)
                    continue;
                
                Vector2 itemPos = abstractInteractable.AbstractInteractableModel.Position;
                
                float currentItemDistance = Vector2.Distance(playerPos, nearestInteractable.AbstractInteractableModel.Position);
                float nextItemDistance = Vector2.Distance(playerPos, itemPos);
                
                if(currentItemDistance > nextItemDistance)
                {
                    nearestInteractable = abstractInteractable;
                }
            }

            if(_currentAbstractInteractable != null)
                _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = false;
            
            _currentAbstractInteractable = nearestInteractable;
            _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = true;
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
            _currentAbstractInteractables.Add(abstractInteractable);

            StartUpdate();
        }

        private void RemoveCurrentInteractable(AbstractInteractable abstractInteractable)
        {
            _currentAbstractInteractables.Remove(abstractInteractable);

            abstractInteractable.AbstractInteractableModel.IsSelected.Value = false;

            if (_currentAbstractInteractables.Count == 0)
            {
                _currentAbstractInteractable = null;
                StopUpdate();
            }
        }

        private void StartUpdate()
        {
            if (_updateIsActive)
                return;
            
            Observable.EveryUpdate().Subscribe(_ => SelectNearestCurrentInteractables()).AddTo(_disposables);
            _updateIsActive = true;
        }

        private void StopUpdate()
        {
            _disposables.Clear();
            _updateIsActive = false;
        }
        
        public void Dispose()
        {
            _playerModel.CanInteract.Unsubscribe(OnCanInteractChanged);
            _eventBus.Unsubscribe<SetCurrentInteractableSignal>(this);
            _disposables.Dispose();
        }
    }
}