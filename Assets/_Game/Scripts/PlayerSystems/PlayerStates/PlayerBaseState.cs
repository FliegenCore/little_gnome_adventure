using _Game.Scripts.FSM;
using _Game.Scripts.InventorySystem;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerBaseState : PlayerState
    {
        private readonly Inventory _inventory;
        
        public PlayerBaseState(Fsm fsm, PlayerModel playerModel, Inventory inventory) : base(fsm, playerModel)
        {
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
            _playerModel.MoveDirectionInput.SetCanMove(false);
            _playerModel.CanInteract.Value = false;
            _inventory.DisableOpenCloseInput();
        }
    }
}