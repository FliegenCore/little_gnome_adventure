using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandAirState : GnomeHandState
    {
        public GnomeHandAirState(Fsm fsm,
            GnomeHandModel handModel
        ) : base(fsm, handModel)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            //_handModel.AnimationModel.IsJumpAnimation.Value = true;
            _handModel.GroundChecker.OnGroundChange += OnGroundChanged;
        }

        public override void Exit()
        {
            base.Exit();
            
            //_handModel.AnimationModel.IsJumpAnimation.Value = false;
            _handModel.GroundChecker.OnGroundChange -= OnGroundChanged;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _handModel.CurrentJumpVelocity.Value -= _handModel.GravityForce * deltaTime;
            
            Vector2 currentDirection = _handModel.Transformation.Direction.Value;
            
            if (_handModel.MoveDirectionInput.GetCanMove())
            {
                currentDirection.x = _handModel.MoveDirectionInput.GetDirection().x * _handModel.MoveSpeed * 0.8f;
            }
            
            currentDirection.y = _handModel.CurrentJumpVelocity.Value;
            
            _handModel.Transformation.Direction.Value = currentDirection;
        }

        private void OnGroundChanged(bool isGround)
        {
            if (isGround)
            {
                if (_handModel.MoveDirectionInput.GetDirection() != Vector2.zero && 
                    _handModel.MoveDirectionInput.GetCanMove())
                {
                    _fsm.SetState<GnomeHandMoveState>();
                }
                else
                {
                    _fsm.SetState<GnomeHandIdleState>();
                }
            }
        }
    }
}