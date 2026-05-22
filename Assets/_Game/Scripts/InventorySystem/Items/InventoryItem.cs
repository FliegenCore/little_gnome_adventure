using System;
using _Game.Scripts.InteractionSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItem : AbstractInteractable
    {
        public readonly InventoryItemModel InventoryItemModel;
        public readonly InventoryItemView InventoryItemView;
        public readonly ItemId ItemId;
        
        public InventoryItem(
            InventoryItemModel inventoryItemModel, 
            EventBus eventBus, 
            InventoryItemView inventoryItemView,
            ItemId itemId) : 
            base(inventoryItemModel, inventoryItemView, eventBus)
        {
            InventoryItemModel = inventoryItemModel;
            ItemId = itemId;
            InventoryItemView = inventoryItemView;
        }

        public override void Interact(Action callback)
        {
            Debug.Log(AbstractInteractableModel.Id);
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Dispose()
        {
            if (AbstractInteractableModel.ContactTriggerProvider != null)
            {
                AbstractInteractableModel.ContactTriggerProvider.OnEnter -= OnPlayerCollided;
                AbstractInteractableModel.ContactTriggerProvider.OnExit -= OnPlayerExit;
            }
            
            if(InteractableView != null)
                UnityEngine.Object.Destroy(InteractableView.gameObject);
            
            AbstractInteractableModel.IsSelected.Value = false;
            AbstractInteractableModel.CanSelected.Value = false;
        }
    }
}