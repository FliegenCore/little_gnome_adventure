using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandIdleState : GnomeHandState
    {
        private bool _isGrounded = true;
        
        public GnomeHandIdleState(Fsm fsm, GnomeHandModel handModel) : base(fsm, handModel)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _handModel.AnimationModel.IsMoveAnimation.Value = false;
            
            _handModel.MoveDirectionInput.JumpEvent += OnJump;
            _handModel.GroundChecker.OnGroundChange += OnGroundChanged;
        }

        public override void Exit()
        {
            base.Exit();
            
            _handModel.MoveDirectionInput.JumpEvent -= OnJump;
            _handModel.GroundChecker.OnGroundChange -= OnGroundChanged;
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (_handModel.MoveDirectionInput.GetDirection() != Vector2.zero && 
                _handModel.MoveDirectionInput.GetCanMove())
            {
                _fsm.SetState<GnomeHandMoveState>();
            }
        }

        private void OnJump()
        {
            if (_handModel.GroundChecker.OnGround)
            {
                _fsm.SetState<GnomeHandJumpState>();
            }
        }

        private void OnGroundChanged(bool isGround)
        {
            _isGrounded = isGround;
            
            if (!isGround)
            {
                _handModel.CurrentJumpVelocity.Value = -0.5f;
                _fsm.SetState<GnomeHandAirState>();
            }
        }
    }
}