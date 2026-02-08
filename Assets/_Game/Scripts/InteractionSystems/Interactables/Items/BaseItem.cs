using System;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItem : AbstractInteractable
    {
        public readonly NightstandView View;
        private readonly Inventory _inventory;
        
        public BaseItem(AbstractInteractableModel abstractInteractableModel, 
            NightstandView view, 
            EventBus eventBus,
            Inventory inventory) : base(abstractInteractableModel, view, eventBus)
        {
            View = view;
            _inventory = inventory;
        }

        public override void Interact()
        {
            _inventory.AddItem(Enum.Parse<ItemId>(AbstractInteractableModel.Id));
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}