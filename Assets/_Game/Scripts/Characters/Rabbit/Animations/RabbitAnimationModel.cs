using UniRx;

namespace _Game.Scripts.Characters.Rabbit.Animations
{
    public class RabbitAnimationModel
    {
        public readonly ReactiveProperty<bool> IsSeatAnimation = new  ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsIdleAnimation = new  ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsWalkAnimation = new  ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsWaitCatchAnimation = new  ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> IsJumpAnimation = new  ReactiveProperty<bool>();
        public readonly ReactiveCommand JumpIsEnded = new ReactiveCommand();
    }
}