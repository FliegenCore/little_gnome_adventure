using _Game.Scripts.Input;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.OverridedStates;
using _Game.Scripts.Utils;
using Game.PlayerSystem;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    public class PlayerModel
    {
        public readonly Transformation Transformation;
        public readonly IMoveDirectionInput MoveDirectionInput;
        public readonly AnimationPlayerModel AnimationPlayerModel;
        public readonly ReactiveProperty<bool> CanInteract;
        public readonly ReactiveProperty<bool> IsActive;
        public readonly MotionOverridedStates MotionOverridedStates;
        public ReactiveCommand OnPosition = new ReactiveCommand();
        public readonly float MoveSpeed;
        public readonly float RunSpeed;
        public Transform AutoMoveTransform;
        public Transform LastInteractableObjectTransform;
        public AbstractInteractable LastInteractable;
        
        public PlayerModel(
            Transformation transformation,
            IMoveDirectionInput moveDirectionInput,
            AnimationPlayerModel animationPlayerModel,
            float moveSpeed,
            float runSpeed
            )
        {
            CanInteract           = new ReactiveProperty<bool>(true);
            IsActive              = new ReactiveProperty<bool>(true);
            MotionOverridedStates = new MotionOverridedStates();
            MoveDirectionInput    = moveDirectionInput;
            AnimationPlayerModel  = animationPlayerModel;
            Transformation        = transformation;
            MoveSpeed             = moveSpeed;
            RunSpeed              = runSpeed;
        }
    }
}