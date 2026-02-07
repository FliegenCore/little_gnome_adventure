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
            base.Enter();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (_playerModel.MoveDirectionInput.GetDirection() != Vector2.zero)
            {
                _fsm.SetState<PlayerMoveMotionState>();                
            }
        }
    }
}