using _Game.Scripts.Utils;

namespace _Game.Scripts.PlayerSystems.Animations
{
    public class AnimationPlayerModel
    {
        public readonly Observable<bool> IsMove;

        public AnimationPlayerModel()
        {
            IsMove = new Observable<bool>();
        }
    }
}