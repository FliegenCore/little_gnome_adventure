using System;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.Characters.Girl
{
    public class GirlView : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        
        private Spine.AnimationState _animationState;

        private void Awake()
        {
            _animationState = _skeletonAnimation.AnimationState;

            _animationState.SetAnimation(0, GirlAnimationNames.BODY_IDLE_ANIMATION, true);
            _animationState.SetAnimation(1, GirlAnimationNames.EMOTION_CRYING_ANIMATION, true);
        }
    }
}