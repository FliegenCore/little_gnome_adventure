using System;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.Quests.PlantsQuest.Impl.Plant
{
    public class Plant : AbstractInteractable
    {
        public Plant(
            PlantModel abstractInteractableModel,
            NightstandView interactableView,
            EventBus eventBus, 
            PlantBehaviour customBehaviour) : base(abstractInteractableModel, interactableView, eventBus, customBehaviour)
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