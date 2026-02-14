using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerMoveMotionState : PlayerMotionState
    {
        public PlayerMoveMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _playerModel.AnimationPlayerModel.IsMove.Value = true;
        }

        public override void Update(float deltaTime)
        {
            if (!_playerModel.MoveDirectionInput.GetCanMove())
            {
                return;
            }
            
            base.Update(deltaTime);

            if (_playerModel.MoveDirectionInput.GetDirection() == Vector2.zero)
            {
                _fsm.SetState<PlayerIdleMotionState>();
                _playerModel.Transformation.Direction.Value = Vector2.zero;
        
                return;
            }
    
            Vector2 moveDirection = _playerModel.MoveDirectionInput.GetDirection();
            _playerModel.Transformation.Direction.Value = moveDirection * _playerModel.MoveSpeed;
    
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _playerModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _playerModel.Transformation.Scale.Value = currentScale;
            }
        }

        public override void Exit()
        {
            base.Exit();
            _playerModel.AnimationPlayerModel.IsMove.Value = false;
        }
    }
}