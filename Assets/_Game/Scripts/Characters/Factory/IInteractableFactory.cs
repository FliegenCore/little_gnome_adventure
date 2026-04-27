using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;

namespace _Game.Scripts.PlayerSystems.Animations.Factory
{
    public interface IInteractableFactory
    {
        Interactable CreateInteractable( ACustomBehaviour customBehaviour, NightstandView nightstandView, AbstractInteractableModel abstractInteractableModel);
    }
}