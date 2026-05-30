using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.Impl.DreamForest;
using Core.Common;

namespace _Game.Scripts.Quests.MushroomQuest
{
    public class MushroomQuestManager
    {
        private readonly EventBus _eventBus;
        private readonly IInteractableFactory _interactableFactory;
        private readonly InventoryProxy _inventoryProxy;
        private readonly ICutsceneManger _cutsceneManger;
        private readonly DreamForestLocationState _dreamForestLocationState;
        private readonly ItemFactory _itemFactory;
        
        public MushroomQuestManager(
            IInteractableFactory interactableFactory,
            EventBus eventBus,
            InventoryProxy inventoryProxy,
            ICutsceneManger cutsceneManger,
            DreamForestLocationState dreamForestLocationState,
            ItemFactory itemFactory
            )
        {
            _itemFactory              = itemFactory;
            _dreamForestLocationState = dreamForestLocationState;
            _eventBus                 = eventBus;
            _inventoryProxy           = inventoryProxy;
            _interactableFactory      = interactableFactory;            
            _cutsceneManger           = cutsceneManger;
        }

        public void Initialize()
        {
            McMushroomBehaviour mcMushroomBehaviour = new McMushroomBehaviour(_eventBus, _inventoryProxy);

            CreateCharacter(nameof(ECharacters.McMushroom),
                mcMushroomBehaviour,
                _dreamForestLocationState.DreamForestLocationView.McMushroomView);


            foreach (var mushroom in _dreamForestLocationState.DreamForestLocationView.Mushrooms)
            {
                _itemFactory.CreateItem(mushroom, ItemId.Mushroom);
            }
        }
        
        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView, 
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
    }
}