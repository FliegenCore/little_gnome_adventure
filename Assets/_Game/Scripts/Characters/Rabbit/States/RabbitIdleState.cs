using _Game.Scripts.FSM;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitIdleState : RabbitState
    {
        public RabbitIdleState(Fsm fsm, RabbitModel rabbitModel) : base(fsm, rabbitModel)
        {
            
        }

        public override void Enter()
        {
            _rabbitModel.AnimationModel.IsIdleAnimation.Value = true;
        }

        public override void Exit()
        {
            _rabbitModel.AnimationModel.IsIdleAnimation.Value = false;
        }
    }
}