using Core.Common;

namespace _Game.Scripts.InventorySystem.Factories
{
    public class InventoryFactoryProvider
    {
        private readonly IItemInfoProvider _itemInfoProvider;
        private readonly EventBus _eventBus;
        private readonly InventoryView _inventoryView;
        
        private InventoryFactoryProvider(IItemInfoProvider itemInfoProvider, EventBus eventBus, InventoryView inventoryView)
        {
            _inventoryView = inventoryView;
            _itemInfoProvider = itemInfoProvider;
            _eventBus = eventBus;
        }
        
        public IInventoryItemFactory GetItemFactory(ItemId id)
        {
            return new BaseInventoryItemFactory(_itemInfoProvider, _eventBus, _inventoryView);
        }
    }
}