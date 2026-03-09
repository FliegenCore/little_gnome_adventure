using _Game.Scripts.Utils;
using UniRx;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayerModel
    {
        public readonly ReactiveProperty<bool> IsMove;
        public readonly ReactiveProperty<bool> InInventory;
        public readonly ReactiveProperty<bool> InRage; //todo проверка

        public AnimationPlayerModel()
        {
            InInventory = new ReactiveProperty<bool>();
            IsMove      = new ReactiveProperty<bool>();
            InRage       = new  ReactiveProperty<bool>();
        }
    }
}