using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Factory.Impl
{
    public class InteractableFactory : IInteractableFactory
    {
        private readonly EventBus _eventBus;

        private InteractableFactory(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public Interactable CreateInteractable(
            ACustomBehaviour customBehaviour,
            NightstandView nightstandView,
            AbstractInteractableModel abstractInteractableModel)
        {
            if(nightstandView != null)
                nightstandView.HintSelect.Construct(_eventBus, abstractInteractableModel.IsSelected);
            
            Interactable interactable = new Interactable(abstractInteractableModel, nightstandView, _eventBus, customBehaviour);

            return interactable;
        }
    }
}