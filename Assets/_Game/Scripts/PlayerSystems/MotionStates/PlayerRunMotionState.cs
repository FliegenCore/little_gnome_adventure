using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerRunMotionState: PlayerMotionState
    {
        public PlayerRunMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _playerModel.AnimationPlayerModel.IsRun.Value = true;
        }

        public override void Update(float deltaTime)
        {
            if (!_playerModel.MoveDirectionInput.GetCanMove())
            {
                _playerModel.Transformation.Direction.Value = Vector2.zero;
                _fsm.SetState<PlayerIdleMotionState>();
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
            _playerModel.Transformation.Direction.Value = moveDirection * _playerModel.RunSpeed;
    
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _playerModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _playerModel.Transformation.Scale.Value = currentScale;
            }

            if (!_playerModel.MoveDirectionInput.GetIsSprint())
            {
                _fsm.SetState<PlayerMoveMotionState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _playerModel.Transformation.Direction.Value = Vector2.zero;
            _playerModel.AnimationPlayerModel.IsRun.Value = false;
        }
    }
}