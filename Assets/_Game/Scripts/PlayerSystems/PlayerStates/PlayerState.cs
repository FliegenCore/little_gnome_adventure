using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerState : FsmAbstractState
    {
        protected readonly PlayerModel _playerModel;
        
        public PlayerState(Fsm fsm, PlayerModel playerModel) : base(fsm)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}