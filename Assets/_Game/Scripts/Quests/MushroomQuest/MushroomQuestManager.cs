using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using _Game.Scripts.Quests.MushroomQuest.Cutscenes;
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
        private readonly IPlayerFactory _playerFactory;
        private readonly CameraController _cameraController;
        
        public MushroomQuestManager(
            IInteractableFactory interactableFactory,
            EventBus eventBus,
            InventoryProxy inventoryProxy,
            ICutsceneManger cutsceneManger,
            DreamForestLocationState dreamForestLocationState,
            ItemFactory itemFactory,
            IPlayerFactory playerFactory,
            CameraController cameraController
            )
        {
            _cameraController         = cameraController;
            _playerFactory            = playerFactory;
            _itemFactory              = itemFactory;
            _dreamForestLocationState = dreamForestLocationState;
            _eventBus                 = eventBus;
            _inventoryProxy           = inventoryProxy;
            _interactableFactory      = interactableFactory;            
            _cutsceneManger           = cutsceneManger;
        }

        public void Initialize()
        {
            CreateMcMushroom();
            CreateMushrooms();
            CreateBusman();
        }

        private void CreateMcMushroom()
        {
            McMushroomBehaviour mcMushroomBehaviour = new McMushroomBehaviour(_eventBus, _inventoryProxy);

            CreateCharacter(nameof(ECharacters.McMushroom),
                mcMushroomBehaviour,
                _dreamForestLocationState.DreamForestLocationView.McMushroomView);
        }

        private void CreateMushrooms()
        {
            foreach (var mushroom in _dreamForestLocationState.DreamForestLocationView.Mushrooms)
            {
                _itemFactory.CreateItem(mushroom, ItemId.Mushroom);
            }
        }

        private void CreateBusman()
        {
            BusmanView busmanView = _dreamForestLocationState.DreamForestLocationView.BusmanView;
            
            BusmanInitializer busmanInitializer = new BusmanInitializer();
            
            busmanInitializer.Initialize(busmanView.AnimationControl);

            GnomeEnterInBusCutscene gnomeEnterInBusCutscene = 
                new GnomeEnterInBusCutscene(_eventBus, _playerFactory, busmanView.CameraFollowPoint, busmanInitializer.Fsm, _cameraController);
            
            CreateCharacter(
                nameof(ECharacters.Busman), 
                new BusmanBehaviour(_eventBus, busmanInitializer.Fsm, _cutsceneManger, gnomeEnterInBusCutscene),
                busmanView);
        }
        
        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView, 
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
    }
}