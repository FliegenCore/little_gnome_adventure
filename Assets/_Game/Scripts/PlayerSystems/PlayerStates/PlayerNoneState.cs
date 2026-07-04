using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerNoneState : PlayerState
    {
        public PlayerNoneState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
           
        }
    }
}