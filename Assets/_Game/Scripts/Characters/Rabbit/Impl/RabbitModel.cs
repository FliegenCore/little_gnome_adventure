using _Game.Scripts.Characters.Rabbit.Animations;
using _Game.Scripts.Utils;
using Game.PlayerSystem;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit
{
    public class RabbitModel
    {
        public readonly Transformation Transformation;
        public readonly RabbitAnimationModel AnimationModel;
        public readonly ReactiveProperty<bool> IsActive = new ReactiveProperty<bool>(true);
        
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