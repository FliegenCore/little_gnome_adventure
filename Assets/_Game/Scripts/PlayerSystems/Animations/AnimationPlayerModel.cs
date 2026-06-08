using _Game.Scripts.Utils;
using UniRx;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayerModel
    {
        public readonly ReactiveProperty<bool> IsMove;
        public readonly ReactiveProperty<bool> IsRun;
        public readonly ReactiveProperty<bool> IsIdle;
        public readonly ReactiveProperty<bool> InInventory;

        public AnimationPlayerModel()
        {
            InInventory = new ReactiveProperty<bool>();
            IsMove      = new ReactiveProperty<bool>();
            IsRun       = new ReactiveProperty<bool>();
            IsIdle      = new ReactiveProperty<bool>();
        }
    }
}