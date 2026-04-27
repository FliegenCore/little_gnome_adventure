using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Impl
{
    public class Interactable : AbstractInteractable
    {
        public Interactable(
            AbstractInteractableModel abstractInteractableModel, 
            NightstandView interactableView, 
            EventBus eventBus,
            ACustomBehaviour customBehaviour) :
            base(abstractInteractableModel, interactableView, eventBus, customBehaviour)
        {

        }

        public override void Interact()
        {
            CustomBehaviour.Interact();
        }

        public override bool CanInteract()
        {
            return CustomBehaviour.CanInteract();
        }
    }
}