using UniRx;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand.Animations
{
    public class GnomeHandAnimationModel
    {
        public readonly ReactiveProperty<bool> IsMoveAnimation = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsIdleAnimation = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsJumpUpAnimation = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsJumpDownAnimation = new ReactiveProperty<bool>();
    }
}