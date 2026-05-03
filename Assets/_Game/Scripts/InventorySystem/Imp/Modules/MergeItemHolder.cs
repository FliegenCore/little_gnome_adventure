namespace _Game.Scripts.InventorySystem.Modules
{
    public class MergeItemHolder
    {
        private InventoryItem _inventoryItem;
        private bool _holdItem;
        
        public InventoryItem InventoryItem => _inventoryItem;
        public bool HoldItem => _holdItem;
        
        public void SetItem(InventoryItem inventoryItem)
        {
            _holdItem = true;
            _inventoryItem = inventoryItem;
            _inventoryItem.InventoryItemView.Uppercase();
        }

        public void Clear()
        {
            if(_inventoryItem != null)
                _inventoryItem.InventoryItemView.Lowercase();

            _inventoryItem = null;
            _holdItem = false;
        }
    }
}