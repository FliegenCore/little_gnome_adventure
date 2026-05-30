using System;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl
{
    public class GirlBehaviour : ACustomBehaviour, IItemNeeder
    {
        public GirlBehaviour(EventBus eventBus) : base(eventBus)
        {
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            _eventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>("Lobotomy");
        }

        public void InteractWithItem(InventoryItem item, Action callback)
        {
            callback?.Invoke();
            if (item.ItemId != ItemId.Needle)
            {
                return;
            }
            
            _eventBus.TriggerEvenet<RemoveItemSignal, InventoryItem>(item);
        }
    }
}