using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerIdleSneakMotionState: PlayerMotionState
    {
        public PlayerIdleSneakMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            _playerModel.AnimationPlayerModel.IsIdleSneak.Value = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            if (_playerModel.MoveDirectionInput.GetDirection() != Vector2.zero && _playerModel.MoveDirectionInput.GetCanMove())
            {
                _fsm.SetState<PlayerSneakMoveMotionState>();
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            
            _playerModel.AnimationPlayerModel.IsIdleSneak.Value = false;
        }
    }
}