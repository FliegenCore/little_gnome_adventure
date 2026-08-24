using System.Collections.Generic;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.Utils;
using UniRx;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectModel
    {
        public readonly IReadOnlyList<AbstractInteractable> Interactables;
        public readonly ReactiveProperty<bool> IsOpen;
        public readonly bool CanClose;
        
        public InspectModel(bool canClose = true, params AbstractInteractable[] interactables)
        {
            Interactables = new List<AbstractInteractable>(interactables);
            IsOpen = new ReactiveProperty<bool>(false);
            CanClose = canClose;
        }
    }
}