using System;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.Animations;

namespace _Game.Scripts.Quests.MushroomQuest.Busman.States
{
    public class BusmanDoorOpenState : BusmanState, INotifyCallbackState
    {
        private Action _callback;
        
        public BusmanDoorOpenState(Fsm fsm, AnimationControl busmanAniamtionContiol) : base(fsm, busmanAniamtionContiol)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _animationControl.SetAnimation(0, "ticket", false, OnDoorOpen);
        }

        public void SetCallback(Action callback)
        {
            _callback = callback;
        }

        private void OnDoorOpen()
        {
            _callback?.Invoke();
            _animationControl.SetAnimation(0, "ready");
        }
    }
}