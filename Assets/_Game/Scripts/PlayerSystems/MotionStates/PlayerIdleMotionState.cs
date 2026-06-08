using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerIdleMotionState : PlayerMotionState
    {
        public PlayerIdleMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            
        }

        public override void Enter()
        {
            _playerModel.AnimationPlayerModel.IsIdle.Value = true;
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (_playerModel.MoveDirectionInput.GetDirection() != Vector2.zero && _playerModel.MoveDirectionInput.GetCanMove())
            {
                if (_playerModel.MoveDirectionInput.GetIsSprint())
                {
                    _fsm.SetState<PlayerRunMotionState>();
                }
                else
                {
                    _fsm.SetState<PlayerMoveMotionState>();
                }
            }
        }

        public override void Exit()
        {
            _playerModel.AnimationPlayerModel.IsIdle.Value = false;
            base.Exit();
        }
    }
}