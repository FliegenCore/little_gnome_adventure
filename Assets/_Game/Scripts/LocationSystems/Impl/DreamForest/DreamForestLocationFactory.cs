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
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.MushroomQuest;
using _Game.Scripts.Quests.StartGameQuest;
using _Game.Scripts.RoomSystems.LocationsStates;
using _Game.Scripts.Sound;
using _Game.Scripts.UpdateSystems;
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
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly CameraControllerHelper _cameraControllerHelper;
        private readonly DialogueModel _dialogueModel;
        private readonly UpdateController _updateController;
        
        private LocationAbstractState _lastCreated;
        
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        private DreamForestLocationFactory(
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            IInteractableFactory interactableFactory,
            CameraController cameraController,
            ICutsceneManager cutsceneManager,
            InventoryProxy inventoryProxy,
            ItemFactory itemFactory,
            ISoundManager soundManager,
            InspectRegistratorService inspectRegistratorService ,
            CameraControllerHelper cameraControllerHelper,
            DialogueModel dialogueModel,
            UpdateController updateController
            )
        {
            _dialogueModel             = dialogueModel;
            _cameraControllerHelper    = cameraControllerHelper;
            _inspectRegistratorService = inspectRegistratorService;
            _soundManager              = soundManager;
            _itemFactory               = itemFactory;
            _inventoryProxy            = inventoryProxy;
            _cameraController          = cameraController;
            _interactableFactory       = interactableFactory;
            _playerFactory             = playerFactory;
            _dialogueManager           = dialogueManager;
            _rootViewFactory           = rootViewFactory;
            _eventBus                  = eventBus;
            _cutsceneManager           = cutsceneManager;
            _updateController          = updateController;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            DreamForestLocationModel forestLocationModel = new DreamForestLocationModel(typeof(DreamForestLocationState));
            
            StartQuestManager startQuestManager = new StartQuestManager
            (
                _cutsceneManager,
                _rootViewFactory,
                _eventBus,
                _playerFactory,
                _dialogueModel,
                _updateController
            );
            
            DreamForestLocationState dreamForestLocation =
                new DreamForestLocationState(fsm,
                    forestLocationModel,
                    _rootViewFactory.GetLocationsRootView().DreamForestLocationView,
                    _dialogueManager,
                    _eventBus,
                    startQuestManager
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
                    _soundManager,
                    _inspectRegistratorService,
                    _rootViewFactory.GetLocationsRootView().InspectsView.BusJumpInteractableAnimation,
                    _cameraControllerHelper
                    );

            mushroomQuestManager.Initialize();
            
            _lastCreated = dreamForestLocation;
            return dreamForestLocation;
        }
    }
}