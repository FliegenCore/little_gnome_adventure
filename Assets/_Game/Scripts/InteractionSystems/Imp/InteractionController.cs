using System;
using System.Collections.Generic;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using UniRx;
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
        private readonly CompositeDisposable _interactableOnPointDisposables = new CompositeDisposable();
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

        public void StartUpdate()
        {
            if (_updateIsActive)
                return;
            
            if (_currentAbstractInteractable != null)
            {
                _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = true;
            }
            
            Observable.EveryUpdate()
                .Subscribe(_ => SelectNearestCurrentInteractables())
                .AddTo(_disposables);
            
            _updateIsActive = true;
        }

        public void StopUpdate()
        {
            _disposables.Clear();
            _updateIsActive = false;
            
            if (_currentAbstractInteractable != null)
            {
                _currentAbstractInteractable.AbstractInteractableModel.IsSelected.Value = false;
            }
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

        private void Deactivate()
        {
            _inputSystemActions.Player.Interact.performed -= Interact;
        }

        private void Interact(InputAction.CallbackContext _)
        {
            if (!CanInteract())
            {
                return;
            }
            
            _interactableOnPointDisposables?.Clear();
            
            if (_currentAbstractInteractable.InteractableView.InteractPoint != null)
            {
                if (_currentAbstractInteractable.InteractableView.BoxCollider2D != null)
                {
                    _currentAbstractInteractable.InteractableView.BoxCollider2D.enabled = false;
                }
                
                StopUpdate();
                
                _playerModel.AutoMoveTransform = _currentAbstractInteractable.InteractableView.InteractPoint;
                _playerModel.LastInteractableObject = _currentAbstractInteractable.InteractableView.transform;
                
                _playerModel.OnPosition.Subscribe(_ =>
                {
                    if (_currentAbstractInteractable.InteractableView.BoxCollider2D != null)
                    {
                        _currentAbstractInteractable.InteractableView.BoxCollider2D.enabled = true;
                    }
                    
                    _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
                    
                    Observable.Timer(TimeSpan.FromSeconds(0.25f))
                        .Subscribe(_ =>
                        {
                            _currentAbstractInteractable.Interact(() =>
                            {
                                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
                            });
                    
                            StartUpdate();
                        });
                })
                .AddTo(_interactableOnPointDisposables);
                
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerAutoMoveState));
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerAutoMoveMotionState));
            }
            else
            {
                _currentAbstractInteractable.Interact(null);
            }
        }

        public void InteractWithItem(AbstractInteractable abstractInteractable)
        {
            if (abstractInteractable.InteractableView.InteractPoint != null)
            {
                if (abstractInteractable.InteractableView.BoxCollider2D != null)
                {
                    abstractInteractable.InteractableView.BoxCollider2D.enabled = false;
                }
                
                StopUpdate();
                
                _playerModel.AutoMoveTransform = abstractInteractable.InteractableView.InteractPoint;
                _playerModel.LastInteractableObject = abstractInteractable.InteractableView.transform;
                
                _playerModel.OnPosition.Subscribe(_ =>
                    {
                        if (abstractInteractable.InteractableView.BoxCollider2D != null)
                        {
                            abstractInteractable.InteractableView.BoxCollider2D.enabled = true;
                        }
                    
                        abstractInteractable.Interact(() =>
                        {
                            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
                            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
                        });
                    
                        StartUpdate();
                    })
                    .AddTo(_interactableOnPointDisposables);
                
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerAutoMoveState));
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerAutoMoveMotionState));
            }
            else
            {
                abstractInteractable.CustomBehaviour.Interact(null);
            }
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

        
        
        public void Dispose()
        {
            _playerModel.CanInteract.Unsubscribe(OnCanInteractChanged);
            _eventBus.Unsubscribe<SetCurrentInteractableSignal>(this);
            _interactableOnPointDisposables?.Dispose();
            _disposables.Dispose();
        }
    }
}