using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationControl : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private float _defaultMixDuration = 0.25f;
        
        private Spine.AnimationState _animationState;
        private Coroutine _durationCoroutine;
        
        public void SetLoopAnimation(int layer, string animationName, bool isLoop = true)
        {
            if (_animationState == null)
            {
                _animationState = _skeletonAnimation.AnimationState;
            }
            
            _animationState.SetAnimation(layer, animationName, isLoop);
        }

        public void SetDurationAnimationWithExit(int layer, string animationName, string exitAnimationName, float duration)
        {
            if (_animationState == null)
            {
                _animationState = _skeletonAnimation.AnimationState;
            }

            if (_durationCoroutine != null)
            {
                StopCoroutine(_durationCoroutine);
            }

            _durationCoroutine = StartCoroutine(PlayTimedAnimation(layer, animationName, exitAnimationName, duration));
        }
        
        private IEnumerator PlayTimedAnimation(int layer, string animationName, string exitAnimationName, float duration)
        {
            var track = _animationState.SetAnimation(layer, animationName, false);
            
            track.MixDuration = _defaultMixDuration;
            
            yield return new WaitForSeconds(duration);
            
            if (!string.IsNullOrEmpty(exitAnimationName))
            {
                var exitTrack = _animationState.SetAnimation(layer, exitAnimationName, false);
                exitTrack.MixDuration = _defaultMixDuration;
            }
        }
        
        public void SetAnimationWithMix(int layer, string animationName, bool isLoop = true, float transitionDuration = 0f)
        {
            if (_animationState == null)
            {
                _animationState = _skeletonAnimation.AnimationState;
            }
    
            if (transitionDuration > 0f)
            {
                var currentTrack = _animationState.GetCurrent(layer);
                if (currentTrack != null)
                {
                    _animationState.AddAnimation(layer, animationName, isLoop, 0f).MixDuration = transitionDuration;
                }
                else
                {
                    _animationState.SetAnimation(layer, animationName, isLoop);
                }
            }
        }
    }
}