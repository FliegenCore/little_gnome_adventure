using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InventorySystem.Factories
{
    public class BaseInventoryItemFactory : IInventoryItemFactory
    {
        private readonly IItemInfoProvider _itemInfoProvider;
        private readonly InventoryView _inventoryView;
        private readonly EventBus _eventBus;
        
        public BaseInventoryItemFactory(IItemInfoProvider itemInfoProvider, EventBus eventBus, InventoryView inventoryView)
        {
            _inventoryView = inventoryView; 
            _eventBus = eventBus;
            _itemInfoProvider = itemInfoProvider;
        }
        
        public InventoryItem CreateItem(ItemId id, int index)
        {
            ItemConfig itemConfig = _itemInfoProvider.GetItemConfig(id);
            
            SpriteStorage spriteStorage = new SpriteStorage(itemConfig.Sprite);
            InventoryItemModel inventoryItemModel = new InventoryItemModel(null, new Vector2(), nameof(id), spriteStorage);
            
            InventoryItemView inventoryItemViewPrefab = Object.Instantiate(itemConfig.ViewPrefab, _inventoryView.Cells[index].transform);
            inventoryItemViewPrefab.SpriteApplyer.Construct(spriteStorage);
            
            InventoryItem inventoryItem = new InventoryItem(inventoryItemModel, _eventBus, inventoryItemViewPrefab);

            return inventoryItem;
        }
    }
}