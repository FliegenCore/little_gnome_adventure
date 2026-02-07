using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.InspectSystem;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerInspectState : PlayerState
    {
        private InspectController _inspectController;
        
        public PlayerInspectState(Fsm fsm, PlayerModel playerModel, InspectController inspectController) : base(fsm, playerModel)
        {
            _inspectController = inspectController;
        }

        public override void Enter()
        {
            _inspectController.EnableInput();
        }

        public override void Exit()
        {
            _inspectController.DisableInput();
        }
    }
}