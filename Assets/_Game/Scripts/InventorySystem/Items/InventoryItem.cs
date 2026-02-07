using _Game.Scripts.InteractionSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItem : AbstractInteractable
    {
        public readonly InventoryItemView InventoryItemView;
        
        public InventoryItem(InventoryItemModel inventoryItemModel, 
            EventBus eventBus, 
            InventoryItemView inventoryItemView) : 
            base(inventoryItemModel, inventoryItemView, eventBus)
        {
            InventoryItemView = inventoryItemView;
        }

        public override void Interact()
        {
            Debug.Log(AbstractInteractableModel.Id);
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}