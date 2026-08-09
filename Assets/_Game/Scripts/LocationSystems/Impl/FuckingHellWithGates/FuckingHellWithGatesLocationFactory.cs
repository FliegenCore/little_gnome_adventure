using System;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.Quests.ClanDoorQuest;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.FuckingHellWithGates
{
    public class FuckingHellWithGatesLocationFactory : ILocationFactory, IDisposable
    {
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly InputSystem_Actions _inputSystem;
        private readonly IInteractableFactory _interactableFactory;
        private readonly ItemFactory _itemFactory;
        
        private LocationAbstractState _lastCreated;

        private ClanHellGatesQuestManager _clanHellGatesQuestManager;
        

        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public FuckingHellWithGatesLocationFactory(
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            InspectRegistratorService  inspectRegistratorService,
            InputSystem_Actions inputSystemActions,
            IInteractableFactory interactableFactory,
            ItemFactory itemFactory
            )
        {
            _itemFactory               = itemFactory;
            _interactableFactory       = interactableFactory;
            _inputSystem               = inputSystemActions;
            _inspectRegistratorService = inspectRegistratorService;
            _playerFactory             = playerFactory;
            _dialogueManager           = dialogueManager;
            _rootViewFactory           = rootViewFactory;
            _eventBus                  = eventBus;
        }

        public LocationAbstractState Create(Fsm fsm)
        {
            FuckingHellWithGatesLocationModel fuckingHellWithGatesLocationModel
                = new FuckingHellWithGatesLocationModel(typeof(FuckingHellWithGatesLocationState));

            FuckingHellWithGatesLocationState testState =
                new FuckingHellWithGatesLocationState(fsm,
                    fuckingHellWithGatesLocationModel,
                    _rootViewFactory.GetLocationsRootView().FuckingHellWithGatesLocationView,
                    _dialogueManager,
                    _eventBus);

            fsm.AddState(testState);
            _lastCreated = testState;
            
            CreateQuest();
                
            return testState;
        }

        private void CreateQuest()
        {
            _clanHellGatesQuestManager = new ClanHellGatesQuestManager(
                _eventBus,
                _inspectRegistratorService,
                _rootViewFactory,
                _inputSystem,
                _interactableFactory,
                _itemFactory
                );
            
            _clanHellGatesQuestManager.Initialize();
        }

        public void Dispose()
        {
            _clanHellGatesQuestManager?.Dispose();
        }
    }
}