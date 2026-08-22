using _Game.Scripts.FSM;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitSeatState : RabbitState
    {
        public RabbitSeatState(Fsm fsm, RabbitModel rabbitModel) : base(fsm, rabbitModel)
        {
            
        }

        public override void Enter()
        {
            _rabbitModel.AnimationModel.IsSeatAnimation.Value = true;
        }

        public override void Exit()
        {
            _rabbitModel.AnimationModel.IsSeatAnimation.Value = false;
        }
    }
}