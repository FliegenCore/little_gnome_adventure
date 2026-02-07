using System;
using _Game.Scripts.FSM;
using _Game.Scripts.GameStateSystems.States;
using Core.Common;

namespace _Game.Scripts.GameStateSystems
{
    public class GameStateController : IDisposable
    {
        private readonly Fsm _gameStateFsm;
        private readonly EventBus _eventBus;
        
        private GameStateController(EventBus eventBus)
        {
            _eventBus = eventBus;
            _gameStateFsm = new Fsm();
            
            CreateStates();
            
            _eventBus.Subscribe<SetGameStateSignal, Type>(this, SetState);            
        }
        
        private void CreateStates()
        {
            _gameStateFsm.AddState(new GameplayState(_gameStateFsm));
        }
        
        private void SetState(Type type)
        {
            _gameStateFsm.SetState(type);
        }

        public void Dispose()
        { 
            _eventBus.Unsubscribe<SetGameStateSignal>(this);       
        }
    }
}