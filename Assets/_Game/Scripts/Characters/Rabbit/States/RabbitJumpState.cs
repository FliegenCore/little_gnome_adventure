using _Game.Scripts.FSM;
using UniRx;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitJumpState : RabbitState
    {
        public RabbitJumpState(Fsm fsm, RabbitModel rabbitModel) : base(fsm, rabbitModel)
        {
            
        }
        
        public override void Enter()
        {
            _rabbitModel.AnimationModel.IsJumpAnimation.Value = true;

            _rabbitModel.AnimationModel.JumpIsEnded.Subscribe(_ =>
            {
                _rabbitModel.IsActive.Value = false;
            });
        }

        public override void Exit()
        {
            _rabbitModel.AnimationModel.IsIdleAnimation.Value = false;
        }
        
        
    }
}