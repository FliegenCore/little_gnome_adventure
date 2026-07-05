using System;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _Game.Scripts.CutsceneSystem.Impl
{
    public class CutsceneManager : ICutsceneManager, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly InputSystem_Actions _inputSystemActions;
        
        private ACutscene _lastCutscene;
        private ACutscene _activeCutscene;
    
        private CutsceneManager(EventBus eventBus, InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _eventBus = eventBus;
        }
        
        public void Play(ACutscene cutscene, Action onComplete = null)
        {
            _lastCutscene = cutscene;
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDisabledMotionState));
            _activeCutscene = cutscene;
            _inputSystemActions.Player.Interact.performed += SkipCutscene;
            
            cutscene.Play(() =>
            {
                _inputSystemActions.Player.Interact.performed -= SkipCutscene;
                
                _activeCutscene = null;
                onComplete?.Invoke();
                
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
            });
        }

        private void SkipCutscene(InputAction.CallbackContext _)
        {
            if (_activeCutscene is ISkipableCutscene skipableCutscene)
            {
                _inputSystemActions.Player.Interact.performed -= SkipCutscene;
                _activeCutscene = null;
                skipableCutscene.Skip();
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
            }
        }

        public void Dispose()
        {
            _inputSystemActions?.Dispose();
        }
    }
}