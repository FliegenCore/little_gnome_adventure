using System;
using _Game.Scripts.FSM;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.MotionStates;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerBaseState : PlayerState
    {
        private readonly Inventory _inventory;
        private readonly EventBus _eventBus;
        
        public PlayerBaseState(Fsm fsm, PlayerModel playerModel, Inventory inventory, EventBus eventBus) : base(fsm, playerModel)
        {
            _eventBus = eventBus;
            _inventory = inventory;
        }

        public override void Enter()
        {
            _playerModel.MoveDirectionInput.SetCanMove(true);
            _playerModel.CanInteract.Value = true;
            _inventory.EnableOpenCloseInput();
        }

        public override void Exit()
        {
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            _playerModel.MoveDirectionInput.SetCanMove(false);
            _playerModel.CanInteract.Value = false;
            _inventory.DisableOpenCloseInput();
        }
    }
}