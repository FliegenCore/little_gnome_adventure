using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandMoveState : GnomeHandState
    {
        public GnomeHandMoveState(Fsm fsm, GnomeHandModel handModel) : base(fsm, handModel)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _handModel.AnimationModel.IsMoveAnimation.Value = true;
            
            _handModel.MoveDirectionInput.JumpEvent += OnJump;
            _handModel.GroundChecker.OnGroundChange += OnGroundChanged;
        }
        
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            Vector2 currentDirection = _handModel.Transformation.Direction.Value;
            
            float targetX = _handModel.MoveDirectionInput.GetDirection().x * _handModel.MoveSpeed;
            
            if (_handModel.MoveDirectionInput.GetDirection() == Vector2.zero || 
                !_handModel.MoveDirectionInput.GetCanMove())
            {
                currentDirection.x = 0;
                _handModel.Transformation.Direction.Value = currentDirection;
                _fsm.SetState<GnomeHandIdleState>();
                return;
            }
            
            currentDirection.x = targetX;
            _handModel.Transformation.Direction.Value = currentDirection;
        }

        public override void Exit()
        {
            base.Exit();
            
            _handModel.AnimationModel.IsMoveAnimation.Value = false;
            
            _handModel.MoveDirectionInput.JumpEvent -= OnJump;
            _handModel.GroundChecker.OnGroundChange -= OnGroundChanged;
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
            if (!isGround)
            {
                _handModel.CurrentJumpVelocity.Value = -0.5f;
                _fsm.SetState<GnomeHandAirState>();
            }
        }
    }
}