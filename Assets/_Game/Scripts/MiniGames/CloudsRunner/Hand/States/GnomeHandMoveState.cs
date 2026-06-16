using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandMoveState : GnomeHandState
    {
        public GnomeHandMoveState(Fsm fsm, GnomeHandModel handModel) : base(fsm, handModel)
        {
            
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (!_handModel.MoveDirectionInput.GetCanMove())
            {
                _handModel.Transformation.Direction.Value = Vector2.zero;
                _fsm.SetState<GnomeHandIdleState>();
                return;
            }

            if (_handModel.MoveDirectionInput.GetDirection() == Vector2.zero)
            {
                _fsm.SetState<GnomeHandIdleState>();
                _handModel.Transformation.Direction.Value = Vector2.zero;
        
                return;
            }
    
            Vector2 moveDirection = _handModel.MoveDirectionInput.GetDirection();
            _handModel.Transformation.Direction.Value = moveDirection * _handModel.MoveSpeed;
    
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _handModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _handModel.Transformation.Scale.Value = currentScale;
            }
        }
    }
}