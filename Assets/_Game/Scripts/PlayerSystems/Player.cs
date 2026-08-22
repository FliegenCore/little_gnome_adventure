using System;
using System.Collections.Generic;
using _Game.Scripts.CameraSystem;
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
        private readonly Inventory _inventory;
        private readonly CameraController _cameraController;
        private readonly Fsm _motionStateMachine;
        private readonly Fsm _playerStateMachine;
        private readonly EventBus _eventBus;

        public readonly PlayerModel PlayerModel;
        public readonly InteractionController InteractionController;
        public readonly PlayerView PlayerView;
        
        public Player(
            PlayerModel playerModel, 
            PlayerView playerView,
            Fsm motionStateMachine,
            Fsm playerStateMachine, 
            InteractionController interactionController,
            Inventory inventory,
            EventBus eventBus,
            CameraController cameraController
            )
        {
            _inventory            = inventory;
            _eventBus             = eventBus;
            InteractionController = interactionController;
            PlayerModel           = playerModel;
            PlayerView            = playerView;
            _motionStateMachine   = motionStateMachine;
            _playerStateMachine   = playerStateMachine;
            _cameraController     = cameraController;
            
            _eventBus.Subscribe<SetPlayerStateSignal, Type, object>(this, SetPlayerState);
            _eventBus.Subscribe<SetPlayerMotionStateSignal, Type, object>(this, SetPlayerMotionState);
            _eventBus.Subscribe<SetPlayerActiveSignal, bool>(this, SetActive);
        }
        
        public  void SetPlayerState<T>(Type type, T parameter = default)
        {
            if (EqualityComparer<T>.Default.Equals(parameter, default(T)))
            {
                _playerStateMachine.SetState(type);
            }
            else
                _playerStateMachine.SetStateWithParameter(type, parameter: parameter);
        }
        
        public  void SetPlayerState(Type type)
        {
            _playerStateMachine.SetState(type);
        }

        public void SetPlayerMotionState<T>(Type type, T parameter = default)
        {
            if (EqualityComparer<T>.Default.Equals(parameter, default(T)))
            {
                _motionStateMachine.SetState(type);
            }
            else
                _motionStateMachine.SetStateWithParameter(type, parameter: parameter);
        }
        
        public void SetPlayerMotionState(Type type)
        {
            _motionStateMachine.SetState(type);
        }

        public void Update(float deltaTime)
        {
            _motionStateMachine?.Update(deltaTime);
            _playerStateMachine?.Update(deltaTime);
        }

        public void Teleport(Vector2 position)
        {
            PlayerModel.Transformation.Position.Value = position;
            _cameraController.SetPosition(new Vector3(position.x, position.y, -5f));
        }

        private void SetActive(bool isActive)
        {
            PlayerModel.IsActive.Value = isActive;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SetPlayerStateSignal>(this);
            _eventBus.Unsubscribe<SetPlayerMotionStateSignal>(this);
            _eventBus.Unsubscribe<DialogueEventSignal>(this);
        }
    }
}