using System;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;

        private AnimationPlayerModel _animationPlayerModel;
        
        public AnimationControl AnimationControl => _animationControl;
        
        public void Construct(AnimationPlayerModel animationPlayerModel)
        {
            _animationPlayerModel = animationPlayerModel;
            
            _animationPlayerModel.IsMove.Subscribe(SetMove).AddTo(gameObject);
            _animationPlayerModel.InInventory.Subscribe(SetInventory).AddTo(gameObject);
            _animationPlayerModel.InRage.Subscribe(SetInRage).AddTo(gameObject);
        }

        private void SetInRage(bool inRage)
        {
            if (inRage)
            {
                _animationControl.SetAnimation(0, PlayerAnimationsName.IN_RAGE_ANIMATION_NAME);
            }
        }

        private void SetInventory(bool inInventory)
        {
            if(inInventory)
                _animationControl.SetAnimation(0, PlayerAnimationsName.IN_INVENTORY_ANIMATION_NAME);
            else
                _animationControl.SetAnimation(0, PlayerAnimationsName.IDLE_ANIMATION_NAME);
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