using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerDisabledMotionState : PlayerState
    {
        public PlayerDisabledMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}