using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;
using VContainer;

namespace _Game.Scripts.RoomSystems.Impl.DreamQuestFirst
{
    public class DreamFirstQuestLocationFactory : ILocationFactory
    {
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInteractableFactory _interactableFactory;
        private readonly CameraController _cameraController;
        private readonly ICutsceneManger _cutsceneManger;
        
        private LocationAbstractState _lastCreated;
        
        public DreamFirstQuestLocationFactory(
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            IInteractableFactory interactableFactory,
            CameraController cameraController,
            ICutsceneManger cutsceneManger)
        {
            _cameraController      = cameraController;
            _interactableFactory      = interactableFactory;
            _playerFactory         = playerFactory;
            _dialogueManager       = dialogueManager;
            _rootViewFactory = rootViewFactory;
            _eventBus              = eventBus;
            _cutsceneManger        = cutsceneManger;
        }
        
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            DreamQuestFirstLocationModel forestLocationModel = new DreamQuestFirstLocationModel(typeof(DreamQuestFirstLocationState));

            DreamQuestFirstLocationState testState =
                new DreamQuestFirstLocationState(fsm,
                    forestLocationModel,
                    _rootViewFactory.GetLocationsRootView().DreamQuestFirstLocationView,
                    _dialogueManager,
                    _eventBus);

            fsm.AddState(testState);
            CreateCharacter(nameof(ECharacters.Granny), new GrannyBehaviour(_eventBus),
                _rootViewFactory.GetLocationsRootView().DreamQuestFirstLocationView.GrannyView);

            
            PlantsQuestManager plantsQuestManager = new PlantsQuestManager(
                _eventBus,
                _rootViewFactory.GetLocationsRootView(), 
                _cameraController,
                _playerFactory,
                _cutsceneManger,
                _interactableFactory
                );
            
            plantsQuestManager.Initialize();
            _lastCreated = testState;
            return testState;
        }

        

        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView, 
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
    }
}