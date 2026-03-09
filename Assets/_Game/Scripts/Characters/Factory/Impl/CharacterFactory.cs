using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Factory.Impl
{
    public class CharacterFactory : ICharacterFactory
    {
        private readonly EventBus _eventBus;

        private CharacterFactory(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public Character CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            CharacterModel characterModel = new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id);
            nightstandView.HintSelect.Construct(_eventBus, characterModel.IsSelected);
            Character character = new Character(characterModel, nightstandView, _eventBus, customBehaviour);

            return character;
        }
    }
}