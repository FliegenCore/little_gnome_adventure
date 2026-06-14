using System;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Spine.Unity;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.PlantsQuest.Impl.Plant
{
    public class PlantView : NightstandView
    {
        private const string HEIGHT_ANIMATION_NAME = "pose";
        
        [SerializeField] private AnimationControl _animationControl;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SkeletonAnimation _pointsAnimation;

        private ReactiveProperty<int> _currentHeight;
        private ReactiveProperty<bool> _canInteract;
        private ReactiveProperty<bool> _needCallback;
        private ReactiveProperty<bool> _enableCollider;
        
        private bool _isInitialized;
        
        public void Construct(
            ReactiveProperty<int> currentHeight,
            ReactiveProperty<bool> canInteract, 
            ReactiveProperty<bool> needCallback,
            ReactiveProperty<bool> enableCollider)
        {
            _needCallback = needCallback;
            _currentHeight = currentHeight;
            _canInteract = canInteract;
            _enableCollider = enableCollider;
            
            _currentHeight.Subscribe(PlayAnimation).AddTo(gameObject);
            enableCollider.Subscribe(SetEnableCollider).AddTo(gameObject);
            
            DisablePoints();
        }

        public void EnablePoints()
        {
            _pointsAnimation.gameObject.SetActive(true);
        }

        public void DisablePoints()
        {
            _pointsAnimation.gameObject.SetActive(false);
        }
        
        private void PlayAnimation(int height)
        {
            if (!_isInitialized)
            {
                _pointsAnimation.Initialize(false);
                _isInitialized = true;
            }

            if(height < 5)
                _pointsAnimation.AnimationState.SetAnimation(0, height.ToString(), false);
            
            string animName = "";
            if (height == 5)
            {
                animName = "rise_flower";
            }
            else
                animName = $"{HEIGHT_ANIMATION_NAME}{height}";


            if (animName == "rise_flower")
            {
                _animationControl.SetAnimation(0, animName, isLoop: false, callback: () =>
                {
                    if(_needCallback.Value)
                        _canInteract.Value = true;
                    
                    _animationControl.SetAnimation(0, "pose5flower");
                });
                
            }
            else
            {
                _animationControl.SetAnimation(0, animName);

                Observable.Timer(TimeSpan.FromSeconds(0.25f)).Subscribe(x => 
                {
                    if(_needCallback.Value)
                        _canInteract.Value = true;
                });
            }
        }

        private void SetEnableCollider(bool enabled)
        {
            _collider2D.enabled = enabled;
        }
    }
}