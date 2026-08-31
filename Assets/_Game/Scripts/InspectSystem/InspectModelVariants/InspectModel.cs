using System.Collections.Generic;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.Utils;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectModel
    {
        public readonly IReadOnlyList<AbstractInteractable> Interactables;
        public readonly ReactiveProperty<bool> IsOpen;
        public readonly bool CanClose;
        public readonly Transform OpenTranform;
        
        public InspectModel(Transform openTransform, bool canClose = true, params AbstractInteractable[] interactables)
        {
            OpenTranform = openTransform;
            Interactables = new List<AbstractInteractable>(interactables);
            IsOpen = new ReactiveProperty<bool>(false);
            CanClose = canClose;
        }
    }
}