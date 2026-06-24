using _Game.Scripts.FSM;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandJumpState : GnomeHandState
    {
        public GnomeHandJumpState(Fsm fsm, GnomeHandModel handModel) : base(fsm, handModel)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _handModel.LastJumpTime.Value = 1;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            UpdateJumpCurve();
            //
        }

        private float UpdateJumpCurve()
        {
            return 0;
        }

        public override void Exit()
        {
            base.Exit();
            _handModel.LastJumpTime.Value = 0;
        }
    }
}