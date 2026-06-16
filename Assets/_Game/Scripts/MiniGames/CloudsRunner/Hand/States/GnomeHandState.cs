using _Game.Scripts.FSM;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandState : FsmAbstractState
    {
        protected readonly GnomeHandModel _handModel;
        
        public GnomeHandState(Fsm fsm, GnomeHandModel handModel) : base(fsm)
        {
            _handModel = handModel;
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