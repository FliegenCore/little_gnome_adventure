using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.InteractionSystems.Interactables.Toy
{
    public class ToyCircle : AbstractInteractable
    {
        public ToyCircle(
            AbstractInteractableModel abstractInteractableModel, 
            NightstandView interactableView,
            EventBus eventBus) : base(abstractInteractableModel, interactableView, eventBus)
        {
            
        }

        public override void Interact()
        {
            //надеть на игрушку через эвент бас, похуй
        }

        public override bool CanInteract()
        {
            return true;
        }
    }
}