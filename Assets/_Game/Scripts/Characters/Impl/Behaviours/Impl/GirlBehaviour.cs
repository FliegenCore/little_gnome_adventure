using _Game.Scripts.InventorySystem;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl
{
    public class GirlBehaviour : ACustomBehaviour, IItemNeeder
    {
        public GirlBehaviour(EventBus eventBus) : base(eventBus)
        {
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact()
        {
            
        }

        public void InteractWithItem(ItemId item)
        {
            
        }
    }
}