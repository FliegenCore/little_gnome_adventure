using System;
using System.Collections;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationControl : MonoBehaviour
    {
        public event Action<TrackEntry, Spine.Event> OnAnimationEventInvoke;
        
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private float _defaultMixDuration = 0.25f;
        [SerializeField] private string _startAnimationName;
        
        private Spine.AnimationState _animationState;
        private Coroutine _durationCoroutine;

        private void Awake()
        {
            if (_animationState == null)
            {
                _skeletonAnimation.Initialize(true);
                _animationState = _skeletonAnimation.AnimationState;
            }

            if (_startAnimationName != "")
            {
                SetAnimation(0, _startAnimationName);
            }
            
            _animationState.Event += HandleEvent;
        }

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

        public void ResetAnimation(int layer)
        {
            _animationState.ClearTrack(layer);
        }
        
        public bool HasAnimation(string animationName)
        {
            if (_skeletonAnimation == null || _skeletonAnimation.Skeleton == null)
                return false;

            var skeletonData = _skeletonAnimation.Skeleton.Data;
    
            return skeletonData.FindAnimation(animationName) != null;
        }

        public void SubscribeOnEvents(Action<TrackEntry, Spine.Event> action)
        {
            OnAnimationEventInvoke += action;
        }
        
        public void UnsubscribeOnEvents(Action<TrackEntry, Spine.Event> action)
        {
            OnAnimationEventInvoke -= action;
        }
        
        private void HandleEvent(TrackEntry trackEntry, Spine.Event e) 
        {
            OnAnimationEventInvoke?.Invoke(trackEntry, e);
        }

        private void OnDestroy()
        {
            _animationState.Event -= HandleEvent;
        }
    }
}