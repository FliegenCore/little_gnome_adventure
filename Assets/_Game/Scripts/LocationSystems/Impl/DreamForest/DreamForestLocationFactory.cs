using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.CutsceneSystem.Impl;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.MushroomQuest;
using _Game.Scripts.Quests.StartGameQuest;
using _Game.Scripts.RoomSystems.LocationsStates;
using _Game.Scripts.Sound;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationFactory : ILocationFactory
    {
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInteractableFactory _interactableFactory;
        private readonly CameraController _cameraController;
        private readonly ICutsceneManager _cutsceneManager;
        private readonly InventoryProxy _inventoryProxy;
        private readonly ItemFactory _itemFactory;
        private readonly ISoundManager _soundManager;
        
        private LocationAbstractState _lastCreated;
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public DreamForestLocationFactory(
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            IInteractableFactory interactableFactory,
            CameraController cameraController,
            ICutsceneManager cutsceneManager,
            InventoryProxy inventoryProxy,
            ItemFactory itemFactory,
            ISoundManager soundManager
            )
        {
            _soundManager          = soundManager;
            _itemFactory           = itemFactory;
            _inventoryProxy        = inventoryProxy;
            _cameraController      = cameraController;
            _interactableFactory   = interactableFactory;
            _playerFactory         = playerFactory;
            _dialogueManager       = dialogueManager;
            _rootViewFactory       = rootViewFactory;
            _eventBus              = eventBus;
            _cutsceneManager       = cutsceneManager;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            DreamForestLocationModel forestLocationModel = new DreamForestLocationModel(typeof(DreamForestLocationState));

            StartCutscene startCutscene = new StartCutscene(
                _eventBus,
                _playerFactory,
                _rootViewFactory.GetLocationsRootView().DreamForestLocationView.StartMovePoint);
                
            DreamForestLocationState dreamForestLocation =
                new DreamForestLocationState(fsm,
                    forestLocationModel,
                    _rootViewFactory.GetLocationsRootView().DreamForestLocationView,
                    _dialogueManager,
                    _eventBus,
                    startCutscene, 
                    _cutsceneManager
                    );
            fsm.AddState(dreamForestLocation);
            
            MushroomQuestManager mushroomQuestManager =
                new MushroomQuestManager(
                    _interactableFactory,
                    _eventBus,
                    _inventoryProxy, 
                    _cutsceneManager,
                    dreamForestLocation,
                    _itemFactory,
                    _playerFactory,
                    _cameraController,
                    _soundManager
                    );

            mushroomQuestManager.Initialize();
            
            _lastCreated = dreamForestLocation;
            return dreamForestLocation;
        }
    }
}