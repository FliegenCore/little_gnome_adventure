using _Game.Scripts.InteractionSystems.Interactables.Items.Hints;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItemView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}