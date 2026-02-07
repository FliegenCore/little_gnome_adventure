using System.Collections.Generic;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.Utils;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectModel
    {
        public readonly IReadOnlyList<AbstractInteractable> Interactables;
        public readonly Observable<bool> IsOpen;
        
        public InspectModel(params AbstractInteractable[] interactables)
        {
            Interactables = new List<AbstractInteractable>(interactables);
            IsOpen = new Observable<bool>(false);   
        }
    }
}