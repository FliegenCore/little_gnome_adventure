using System;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using Core.Common;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl.Hedgehog
{
    public class HedgehogBehaviour : ACustomBehaviour, IItemNeeder
    {
        private readonly AnimationControl _animationControl;
        
        public HedgehogBehaviour(EventBus eventBus, AnimationControl animationControl) : base(eventBus)
        {
            _animationControl = animationControl;
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            //начать диалог
        }

        public void InteractWithItem(InventoryItem item, Action callback)
        {
            callback?.Invoke();
            
            if (item.ItemId != ItemId.Candy)
            {
                return;
            }
            
            _animationControl.SetAnimation(0, "hand/idle_candy");
            _eventBus.TriggerEvenet<RemoveItemSignal, InventoryItem>(item);
            //начать диалог, потом отдать конфетку
        }
    }
}