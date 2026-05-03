using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems;
using Core.Common;
using UnityEngine.UIElements;

namespace _Game.Scripts.InteractionSystems.Interactables.Items.Managers
{
    public class ItemFactory
    {
        private readonly EventBus _eventBus;
        private readonly InventoryProxy _inventoryProxy;
        private readonly IPlayerFactory _playerFactory;

        public ItemFactory(EventBus eventBus, InventoryProxy inventoryProxy, IPlayerFactory playerFactory)
        {
            _eventBus = eventBus;
            _inventoryProxy = inventoryProxy;
            _playerFactory = playerFactory;
        }
        
        public BaseItem CreateItem(BaseItemView itemView, ItemId id)
        {
            BaseItemModel baseItemModel =
                new BaseItemModel(itemView.ContactTriggerProvider, itemView.transform.position, id.ToString(), true);
            
            itemView.HintSelect.Construct(_eventBus, baseItemModel.IsSelected);
            BaseItem item = new BaseItem(baseItemModel, itemView, _eventBus, _inventoryProxy, _playerFactory);

            return item;
        }
    }
}