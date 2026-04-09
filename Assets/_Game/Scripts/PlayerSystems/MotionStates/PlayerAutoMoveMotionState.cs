using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerAutoMoveMotionState : FsmAbstractState
    {
        protected readonly PlayerModel _playerModel;
        
        public PlayerAutoMoveMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm)
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