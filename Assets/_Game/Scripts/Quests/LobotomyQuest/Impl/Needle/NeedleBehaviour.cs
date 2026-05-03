using System;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl.Needle
{
    public class NeedleBehaviour : ACustomBehaviour, IDisposable
    {
        private const int MINIMAL_DEPTH = 0;
        private const int MAXIMUM_DEPTH = 10;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly NeedleModel _needleModel;
        private readonly AnimationControl _animationControl;

        private int _index;
        private int _lastYDirection;
        
        public NeedleBehaviour(EventBus eventBus, NeedleModel needleModel, AnimationControl animationControl, int index) : base(eventBus)
        {
            _index            = index;
            _animationControl = animationControl;
            _needleModel      = needleModel;
            _needleModel.IsSelected.Subscribe(IsSelected).AddTo(_disposables);
        }

        public void Initialize()
        {
            int fakeIndex = _index + 1;

            string animationName = $"gv{fakeIndex}/gv{fakeIndex}wait";
            string animationSecondVariant = $"gv{fakeIndex}/gv{fakeIndex}pwait";

            if (_animationControl.HasAnimation(animationName))
            {
                _animationControl.SetAnimation(_index, animationName);
            }
            else
            {
                _animationControl.SetAnimation(_index, animationSecondVariant);
            }
        }

        private void IsSelected(bool isSelected)
        {
            if (!isSelected)
                return;
            
            int fakeIndex = _index + 1;
            _animationControl.SetAnimation(5, $"selected/{fakeIndex}");
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            if (_lastYDirection > 0)
            {
                if(_needleModel.Depth.Value + 1 > MAXIMUM_DEPTH)
                    return;
                
                _needleModel.Depth.Value++;
            }
            if (_lastYDirection < 0)
            {
                if(_needleModel.Depth.Value - 1 < MINIMAL_DEPTH)
                    return;
                
                _needleModel.Depth.Value--;
            }
            
            int fakeIndex = _index + 1;
            _animationControl.SetAnimation(_index, $"gv{fakeIndex}/gv{fakeIndex}p{_needleModel.Depth.Value}");
        }

        public void SetDirection(int yDirection)
        {
            _lastYDirection = yDirection;   
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}