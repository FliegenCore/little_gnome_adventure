using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.States
{
    public class GnomeHandIdleState : GnomeHandState
    {
        public GnomeHandIdleState(Fsm fsm, GnomeHandModel handModel) : base(fsm, handModel)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _handModel.AnimationModel.IsMoveAnimation.Value = true;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (_handModel.MoveDirectionInput.GetDirection() != Vector2.zero && _handModel.MoveDirectionInput.GetCanMove())
            {
                _fsm.SetState<GnomeHandMoveState>();
            }
        }

        public override void Exit()
        {
            base.Exit();
            _handModel.AnimationModel.IsMoveAnimation.Value = false;
        }
    }
}