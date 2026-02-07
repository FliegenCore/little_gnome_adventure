using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerInventoryState : PlayerState
    {
        public PlayerInventoryState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
        }

        public override void Enter()
        {
            //включить перемещение по инвентарю
        }

        public override void Exit()
        {
            //выключить перемещение по инвентарю
        }
    }
}