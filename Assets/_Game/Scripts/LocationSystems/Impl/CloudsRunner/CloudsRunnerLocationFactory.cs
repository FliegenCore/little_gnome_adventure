using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.MiniGames.CloudsRunner;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems.LocationModels.Test;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.CloudsRunner
{
    public class CloudsRunnerLocationFactory : ILocationFactory
    {
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly CloudsRunnerInitializer _cloudsRunnerInitializer;
        
        private LocationAbstractState _lastCreated;
        public LocationAbstractState GetLastCreated()
        {
            return _lastCreated;
        }
        
        public CloudsRunnerLocationFactory(
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            CloudsRunnerInitializer cloudsRunnerInitializer
            )
        {
            _cloudsRunnerInitializer = cloudsRunnerInitializer;
            _playerFactory           = playerFactory;
            _dialogueManager         = dialogueManager;
            _rootViewFactory         = rootViewFactory;
            _eventBus                = eventBus;
        }
        
        public LocationAbstractState Create(Fsm fsm)
        {
            CloudsRunnerLocationModel forestLocationModel = new CloudsRunnerLocationModel(typeof(CloudsRunnerLocationState));
            
            CloudsRunnerLocationState testState = 
                new CloudsRunnerLocationState(fsm,
                    forestLocationModel,
                    _rootViewFactory.GetLocationsRootView().RunnerLocationView,
                    _dialogueManager,
                    _eventBus,
                    _cloudsRunnerInitializer);
            
            fsm.AddState(testState);
            _lastCreated = testState;
            
            return testState;
        }

    }
}