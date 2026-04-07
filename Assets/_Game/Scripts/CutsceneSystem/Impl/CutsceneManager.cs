using System;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;

namespace _Game.Scripts.CutsceneSystem.Impl
{
    public class CutsceneManager : ICutsceneManger
    {
        private readonly EventBus _eventBus;
        private ACutscene _lastCutscene;
    

        public CutsceneManager(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Play(ACutscene cutscene, Action onComplete = null)
        {
            _lastCutscene = cutscene;
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDisabledMotionState));
            
            cutscene.Play(() =>
            {
                onComplete?.Invoke();
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
            });
        }
    }
}