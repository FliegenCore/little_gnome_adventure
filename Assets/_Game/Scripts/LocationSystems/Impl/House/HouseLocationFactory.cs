using System.Diagnostics.CodeAnalysis;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Variants
{
    public class HouseLocationFactory : ILocationFactory
    {
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IInteractableFactory _interactableFactory;
        private readonly IPlayerFactory _playerFactory;
        
        private LocationAbstractState _lastCreated;
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public HouseLocationFactory(
            RootViewFactory rootViewFactory, 
            EventBus eventBus, 
            IDialogueManager dialogueManager,
            IInteractableFactory interactableFactory,
            IPlayerFactory playerFactory)
        {
            _playerFactory         = playerFactory;
            _interactableFactory      = interactableFactory;
            _dialogueManager       = dialogueManager;
            _rootViewFactory = rootViewFactory;
            _eventBus              = eventBus;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            LampModel lampModel = new LampModel(0.6f, 1.6f);
            
            var nightstand = CreateInteractable("Nightstand", _rootViewFactory.GetLocationsRootView().StartHouseView.NightstandView);
            CreateInteractable("Table", _rootViewFactory.GetLocationsRootView().StartHouseView.Table);
            
            StartHouseLocationModel startHouseLocationModel = new StartHouseLocationModel(typeof(StartHouseState),
                lampModel, nightstand);
            
            StartHouseState startHouseState = 
                new StartHouseState(fsm,
                    startHouseLocationModel,
                    _rootViewFactory.GetLocationsRootView().StartHouseView,
                    _dialogueManager,
                    _eventBus);
            
            _rootViewFactory.GetLocationsRootView().StartHouseView.Construct(lampModel);

            CreateCharacter(nameof(ECharacters.Ded), new DedBehaviour(_eventBus),
                _rootViewFactory.GetLocationsRootView().StartHouseView.DedView);
            
            fsm.AddState(startHouseState);
            _lastCreated = startHouseState;
            
            return startHouseState;
        }

        private Nightstand CreateInteractable(string id, NightstandView view)
        {
            NightstandView nightstandView = view;
            NightstandModel nightstandModel = new NightstandModel(nightstandView.transform.position,id, nightstandView.ContactTriggerProvider);
            nightstandView.HintSelect.Construct(_eventBus, nightstandModel.IsSelected);
            Nightstand nightstand = new Nightstand(_eventBus, nightstandModel, nightstandView);
            
            return nightstand;
        }

        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView,
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
    }
}