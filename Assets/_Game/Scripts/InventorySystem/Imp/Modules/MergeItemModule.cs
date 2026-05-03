using System;
using _Game.Scripts.InventorySystem.Configs;

namespace _Game.Scripts.InventorySystem.Modules
{
    public class MergeItemModule
    {
        private readonly MergeItemHolder _firstItem;
        private readonly MergeItemHolder _secondItem;
        
        private readonly MergeItemConfig _mergeItemConfig;
        private readonly Inventory _inventory;

        public bool IsEnable;
        
        public MergeItemModule(MergeItemConfig mergeItemConfig, Inventory inventory)
        {
            _firstItem       = new MergeItemHolder();
            _secondItem      = new MergeItemHolder();
            _inventory       = inventory;
            _mergeItemConfig = mergeItemConfig;
        }
        
        public void SetupItemForMerge(InventoryItem itemId, Action<ItemId> onMerge)
        {
            if (_firstItem.HoldItem)
            {
                _secondItem.SetItem(itemId);

                if (_firstItem.HoldItem && _secondItem.HoldItem)
                {
                    if (_mergeItemConfig.HasMerge(_firstItem.InventoryItem.ItemId, _secondItem.InventoryItem.ItemId, out ItemId resultItem))
                    {
                        _inventory.RemoveItem(_firstItem.InventoryItem);
                        _inventory.RemoveItem(_secondItem.InventoryItem);
                        
                        onMerge?.Invoke(resultItem);
                    }
                    
                    Clear(); 
                }
            }
            else
            {
                _firstItem.SetItem(itemId);
            }
        }

        public void Clear()
        {
            IsEnable = false;
            _firstItem.Clear();
            _secondItem.Clear();
        }
    }
}