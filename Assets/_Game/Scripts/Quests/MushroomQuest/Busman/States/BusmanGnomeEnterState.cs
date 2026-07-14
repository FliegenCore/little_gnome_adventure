using System;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanGnomeEnterState : BusmanState, INotifyCallbackState
    {
        private Action _callback;
        
        public BusmanGnomeEnterState(Fsm fsm, AnimationControl busmanAniamtionContiol) : base(fsm, busmanAniamtionContiol)
        {
            
        }

        public override void Enter()
        {
            _animationControl.SetAnimation(0, "gnomecome", false, () =>
            {
                _callback?.Invoke();
            });
        }

        public void SetCallback(Action callback)
        {
            _callback = callback;
        }
    }
}