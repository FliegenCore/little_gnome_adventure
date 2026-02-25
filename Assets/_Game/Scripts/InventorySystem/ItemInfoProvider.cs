namespace _Game.Scripts.InventorySystem
{
    public class ItemInfoProvider : IItemInfoProvider
    {
        private readonly ItemConfigs _itemConfigs;
        
        private ItemInfoProvider(ItemConfigs itemConfigs)
        {
            _itemConfigs = itemConfigs;
        }
        
        public ItemConfig GetItemConfig(ItemId itemId)
        {
            foreach (var item in _itemConfigs.Items)
            {
                if(item.ItemId == itemId)
                    return item;
            }
            
            return new ItemConfig();
        }
    }
}