using _Game.Scripts.Characters.Rabbit.Animations;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit
{
    public class RabbitModel
    {
        public readonly Transformation Transformation;
        public readonly RabbitAnimationModel AnimationModel;
        
        public Transform AutoMovePoint;
        public readonly float Speed;

        public RabbitModel(Transformation transformation, RabbitAnimationModel animationModel)
        {
            Speed = 6.5f;
            AnimationModel = animationModel;
            Transformation = transformation;
        }
    }
}