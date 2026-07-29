using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordInspectView : InspectAbstractView
    {
        private const string POSITION_ANIMATION_NAME = "possition/";
        private const string BAR_ANIMATION_NAME = "bar/bar";
        
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
            
            SetPointPosition(0);
            ResetBarFill();
        }

        private void SetPointPosition(int position)
        {
            string positionString = POSITION_ANIMATION_NAME + position;
            
            _animationControl.SetAnimation(0, positionString, false);
        }

        private void SetBarFill(int bar)
        {
            string barString = POSITION_ANIMATION_NAME + bar;
            
            _animationControl.SetAnimation(1, barString, false);
        }

        private void ResetBarFill()
        {
            _animationControl.SetAnimation(1, BAR_ANIMATION_NAME + "0empty", false);
        }
    }
}