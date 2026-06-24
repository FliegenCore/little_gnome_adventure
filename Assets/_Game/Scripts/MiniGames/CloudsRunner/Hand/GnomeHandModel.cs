using _Game.Scripts.Input;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.Animations;
using Game.PlayerSystem;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHandModel
    {
        public readonly Transformation Transformation;
        public readonly GnomeHandAnimationModel AnimationModel;
        public readonly IMoveDirectionInput MoveDirectionInput;
        public readonly ReactiveProperty<float> LastJumpTime = new ReactiveProperty<float>();
        public readonly ReactiveProperty<float> PhysicPower;
        public readonly AnimationCurve JumpAnimationCurve;
        public readonly float MoveSpeed;
        
        
        public GnomeHandModel(Transformation transformation,
            IMoveDirectionInput moveDirectionInput,
            AnimationCurve jumpAnimationCurve, 
            GnomeHandAnimationModel animationModel,
            float moveSpeed
            )
        {
            AnimationModel     = animationModel;
            JumpAnimationCurve = jumpAnimationCurve;
            MoveSpeed          = moveSpeed;
            MoveDirectionInput = moveDirectionInput;
            Transformation     = transformation;
            PhysicPower        = new ReactiveProperty<float>( 1);
        }
    }
}