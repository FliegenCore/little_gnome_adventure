using _Game.Scripts.FSM;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitState : FsmAbstractState
    {
        protected readonly RabbitModel _rabbitModel;
        
        public RabbitState(Fsm fsm, RabbitModel rabbitModel) : base(fsm)
        {
            _rabbitModel = rabbitModel;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
        }
    }
}