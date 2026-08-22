using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerEmptyMotionState : PlayerMotionState
    {
        public PlayerEmptyMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
        }
    }
}