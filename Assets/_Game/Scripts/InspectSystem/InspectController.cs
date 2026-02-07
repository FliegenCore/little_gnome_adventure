using System;
using System.Collections.Generic;
using _Game.Scripts.InspectSystem;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectController : IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly Dictionary<string, InspectModel> _inspectModels = new Dictionary<string, InspectModel>();
        private InspectInputHandler _inspectInputHandler;
        private InspectModel _currentInspectModel;
        
        private InspectController(EventBus eventBus, InputSystem_Actions inputSystemActions)
        {
            _eventBus = eventBus;
            _inputSystemActions = inputSystemActions;
            _inspectInputHandler = new InspectInputHandler(inputSystemActions);
            
            _eventBus.Subscribe<ShowInspectWindowByIdSignal, string>(this, Show);
        }

        public void EnableInput()
        {
            _inputSystemActions.Player.Back.performed += Hide;
            
            _inspectInputHandler.EnableInput(_currentInspectModel);
        }

        public void DisableInput()
        {
            _inputSystemActions.Player.Back.performed -= Hide;
            _inspectInputHandler.DisableInput();
        }
        
        public void AddInspectModel(string id, InspectModel inspectModel)
        {
            Debug.Log("Register " + id + " inspect");
            _inspectModels.Add(id, inspectModel);
        }
        
        private void Show(string id)
        {
            _inspectModels[id].IsOpen.Value = true;
            _currentInspectModel = _inspectModels[id];
            
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerInspectState));
        }

        private void Hide(InputAction.CallbackContext _)
        {
            if (_currentInspectModel == null)
            {
                return;
            }

            _currentInspectModel.IsOpen.Value = false;
            _currentInspectModel = null;
            
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ShowInspectWindowByIdSignal>(this);
            _inspectInputHandler?.Dispose();
        }
    }
}