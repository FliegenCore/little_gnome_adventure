using _Game.Scripts.InteractionSystems.Interactables.Items;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants
{
    public class InspectNightstandView : InspectAbstractView
    {
        [field: SerializeField] public BaseItemView Toy { get; private set; }
        [field: SerializeField] public BaseItemView Apple { get; private set; }
    }
}