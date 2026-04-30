using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.LobotomyQuest.Impl;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationModels.Forest;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Variants
{
    public class ForestLocationFactory : ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInteractableFactory _interactableFactory;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly InspectRegistratorService _inspectRegistratorService;
        
        public ForestLocationFactory(
            ForestRootViewFactory forestRootViewFactory,
            EventBus eventBus, 
            IDialogueManager dialogueManager, 
            IPlayerFactory playerFactory, 
            IInteractableFactory interactableFactory,
            InputSystem_Actions inputSystemActions,
            InspectRegistratorService inspectRegistratorService)
        {
            _inspectRegistratorService = inspectRegistratorService;
            _inputSystemActions        = inputSystemActions;
            _interactableFactory       = interactableFactory;
            _playerFactory             = playerFactory;
            _dialogueManager           = dialogueManager;
            _forestRootViewFactory     = forestRootViewFactory;
            _eventBus                  = eventBus;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            ForestLocationModel forestLocationModel = new ForestLocationModel(typeof(ForestState));
            
            ForestState forestState = new ForestState(fsm, 
                forestLocationModel,
                _forestRootViewFactory.GetLocationsRootView().ForestLocationView, 
                _dialogueManager,
                _eventBus);
            
            CreateCharacter(nameof(ECharacters.Girl), new GirlBehaviour(_eventBus), 
                  _forestRootViewFactory.GetLocationsRootView().ForestLocationView.GirlView);
            
            LobotomyManager lobotomyManager = new LobotomyManager(
                _forestRootViewFactory.GetLocationsRootView(), 
                _interactableFactory,
                _inputSystemActions,
                _eventBus,
                _inspectRegistratorService);
            
            lobotomyManager.Initialize();
            
            fsm.AddState(forestState);
            return forestState;
        }
        
        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView,
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
    }
}