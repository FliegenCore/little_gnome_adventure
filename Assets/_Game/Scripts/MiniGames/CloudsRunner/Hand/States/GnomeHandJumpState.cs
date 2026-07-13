using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandJumpState : GnomeHandState
    {
        private readonly float JUMP_HEIGHT;
        private readonly float JUMP_DURATION;
        
        public GnomeHandJumpState(Fsm fsm,
            GnomeHandModel handModel
        ) : base(fsm, handModel)
        {
            JUMP_HEIGHT = handModel.JumpHeight;
            JUMP_DURATION = handModel.JumpDuration;
            
            float maxHeightTime = JUMP_DURATION / 2;
            _handModel.GravityForce = (2 * JUMP_HEIGHT) / Mathf.Pow(JUMP_DURATION, 2);
            _handModel.StartVelocityY = (2 * JUMP_HEIGHT) / maxHeightTime;
        }

        public override void Enter()
        {
            base.Enter();
            
            _handModel.CurrentJumpVelocity.Value = _handModel.StartVelocityY;
            _fsm.SetState<GnomeHandAirState>();
        }
    }
}