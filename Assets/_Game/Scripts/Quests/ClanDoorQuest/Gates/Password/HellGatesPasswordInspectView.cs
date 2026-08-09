using System;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordInspectView : InspectAbstractView
    {
        private const string POSITION_ANIMATION_NAME = "possition/";
        private const string PRESS_BUTTON_ANIMATION_IDLE = "button/idle";
        private const string PRESS_BUTTON_ANIMATION_PRESS = "button/press";
        private const string BAR_ANIMATION_NAME = "bar/bar";
        private const string REJECT_BAR_ANIMATION_NAME = "bar/barError";
        private const string ACCEPT_BAR_ANIMATION_NAME = "bar/barGood";
        
        [SerializeField] private AnimationControl _animationControl;

        private HellGatesPasswordModel _hellGatesPasswordModel;
        
        public void Construct(HellGatesPasswordModel passwordModel)
        {
            _hellGatesPasswordModel = passwordModel;
            
            _hellGatesPasswordModel.CurrentIndex
                .Subscribe(SetPointPosition)
                .AddTo(gameObject);
            
            _hellGatesPasswordModel.WritedCount
                .Subscribe(SetBarFill)
                .AddTo(gameObject);
            
            _hellGatesPasswordModel.PressButton
                .Subscribe(_ => OnPressButton())
                .AddTo(gameObject);
            
            _animationControl.SetAnimation(2, PRESS_BUTTON_ANIMATION_IDLE, false);
            SetPointPosition(0);
            SetBarFill(0);
        }

        public void RejectBarAnimation(Action callback)
        {
            _animationControl.SetAnimation(1, REJECT_BAR_ANIMATION_NAME, false, () =>
            {
                callback?.Invoke();
            });
        }

        public void AcceptBarAnimation(Action callback)
        {
            _animationControl.SetAnimation(1, ACCEPT_BAR_ANIMATION_NAME, false, () =>
            {
                callback?.Invoke();
            });
        }

        private void SetPointPosition(int position)
        {
            string positionString = POSITION_ANIMATION_NAME + position;
            
            _animationControl.SetAnimation(0, positionString, false);
        }

        private void OnPressButton()
        {
            _animationControl.SetAnimation(2, PRESS_BUTTON_ANIMATION_PRESS, false, () =>
            {
                _animationControl.SetAnimation(2, PRESS_BUTTON_ANIMATION_IDLE, false);
            });
            
            DoActionByTime(() => _hellGatesPasswordModel.CanWrite = true);
        }

        private void SetBarFill(int bar)
        {
            if (bar == 0)
            {
                ResetBarFill();
                
                return;
            }
            
            string barString = BAR_ANIMATION_NAME + bar;
            
            _animationControl.SetAnimation(1, barString, false);
        }

        private void ResetBarFill()
        {
            _animationControl.SetAnimation(1, BAR_ANIMATION_NAME + "0empty", false);
        }

        private void DoActionByTime(Action callback)
        {
            Observable.Timer(TimeSpan.FromSeconds(0.25f)).Subscribe(_ =>
                {
                    callback?.Invoke();
                })
                .AddTo(gameObject);
        }
    }
}