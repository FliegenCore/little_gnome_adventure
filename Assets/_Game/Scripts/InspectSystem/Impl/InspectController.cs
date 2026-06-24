using System;
using System.Collections.Generic;
using _Game.Scripts.InspectSystem;
using _Game.Scripts.InspectSystem.Camera;
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
        private readonly InspectCamera _inspectCamera;
        private readonly Dictionary<string, InspectModel> _inspectModels = new Dictionary<string, InspectModel>();
        private readonly Dictionary<string, InspectInputHandler> _inputs;
        private readonly InspectInputHandler _baseInputHandler;
        
        private Queue<InspectModel> _inspectModelsQueue = new Queue<InspectModel>();
        private Queue<string> _inspectNamesQueue = new Queue<string>();
        
        private InspectModel _currentInspectModel;
        private string _currentInspectModelId;
        
        private InspectController(EventBus eventBus, InputSystem_Actions inputSystemActions, InspectCamera inspectCamera)
        {
            _inspectCamera      = inspectCamera;
            _eventBus           = eventBus;
            _inputSystemActions = inputSystemActions;
            _baseInputHandler   = new InspectInputHandler(inputSystemActions);
            _inputs             = new Dictionary<string, InspectInputHandler>();
            
            _eventBus.Subscribe<ShowInspectWindowByIdSignal, string>(this, Show);
        }
        
        public void AddInspectModel(string id, InspectModel inspectModel, InspectInputHandler inspectInputHandler = null)
        {
            Debug.Log($"{nameof(InspectController)} Register " + id + " inspect");
            
            _inspectModels.Add(id, inspectModel);
            if(inspectInputHandler != null)
                _inputs.Add(id, inspectInputHandler);
            else
                _inputs.Add(id, _baseInputHandler);
        }
        
        public void EnableInput()
        {
            _inputSystemActions.Player.Back.performed += Hide;
            _inputs[_currentInspectModelId].EnableInput(_currentInspectModel);
        }

        public void DisableInput()
        {
            _inputSystemActions.Player.Back.performed -= Hide;
            _inputs[_currentInspectModelId].DisableInput();
        }
        
        private void Show(string id)
        {
            bool isMultyInspect = false;
            
            if (_currentInspectModel != null)
            {
                _inspectModelsQueue.Enqueue(_currentInspectModel);
                _inspectNamesQueue.Enqueue(_currentInspectModelId);
                
                DisableInput();
                isMultyInspect = true;
            }
            
            _currentInspectModelId = id;
            _inspectModels[id].IsOpen.Value = true;
            _currentInspectModel = _inspectModels[id];

            if (isMultyInspect)
            {
                EnableInput();
            }
            else
            {
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerInspectState));
            
                _inspectCamera.gameObject.SetActive(true);
            }
        }

        private void Hide(InputAction.CallbackContext _)
        {
            if (_currentInspectModel == null)
            {
                return;
            }

            _currentInspectModel.IsOpen.Value = false;
            _currentInspectModel = null;

            if (_inspectModelsQueue.Count > 0)
            {
                _inspectModelsQueue.Dequeue();
                string name = _inspectNamesQueue.Dequeue();
                
                DisableInput();
                Show(name);
                EnableInput();
                
                return;
            }
            
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));

            _inspectCamera.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<ShowInspectWindowByIdSignal>(this);
            _baseInputHandler?.Dispose();
        }
    }
}