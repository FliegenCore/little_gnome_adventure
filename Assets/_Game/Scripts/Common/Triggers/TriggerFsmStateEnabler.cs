using System;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.Characters.Rabbit.OtherComponents
{
    public class TriggerFsmStateEnabler
    {
        public event Action PreareSetState;
        public event Action AfterSetState;
        
        private readonly Fsm _stateMachine;
        private readonly IContactTriggerProvider _contactTriggerProvider;
        private readonly Type _state;
        private readonly bool _unsubscribeAfterTrigger;
        
        public TriggerFsmStateEnabler(
            Fsm stateMachine,
            IContactTriggerProvider contactTriggerProvider,
            Type state,
            bool unsubscribeAfterTrigger
            )
        {
            _stateMachine            = stateMachine;
            _contactTriggerProvider  = contactTriggerProvider;
            _state                   = state;
            _unsubscribeAfterTrigger = unsubscribeAfterTrigger;
            _contactTriggerProvider.OnEnter += OnEnter;
        }

        private void OnEnter(Collider2D _)
        {
            if(_unsubscribeAfterTrigger)
                _contactTriggerProvider.OnEnter -= OnEnter;
            
            PreareSetState?.Invoke();
            
            _stateMachine.SetState(_state);
        }
    }
}