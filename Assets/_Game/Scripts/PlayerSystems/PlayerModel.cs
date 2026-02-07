using _Game.Scripts.Input;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.Utils;
using Game.PlayerSystem;

namespace _Game.Scripts.PlayerSystems
{
    public class PlayerModel
    {
        public readonly Transformation Transformation;
        public readonly IMoveDirectionInput MoveDirectionInput;
        public readonly AnimationPlayerModel  AnimationPlayerModel;
        public readonly Observable<bool> CanInteract;
        public readonly float MoveSpeed;
        
        public PlayerModel(Transformation transformation, IMoveDirectionInput moveDirectionInput, AnimationPlayerModel animationPlayerModel, float moveSpeed)
        {
            CanInteract = new Observable<bool>(true);
            MoveDirectionInput = moveDirectionInput;
            AnimationPlayerModel = animationPlayerModel;
            Transformation = transformation;
            MoveSpeed = moveSpeed;
        }
    }
}