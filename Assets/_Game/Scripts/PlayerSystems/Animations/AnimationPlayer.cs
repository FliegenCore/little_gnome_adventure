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
            _animationPlayerModel.IsRun.Subscribe(SetRun).AddTo(gameObject);
            _animationPlayerModel.InInventory.Subscribe(SetInventory).AddTo(gameObject);
            _animationPlayerModel.IsIdle.Subscribe(SetIsIdle).AddTo(gameObject);
            _animationPlayerModel.IsIdleSneak.Subscribe(SetIsIdleSneak).AddTo(gameObject);
            _animationPlayerModel.IsMoveSneak.Subscribe(SetIsSneakMove).AddTo(gameObject);
        }

        private void SetIsIdleSneak(bool isIdleSneak)
        {
            if(isIdleSneak)
                _animationControl.SetAnimation(0, PlayerAnimationsName.IDLE_CROUCH_ANIMATION_NAME);
        }

        private void SetIsSneakMove(bool isSneakMove)
        {
            if(isSneakMove)
                _animationControl.SetAnimation(0, PlayerAnimationsName.CROUCH_WALK_ANIMATION_NAME);
        }

        private void SetIsIdle(bool isIdle)
        {
            if(isIdle)
                _animationControl.SetAnimation(0, PlayerAnimationsName.IDLE_ANIMATION_NAME);
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
        }
        
        private void SetRun(bool isRun)
        {
            if(isRun)
                _animationControl.SetAnimation(0, PlayerAnimationsName.RUN_ANIMATION_NAME);
        }
    }
}