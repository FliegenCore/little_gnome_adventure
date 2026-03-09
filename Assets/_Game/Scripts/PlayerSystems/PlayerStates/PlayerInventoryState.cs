using _Game.Scripts.FSM;
using _Game.Scripts.InventorySystem;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerInventoryState : PlayerState
    {
        private readonly Inventory _inventory;
        private readonly EventBus _eventBus;
        
        public PlayerInventoryState(Fsm fsm, PlayerModel playerModel, Inventory inventory) : base(fsm, playerModel)
        {
            _inventory = inventory;
        }

        public override void Enter()
        {
            _inventory.EnableOpenCloseInput();
            _playerModel.AnimationPlayerModel.InInventory.Value = true;
        }

        public override void Exit()
        {
            _playerModel.AnimationPlayerModel.InInventory.Value = false;
            _inventory.DisableOpenCloseInput();
        }
    }
}