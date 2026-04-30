using _Game.Scripts.PlayerSystems.Animations;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants
{
    public class LobotomyInspectView : InspectAbstractView
    {
        [field: SerializeField] public AnimationControl AnimationControl { get; private set; }
    }
}