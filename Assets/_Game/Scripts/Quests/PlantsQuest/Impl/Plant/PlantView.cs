using System;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.PlantsQuest.Impl.Plant
{
    public class PlantView : NightstandView
    {
        private const string HEIGHT_ANIMATION_NAME = "pose";
        
        [SerializeField] private AnimationControl _animationControl;

        private ReactiveProperty<int> _currentHeight;
        private ReactiveProperty<bool> _canInteract;
        private ReactiveProperty<bool> _needCallback;
        
        public void Construct(
            ReactiveProperty<int> currentHeight,
            ReactiveProperty<bool> canInteract, 
            ReactiveProperty<bool> needCallback)
        {
            _needCallback = needCallback;
            _currentHeight = currentHeight;
            _canInteract = canInteract;
            _currentHeight.Subscribe(PlayAnimation).AddTo(gameObject);
        }

        private void PlayAnimation(int height)
        {
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
                _animationControl.SetAnimation(0, animName, callback: () =>
                {
                    if(_needCallback.Value)
                        _canInteract.Value = true;
                });
            }
        }
    }
}