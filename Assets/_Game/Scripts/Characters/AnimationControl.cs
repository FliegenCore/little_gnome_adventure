using System;
using System.Collections;
using Spine;
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
        
        public void SetAnimation(int layer, string animationName, bool isLoop = true, Action callback = null)
        {
            if (_animationState == null)
            {
                _skeletonAnimation.Initialize(true);
                _animationState = _skeletonAnimation.AnimationState;
            }
            TrackEntry trackEntry = _animationState.SetAnimation(layer, animationName, isLoop);

            if (callback != null)
            {
                void OnComplete(TrackEntry entry)
                {
                    callback?.Invoke();
                    trackEntry.Complete -= OnComplete; 
                }
        
                trackEntry.Complete += OnComplete;
            }
        }
    }
}