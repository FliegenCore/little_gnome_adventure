using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanIdleState : BusmanState
    {
        public BusmanIdleState(Fsm fsm, AnimationControl busmanAniamtionContiol) : base(fsm, busmanAniamtionContiol)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _animationControl.SetAnimation(0, "idle");
        }
    }
}