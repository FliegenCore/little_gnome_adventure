using Core.Common;

namespace _Game.Scripts.InventorySystem.Factories
{
    public class InventoryFactoryProvider
    {
        private readonly IItemInfoProvider _itemInfoProvider;
        private readonly EventBus _eventBus;
        
        private InventoryFactoryProvider(IItemInfoProvider itemInfoProvider, EventBus eventBus)
        {
            _itemInfoProvider = itemInfoProvider;
            _eventBus = eventBus;
        }
        
        public IInventoryItemFactory GetItemFactory(ItemId id)
        {
            return new BaseInventoryItemFactory(_itemInfoProvider, _eventBus);
        }
    }
}