using _Game.Scripts.FSM;

namespace _Game.Scripts.GameStateSystems.States
{
    public class GameplayState : GameState
    {
        public GameplayState(Fsm fsm) : base(fsm)
        {
            
        }

        public override void Enter()
        {
            
            //enable player input
            //enableа  playerUI 
        }

        public override void Exit()
        {
            //Disable pleyer input
            //disable playerUI 
        }
    }
}