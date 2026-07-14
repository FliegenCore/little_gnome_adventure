using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations.Impl
{
    public class CharacterView : NightstandView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}