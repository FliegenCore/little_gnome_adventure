using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
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
        
        public ForestLocationFactory(
            ForestRootViewFactory forestRootViewFactory,
            EventBus eventBus, 
            IDialogueManager dialogueManager, 
            IPlayerFactory playerFactory)
        {
            _playerFactory         = playerFactory;
            _dialogueManager       = dialogueManager;
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus              = eventBus;
        }
        
        public void Create(Fsm fsm)
        {
            LampModel lampModel = new LampModel(0.6f, 1.6f);
            
            ForestLocationModel forestLocationModel = new ForestLocationModel(LocationsIdEnum.Forest);
            
            ForestState forestState = new ForestState(fsm, 
                _forestRootViewFactory.GetLocationsRootView().ForestLocationView, 
                _dialogueManager);
            
            _forestRootViewFactory.GetLocationsRootView().StartHouseView.Construct(lampModel);
            
            fsm.AddState(forestState);
        }

        private Nightstand CreateInteractable(string id, NightstandView view)
        {
            NightstandView nightstandView = view;
            NightstandModel nightstandModel = new NightstandModel(nightstandView.transform.position,id, nightstandView.ContactTriggerProvider);
            nightstandView.HintSelect.Construct(_eventBus, nightstandModel.IsSelected);
            Nightstand nightstand = new Nightstand(_eventBus, nightstandModel, nightstandView);
            
            return nightstand;
        }
    }
}