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
            
            _playerModel.Transformation.Direction.Value = _playerModel.MoveDirectionInput.GetDirection() * _playerModel.MoveSpeed;
        }
    }
}