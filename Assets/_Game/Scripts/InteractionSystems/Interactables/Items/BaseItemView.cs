using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    [RequireComponent(typeof(AnimationControl))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class BaseItemView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}