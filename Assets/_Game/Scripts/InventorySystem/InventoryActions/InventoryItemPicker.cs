using _Game.Scripts.InteractionSystems;

namespace _Game.Scripts.InventorySystem.InventoryActions
{
    public class InventoryItemPicker
    {
        private InventoryItemModel _inventoryItemModel;

        private InventoryItemPicker()
        {
            
        }
        
        public void ChooseItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModel = inventoryItemModel;
        }

        private void ApplyOnOther(AbstractInteractable interactable)
        {
            
        }
    }
}