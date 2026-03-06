using System;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItem : AbstractInteractable
    {
        private readonly InventoryProxy _inventoryProxy;
        
        public BaseItem(AbstractInteractableModel abstractInteractableModel, 
            NightstandView view, 
            EventBus eventBus,
            InventoryProxy inventory) : base(abstractInteractableModel, view, eventBus)
        {
            _inventoryProxy = inventory;
        }

        public override void Interact()
        {
            _inventoryProxy.AddItem(Enum.Parse<ItemId>(AbstractInteractableModel.Id));
            Dispose();
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}