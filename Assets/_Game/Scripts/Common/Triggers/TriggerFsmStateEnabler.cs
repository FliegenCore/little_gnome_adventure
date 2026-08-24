using System;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.Characters.Rabbit.OtherComponents
{
    public class TriggerFsmStateEnabler
    {
        public event Action PrepareSetState;
        public event Action PrepareSetExitState;
        public event Action AfterSetState;
        public event Action AfterSetExitState;
        
        private readonly Fsm _stateMachine;
        private readonly IContactTriggerProvider _contactTriggerProvider;
        private readonly Type _state;
        private readonly Type _afterExitState;
        private readonly bool _unsubscribeAfterTrigger;
        private readonly bool _unsubscribeAfterExit;
        
        public TriggerFsmStateEnabler(
            Fsm stateMachine,
            IContactTriggerProvider contactTriggerProvider,
            Type state,
            bool unsubscribeAfterTrigger,
            Type afterExitState = null,
            bool unsubscribeAfterExit = false
            )
        {
            _stateMachine            = stateMachine;
            _contactTriggerProvider  = contactTriggerProvider;
            _state                   = state;
            _afterExitState          = afterExitState;
            _unsubscribeAfterTrigger = unsubscribeAfterTrigger;
            _unsubscribeAfterExit    = unsubscribeAfterExit;
            
            _contactTriggerProvider.OnEnter += OnEnter;
            _contactTriggerProvider.OnExit  += OnExit;
        }

        private void OnEnter(Collider2D _)
        {
            if(_unsubscribeAfterTrigger)
                _contactTriggerProvider.OnEnter -= OnEnter;
            
            PrepareSetState?.Invoke();
            _stateMachine.SetState(_state);
            AfterSetState?.Invoke();
        }

        private void OnExit(Collider2D _)
        {
            if (_afterExitState == null)
                return;
            
            if(_unsubscribeAfterExit)
                _contactTriggerProvider.OnExit -= OnExit;
            
            PrepareSetExitState?.Invoke();
            _stateMachine.SetState(_afterExitState);
            AfterSetExitState?.Invoke();
        }
    }
}