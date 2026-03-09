using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;

namespace _Game.Scripts.PlayerSystems.Animations.Factory
{
    public interface ICharacterFactory
    {
        Character CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView);
    }
}