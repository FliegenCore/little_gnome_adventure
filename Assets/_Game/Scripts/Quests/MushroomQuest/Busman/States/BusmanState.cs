using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanState : FsmAbstractState
    {
        protected readonly AnimationControl _animationControl;
        
        public BusmanState(Fsm fsm, AnimationControl busmanAniamtionContiol) : base(fsm)
        {
            _animationControl = busmanAniamtionContiol;
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