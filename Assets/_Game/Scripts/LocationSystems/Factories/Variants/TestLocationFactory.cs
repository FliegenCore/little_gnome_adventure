using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.LocationModels.Test;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Variants
{
    public class TestLocationFactory: ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        
        public TestLocationFactory(
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
            TestLocationModel forestLocationModel = new TestLocationModel(LocationsIdEnum.Test1);
            
            TestLocState testState = 
                new TestLocState(fsm,
                    _forestRootViewFactory.GetLocationsRootView().TestRoom,
                    _dialogueManager);
            
            fsm.AddState(testState);
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