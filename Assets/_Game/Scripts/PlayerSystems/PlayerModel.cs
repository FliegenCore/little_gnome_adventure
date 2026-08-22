using _Game.Scripts.Input;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.Animations.Impl;
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
        public readonly Observable<bool> CanInteract;
        public readonly Observable<bool> IsActive;
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
            CanInteract          = new Observable<bool>(true);
            IsActive             = new Observable<bool>(true);
            MoveDirectionInput   = moveDirectionInput;
            AnimationPlayerModel = animationPlayerModel;
            Transformation       = transformation;
            MoveSpeed            = moveSpeed;
            RunSpeed             = runSpeed;
        }
    }
}