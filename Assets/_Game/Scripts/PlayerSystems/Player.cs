using System;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.UpdateSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public class Player : IUpdateListener, ITeleportable, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly Inventory _inventory;
        private readonly Fsm _motionStateMachine;
        private readonly Fsm _playerStateMachine;
        private readonly EventBus _eventBus;

        public readonly InteractionController InteractionController;
        public readonly PlayerView PlayerView;
        
        
        public Player(
            PlayerModel playerModel, 
            PlayerView playerView,
            Fsm motionStateMachine,
            Fsm playerStateMachine, 
            InteractionController interactionController,
            Inventory inventory,
            EventBus eventBus)
        {
            _inventory = inventory;
            _eventBus = eventBus;
            InteractionController = interactionController;
            _playerModel = playerModel;
            PlayerView = playerView;
            _motionStateMachine = motionStateMachine;
            _playerStateMachine = playerStateMachine;
            
            _eventBus.Subscribe<SetPlayerStateSignal, Type>(this, SetPlayerState);
            _eventBus.Subscribe<SetPlayerMotionStateSignal, Type>(this, SetPlayerMotionState);
        }

        
        private void SetPlayerState(Type type)
        {
            _playerStateMachine.SetState(type);
        }

        private void SetPlayerMotionState(Type type)
        {
            _motionStateMachine.SetState(type);
        }

        public void Update(float deltaTime)
        {
            _motionStateMachine?.Update(deltaTime);
        }

        public void Teleport(Vector2 position)
        {
            _playerModel.Transformation.Position.Value = position;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SetPlayerStateSignal>(this);
            _eventBus.Unsubscribe<SetPlayerMotionStateSignal>(this);
            _eventBus.Unsubscribe<DialogueEventSignal>(this);
        }
    }
}