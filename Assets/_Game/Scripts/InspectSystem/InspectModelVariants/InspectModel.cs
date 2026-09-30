using System;
using System.Collections.Generic;
using _Game.Scripts.InteractionSystems;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem
{
    public class InspectModel
    {
        public readonly IReadOnlyList<AbstractInteractable> Interactables;
        public readonly ReactiveProperty<bool> IsOpen;
        public readonly ReactiveProperty<bool> IsPrepareShow;
        public readonly Transform OpenTranform;
        public readonly bool CanClose;
        private readonly bool _momentalEndActive;

        private Action _setOpenCallback;

        
        public InspectModel(Transform openTransform, bool momentalEndActive, bool canClose = true, params AbstractInteractable[] interactables)
        {
            IsPrepareShow = new ReactiveProperty<bool>();
            OpenTranform = openTransform;
            Interactables = new List<AbstractInteractable>(interactables);
            IsOpen = new ReactiveProperty<bool>(false);
            CanClose = canClose;

            _momentalEndActive = momentalEndActive;
        }

        public void SetIsOpen(bool isOpen, Action onEnded)
        {
            if(_momentalEndActive)
            {
                IsOpen.Value = isOpen;
                onEnded?.Invoke();
            }
            else
            {
                _setOpenCallback = onEnded;
            }
        }
    }
}