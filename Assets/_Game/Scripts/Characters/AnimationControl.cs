using System;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationControl : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;

        public void SetAnimation(int layer, string animationName, bool isLoop = true)
        {
            if (_animationState == null)
            {
                _animationState = _skeletonAnimation.AnimationState;
            }
            
            _animationState.SetAnimation(layer, animationName, isLoop);
        }
    }
}