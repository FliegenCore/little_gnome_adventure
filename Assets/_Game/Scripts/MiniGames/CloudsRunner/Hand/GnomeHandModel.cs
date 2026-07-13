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
        public readonly ReactiveProperty<float> CurrentJumpVelocity;
        public readonly GroundChecker GroundChecker;
        public readonly float MoveSpeed;
        public readonly float JumpDuration;
        public readonly float JumpHeight;
        public float GravityForce = 9.8f;
        public float StartVelocityY;
        
        public GnomeHandModel(Transformation transformation,
            IMoveDirectionInput moveDirectionInput,
            GnomeHandAnimationModel animationModel,
            GroundChecker groundChecker,
            float moveSpeed,
            float jumpDuration,
            float jumpHeight
            )
        {
            JumpDuration        = jumpDuration;
            JumpHeight          = jumpHeight;
            GroundChecker       = groundChecker;
            AnimationModel      = animationModel;
            MoveSpeed           = moveSpeed;
            MoveDirectionInput  = moveDirectionInput;
            Transformation      = transformation;
            CurrentJumpVelocity = new ReactiveProperty<float>( 0);
        }
    }
}