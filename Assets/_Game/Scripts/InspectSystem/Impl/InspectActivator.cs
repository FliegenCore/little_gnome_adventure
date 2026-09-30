using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Game.Scripts.PlayerSystems.Animations;
using UniRx;
using UnityEngine;

namespace Assets._Game.Scripts.InspectSystem.Impl
{
    public class InspectActivator : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;

        private ReactiveProperty<bool> _isActive;

        public void Construct(ReactiveProperty<bool> isActive)
        {
            _isActive = isActive;

            _isActive.Subscribe(EnableShowAnimationAnimation).AddTo(gameObject);
        }

        public void EnableShowAnimationAnimation(bool isShow)
        {
            if(isShow)
            {
                _animationControl.SetAnimation(0, "open", false, SetIdleAnimation);
            }
            else
            {
                _animationControl.SetAnimation(0, "close", false, SetIdleAnimation);
            }
        }

        private void SetIdleAnimation()
        {
            _animationControl.SetAnimation(0, "idle", false);
        }
    }
}
