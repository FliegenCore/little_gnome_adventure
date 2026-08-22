using _Game.Scripts.PlayerSystems.Animations;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Characters.Rabbit.Animations
{
    public class RabbitAnimationView : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;

        private RabbitAnimationModel _rabbitAnimationModel;
        
        public void Construct(RabbitAnimationModel rabbitAnimationModel)
        {
            _rabbitAnimationModel = rabbitAnimationModel;
            
            _rabbitAnimationModel.IsIdleAnimation.Subscribe(SetIdleAnimation).AddTo(gameObject);
            _rabbitAnimationModel.IsSeatAnimation.Subscribe(SetSeatAnimation).AddTo(gameObject);
            _rabbitAnimationModel.IsWalkAnimation.Subscribe(SetWalkAnimation).AddTo(gameObject);
            _rabbitAnimationModel.IsWaitCatchAnimation.Subscribe(SetWaitAnimation).AddTo(gameObject);
        }

        private void SetSeatAnimation(bool isEnabled)
        {
            if(isEnabled)
                _animationControl.SetAnimation(0, RabbitAnimationNames.SEAT);
        }
        
        private void SetWalkAnimation(bool isEnabled)
        {
            if(isEnabled)
                _animationControl.SetAnimation(0, RabbitAnimationNames.WALK);
        }
        
        private void SetIdleAnimation(bool isEnabled)
        {
            if(isEnabled)
                _animationControl.SetAnimation(0, RabbitAnimationNames.IDLE);
        }
        
        private void SetWaitAnimation(bool isEnabled)
        {
            if(isEnabled)
                _animationControl.SetAnimation(0, RabbitAnimationNames.WAIT_CATCH);
        }
        
    }
}