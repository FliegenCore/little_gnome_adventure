using _Game.Scripts.PlayerSystems.Animations;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants
{
    public class InspectAnimationView : InspectEmptyView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}