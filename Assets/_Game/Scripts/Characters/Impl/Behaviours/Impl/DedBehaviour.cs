using _Game.Scripts.DialogueSystem;
using _Game.Scripts.InventorySystem;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl
{
    public class DedBehaviour : ACustomBehaviour, IItemNeeder
    {
        public DedBehaviour(EventBus eventBus) : base(eventBus)
        {
            
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact()
        {
            _eventBus.TriggerEvenet<StartDialogueSignal, string>("ded_d1");
        }

        public void InteractWithItem(ItemId item)
        {
            if (item == ItemId.Apple)
            {
                _eventBus.TriggerEvenet<StartDialogueSignal, string>("ded_d2");
                //start dialogue
            }
        }
    }
}