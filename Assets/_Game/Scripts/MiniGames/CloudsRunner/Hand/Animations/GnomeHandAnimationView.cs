using _Game.Scripts.PlayerSystems.Animations;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.Animations
{
    [RequireComponent(typeof(AnimationControl))]
    public class GnomeHandAnimationView : MonoBehaviour
    {
        [SerializeField] private AnimationControl _animationControl;
        
        private GnomeHandAnimationModel _animationModel;
        
        public void Construct(GnomeHandAnimationModel animationModel)
        {
            _animationModel = animationModel;
        }

        private void SetRun(bool value)
        {
            if (!value)
                return;
            
            _animationControl.SetAnimation(0, "");
        }

        private void SetIdle(bool value)
        {
            if (!value)
                return;
        }

        private void SetJumpUp(bool value)
        {
            if (!value)
                return;
        }

        private void SetJumpDown(bool value)
        {
            if (!value)
                return;
        }
    }
}