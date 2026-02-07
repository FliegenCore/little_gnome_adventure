using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerBaseState : PlayerState
    {
        public PlayerBaseState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            
        }

        public override void Enter()
        {
            _playerModel.MoveDirectionInput.SetCanMove(true);
            _playerModel.CanInteract.Value = true;
        }

        public override void Exit()
        {
            _playerModel.MoveDirectionInput.SetCanMove(false);
            _playerModel.CanInteract.Value = false;
        }
    }
}