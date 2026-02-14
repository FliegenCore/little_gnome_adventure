using _Game.Scripts.Utils;
using UniRx;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayerModel
    {
        public readonly ReactiveProperty<bool> IsMove;

        public AnimationPlayerModel()
        {
            IsMove = new ReactiveProperty<bool>();
        }
    }
}