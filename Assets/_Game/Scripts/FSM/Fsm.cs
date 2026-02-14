using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.FSM
{
    public class Fsm
    {
        private FsmAbstractState _currentState;
        
        private Dictionary<Type, FsmAbstractState> _states = new();

        public T GetState<T>() where T : FsmAbstractState
        {
            if(_states.TryGetValue(typeof(T), out FsmAbstractState state))
            {
                return (T) state;
            }
            
            return default;
        }

        public FsmAbstractState GetState(Type stateType)
        {
            return _states.GetValueOrDefault(stateType);
        }
        
        public void AddState(FsmAbstractState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState(Type type)
        {
            var stateType = type;
            if (_currentState != null && _currentState.GetType() == stateType)
            {
                return;
            }

            if (_states.TryGetValue(stateType, out FsmAbstractState stateToSet))
            {
                _currentState?.Exit();
            
                _currentState = stateToSet;
                _currentState.Enter();
            }
        }
        
        public void SetState<T>() where T : FsmAbstractState
        {
            var stateType = typeof(T);

            if (_currentState != null && _currentState.GetType() == stateType)
            {
                return;
            }

            if (_states.TryGetValue(stateType, out FsmAbstractState stateToSet))
            {
                _currentState?.Exit();
            
                _currentState = stateToSet;
                _currentState.Enter();
            }
        }

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
        }
    }
}