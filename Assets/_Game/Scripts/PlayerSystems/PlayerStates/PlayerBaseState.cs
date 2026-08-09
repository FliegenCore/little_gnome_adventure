using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.MotionStates;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerBaseState : PlayerState
    {
        private readonly Inventory _inventory;
        private readonly EventBus _eventBus;
        private readonly CameraController _cameraController;
        private readonly PlayerView _playerView;
        private readonly InteractionController _interactionController;
        
        public PlayerBaseState(
            Fsm fsm, 
            PlayerModel playerModel,
            Inventory inventory,
            EventBus eventBus,
            CameraController cameraController,
            PlayerView playerView,
            InteractionController interactionController
            ) : base(fsm, playerModel)
        {
            _interactionController = interactionController;
            _playerView            = playerView;
            _cameraController      = cameraController;
            _eventBus              = eventBus;
            _inventory             = inventory;
        }

        public override void Enter()
        {
            _cameraController.SetFollowTarget(_playerView.transform);
            _playerModel.MoveDirectionInput.SetCanMove(true);
            _playerModel.CanInteract.Value = true;
            _interactionController.StartUpdate();
            _inventory.EnableOpenCloseInput();
        }

        public override void Exit()
        {
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            _playerModel.MoveDirectionInput.SetCanMove(false);
            _playerModel.CanInteract.Value = false;
            _inventory.DisableOpenCloseInput();
            _interactionController.StopUpdate();
        }
    }
}