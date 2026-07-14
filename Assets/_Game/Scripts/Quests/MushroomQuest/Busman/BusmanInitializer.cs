using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanInitializer
    {
        private AnimationControl _busmanAnimation;

        private Fsm _fsm;
        
        public Fsm Fsm => _fsm;
        
        public void Initialize(AnimationControl animationControl)
        {
            _busmanAnimation = animationControl;
            _fsm = new Fsm();

            CreateStates(_fsm);
        }

        private void CreateStates(Fsm fsm)
        {
            fsm.AddState(new BusmanDoorOpenState(fsm, _busmanAnimation));
            fsm.AddState(new BusmanGnomeEnterState(fsm, _busmanAnimation));
            fsm.AddState(new BusmanIdleState(fsm, _busmanAnimation));
            
            fsm.SetState<BusmanIdleState>();
        }
    }
}