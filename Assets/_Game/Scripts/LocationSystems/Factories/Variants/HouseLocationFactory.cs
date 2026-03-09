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
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly ICharacterFactory _characterFactory;
        private readonly IPlayerFactory _playerFactory;
        
        public HouseLocationFactory(
            ForestRootViewFactory forestRootViewFactory, 
            EventBus eventBus, 
            IDialogueManager dialogueManager,
            ICharacterFactory characterFactory,
            IPlayerFactory playerFactory)
        {
            _playerFactory         = playerFactory;
            _characterFactory      = characterFactory;
            _dialogueManager       = dialogueManager;
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus              = eventBus;
        }
        
        public void Create(Fsm fsm)
        {
            LampModel lampModel = new LampModel(0.6f, 1.6f);
            
            var nightstand = CreateInteractable("Nightstand", _forestRootViewFactory.GetLocationsRootView().StartHouseView.NightstandView);
            CreateInteractable("Table", _forestRootViewFactory.GetLocationsRootView().StartHouseView.Table);
            
            StartHouseLocationModel startHouseLocationModel = new StartHouseLocationModel(LocationsIdEnum.Forest, lampModel, nightstand);
            
            StartHouseState startHouseState = 
                new StartHouseState(fsm,
                    _forestRootViewFactory.GetLocationsRootView().StartHouseView,
                    _dialogueManager,
                    startHouseLocationModel);
            
            _forestRootViewFactory.GetLocationsRootView().StartHouseView.Construct(lampModel);

            CreateCharacter(nameof(ECharacters.Ded), new DedBehaviour(_eventBus),
                _forestRootViewFactory.GetLocationsRootView().StartHouseView.DedView);
            
            fsm.AddState(startHouseState);
        }

        private Nightstand CreateInteractable(string id, NightstandView view)
        {
            NightstandView nightstandView = view;
            NightstandModel nightstandModel = new NightstandModel(nightstandView.transform.position,id, nightstandView.ContactTriggerProvider);
            nightstandView.HintSelect.Construct(_eventBus, nightstandModel.IsSelected);
            Nightstand nightstand = new Nightstand(_eventBus, nightstandModel, nightstandView);
            
            return nightstand;
        }

        private Character CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _characterFactory.CreateCharacter(id, customBehaviour, nightstandView);
        }
    }
}