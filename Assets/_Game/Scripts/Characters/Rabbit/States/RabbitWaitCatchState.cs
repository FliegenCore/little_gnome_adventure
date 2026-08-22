using _Game.Scripts.FSM;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitWaitCatchState : RabbitState
    {
        public RabbitWaitCatchState(Fsm fsm, RabbitModel rabbitModel) : base(fsm, rabbitModel)
        {
            
        }

        public override void Enter()
        {
            _rabbitModel.AnimationModel.IsWaitCatchAnimation.Value = true;
        }

        public override void Exit()
        {
            _rabbitModel.AnimationModel.IsWaitCatchAnimation.Value = false;
        }
    }
}