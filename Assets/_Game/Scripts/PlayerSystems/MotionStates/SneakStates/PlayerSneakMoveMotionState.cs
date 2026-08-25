using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerSneakMoveMotionState : PlayerMotionState
    {
        private readonly float _speed = 2;
        
        public PlayerSneakMoveMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            
        }

        public override void Exit()
        {
            _playerModel.AnimationPlayerModel.IsMoveSneak.Value = false;
        }

        public override void Enter()
        {
            _playerModel.AnimationPlayerModel.IsMoveSneak.Value = true;
        }

        public override void Update(float deltaTime)
        {
            if (!_playerModel.MoveDirectionInput.GetCanMove())
            {
                _playerModel.Transformation.Direction.Value = Vector2.zero;
                _fsm.SetState<PlayerIdleSneakMotionState>();
                return;
            }
            
            if (_playerModel.MoveDirectionInput.GetDirection() == Vector2.zero)
            {
                _fsm.SetState<PlayerIdleSneakMotionState>();
                _playerModel.Transformation.Direction.Value = Vector2.zero;
        
                return;
            }
            
            Vector2 moveDirection = _playerModel.MoveDirectionInput.GetDirection();
            _playerModel.Transformation.Direction.Value = moveDirection * _playerModel.MoveSpeed / _speed;
    
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _playerModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _playerModel.Transformation.Scale.Value = currentScale;
            }
        }
    }
}