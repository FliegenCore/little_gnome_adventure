using System;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.UpdateSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public class Player : IUpdateListener, ITeleportable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly InteractionController _interactionController;
        private readonly Fsm _motionStateMachine;
        private readonly Fsm _playerStateMachine;
        public readonly PlayerView PlayerView;

        private readonly EventBus _eventBus;
        
        public Player(
            PlayerModel playerModel, 
            PlayerView playerView,
            Fsm motionStateMachine,
            Fsm playerStateMachine, 
            InteractionController interactionController,
            EventBus eventBus)
        {
            _eventBus = eventBus;
            _interactionController = interactionController;
            _playerModel = playerModel;
            PlayerView = playerView;
            _motionStateMachine = motionStateMachine;
            _playerStateMachine = playerStateMachine;
            
            _eventBus.Subscribe<SetPlayerStateSignal, Type>(this, SetPlayerState);
        }

        private void SetPlayerState(Type type)
        {
            _playerStateMachine.SetState(type);
        }

        public void Update(float deltaTime)
        {
            _motionStateMachine?.Update(deltaTime);
        }

        public void Teleport(Vector2 position)
        {
            
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SetPlayerStateSignal>(this);
        }
    }
}