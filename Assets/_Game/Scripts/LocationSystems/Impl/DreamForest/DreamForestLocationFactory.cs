using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
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
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationFactory : ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInteractableFactory _interactableFactory;
        private readonly CameraController _cameraController;
        private readonly ICutsceneManger _cutsceneManger;
        private readonly InventoryProxy _inventoryProxy;
        private readonly ItemFactory _itemFactory;
        
        private LocationAbstractState _lastCreated;
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public DreamForestLocationFactory(
            ForestRootViewFactory forestRootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            IInteractableFactory interactableFactory,
            CameraController cameraController,
            ICutsceneManger cutsceneManger,
            InventoryProxy inventoryProxy,
            ItemFactory itemFactory
            )
        {
            _itemFactory           = itemFactory;
            _inventoryProxy        = inventoryProxy;
            _cameraController      = cameraController;
            _interactableFactory   = interactableFactory;
            _playerFactory         = playerFactory;
            _dialogueManager       = dialogueManager;
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus              = eventBus;
            _cutsceneManger        = cutsceneManger;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            DreamForestLocationModel forestLocationModel = new DreamForestLocationModel(typeof(DreamForestLocationState));

            DreamForestLocationState dreamForestLocation =
                new DreamForestLocationState(fsm,
                    forestLocationModel,
                    _forestRootViewFactory.GetLocationsRootView().DreamForestLocationView,
                    _dialogueManager,
                    _eventBus);
            fsm.AddState(dreamForestLocation);
            
            MushroomQuestManager mushroomQuestManager =
                new MushroomQuestManager(
                    _interactableFactory,
                    _eventBus,
                    _inventoryProxy, 
                    _cutsceneManger,
                    dreamForestLocation,
                    _itemFactory,
                    _playerFactory,
                    _cameraController);

            mushroomQuestManager.Initialize();
            
            _lastCreated = dreamForestLocation;
            return dreamForestLocation;
        }
    }
}