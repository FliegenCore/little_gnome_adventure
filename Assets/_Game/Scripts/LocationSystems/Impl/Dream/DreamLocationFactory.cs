using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems.LocationModels.Test;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamRoom1
{
    public class DreamLocationFactory : ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        
        private LocationAbstractState _lastCreated;
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public DreamLocationFactory(
            ForestRootViewFactory forestRootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
            _dialogueManager = dialogueManager;
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus = eventBus;
        }

        public LocationAbstractState Create(Fsm fsm)
        {
            DreamLocationModel forestLocationModel = new DreamLocationModel(typeof(DreamLocationState));

            DreamLocationState testState =
                new DreamLocationState(fsm,
                    forestLocationModel,
                    _forestRootViewFactory.GetLocationsRootView().DreamLocationView,
                    _dialogueManager,
                    _eventBus);

            fsm.AddState(testState);
            _lastCreated = testState;
            
            return testState;
        }

    }
}