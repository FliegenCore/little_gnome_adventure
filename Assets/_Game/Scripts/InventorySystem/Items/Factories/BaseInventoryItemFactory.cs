using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InventorySystem.Factories
{
    public class BaseInventoryItemFactory : IInventoryItemFactory
    {
        private readonly IItemInfoProvider _itemInfoProvider;
        private readonly EventBus _eventBus;
        
        public BaseInventoryItemFactory(IItemInfoProvider itemInfoProvider, EventBus eventBus)
        {
            _eventBus = eventBus;
            _itemInfoProvider = itemInfoProvider;
        }
        
        public InventoryItem CreateItem(ItemId id)
        {
            ItemConfig itemConfig = _itemInfoProvider.GetItemConfig(id);
            
            SpriteStorage spriteStorage = new SpriteStorage(itemConfig.Sprite);
            InventoryItemModel inventoryItemModel = new InventoryItemModel(null, new Vector2(), nameof(id), spriteStorage);
            
            InventoryItemView inventoryItemViewPrefab = Object.Instantiate(itemConfig.ViewPrefab);
            inventoryItemViewPrefab.SpriteApplyer.Construct(spriteStorage);
            
            InventoryItem inventoryItem = new InventoryItem(inventoryItemModel, _eventBus, inventoryItemViewPrefab);

            return inventoryItem;
        }
    }
}