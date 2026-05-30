using System;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using Core.Common;

namespace _Game.Scripts.Quests.MushroomQuest
{
    public class McMushroomBehaviour : ACustomBehaviour, IItemNeeder
    {
        private readonly InventoryProxy _inventoryProxy;

        private bool _isComplete;
        
        public McMushroomBehaviour(EventBus eventBus, InventoryProxy inventoryProxy) : base(eventBus)
        {
            _inventoryProxy = inventoryProxy;
        }

        public override bool CanInteract()
        {
            if (_isComplete)
                return false;
            
            return true;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            
            int count = _inventoryProxy.CheckCount(ItemId.Mushroom);

            if (count > 0)
            {
                _eventBus.TriggerEvenet<StartDialogueSignal, string>("mushroom3");
            }
            else
            {
                _eventBus.TriggerEvenet<StartDialogueSignal, string>("mushroom1");
            }
        }

        public void InteractWithItem(InventoryItem item, Action callback)
        {
            if (_isComplete)
            {
                callback?.Invoke();
                return;
            }
            
            if (item.ItemId == ItemId.Mushroom)
            {
                int count = _inventoryProxy.CheckCount(ItemId.Mushroom);

                if (count >= 3)
                {
                    _eventBus.TriggerEvenet<RemoveItemAllItemsWithIdSignal, ItemId>(ItemId.Mushroom);
                    _eventBus.TriggerEvenet<DialogueEventWithCallbackSignal, string, Action>("a_McMushroom_idletodrunk2", () =>
                    {
                        _isComplete = true;
                        callback?.Invoke();
                        _eventBus.TriggerEvenet<StartDialogueSignal, string>("mushroom4");
                    });
                }
                else
                {
                    callback?.Invoke();
                    _eventBus.TriggerEvenet<StartDialogueSignal, string>("mushroom2");
                }
            }
            else
            {
                callback?.Invoke();
            }
        }
    }
}