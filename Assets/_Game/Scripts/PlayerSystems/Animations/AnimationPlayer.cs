using System;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;

        private AnimationPlayerModel _animationPlayerModel;
        
        public void Construct(AnimationPlayerModel animationPlayerModel)
        {
            _animationPlayerModel = animationPlayerModel;
            
            _animationPlayerModel.IsMove.Subscribe(SetMove).AddTo(gameObject);
        }

        private void SetMove(bool isMove)
        {
            if(isMove)
                _animationControl.SetAnimation(0, PlayerAnimationsName.MOVE_ANIMATION_NAME);
            else
                _animationControl.SetAnimation(0, PlayerAnimationsName.IDLE_ANIMATION_NAME);
        }
    }
}