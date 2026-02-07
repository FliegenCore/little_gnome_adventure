using _Game.Scripts.InventorySystem;
using Core.Common;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItem : AbstractInteractable
    {
        private readonly Inventory _inventory;
        
        public BaseItem(AbstractInteractableModel abstractInteractableModel, EventBus eventBus, Inventory inventory) : base(abstractInteractableModel, eventBus)
        {
            _inventory = inventory;
        }

        public override void Interact()
        {
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}