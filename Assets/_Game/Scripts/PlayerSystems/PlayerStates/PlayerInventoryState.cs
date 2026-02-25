using _Game.Scripts.FSM;
using _Game.Scripts.InventorySystem;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerInventoryState : PlayerState
    {
        private readonly Inventory _inventory;
        
        public PlayerInventoryState(Fsm fsm, PlayerModel playerModel, Inventory inventory) : base(fsm, playerModel)
        {
            _inventory = inventory;
        }

        public override void Enter()
        {
            _inventory.Enable();
            //включить перемещение по инвентарю
        }

        public override void Exit()
        {
            //выключить перемещение по инвентарю
        }
    }
}