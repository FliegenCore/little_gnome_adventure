using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.Quests.StartGameQuest.Rabbit;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.UpdateSystems;
using Core.Common;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class StartQuestManager
    {
        private readonly RabbitFactory _rabbitFactory;
        private readonly ICutsceneManager _cutsceneManager;
        private readonly RootViewFactory _rootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IPlayerFactory _playerFactory;
        private readonly DialogueModel _dialogueModel;
        private readonly UpdateController _updateController;
        
        public StartQuestManager(
            ICutsceneManager cutsceneManager,
            RootViewFactory rootViewFactory,
            EventBus eventBus,
            IPlayerFactory playerFactory,
            DialogueModel dialogueModel,
            UpdateController updateController
        )
        {
            _updateController = updateController;
            _dialogueModel    = dialogueModel;
            _playerFactory    = playerFactory;
            _eventBus         = eventBus;
            _rootViewFactory  = rootViewFactory;
            _cutsceneManager  = cutsceneManager;
            
            _rabbitFactory = new RabbitFactory(updateController);
        }

        public void Initialize()
        {
            StartCutscene startCutscene = new StartCutscene(
                _eventBus,
                _playerFactory,
                _rootViewFactory.GetLocationsRootView().DreamForestLocationView.StartMovePoint,
                _rootViewFactory.GetLocationsRootView().DreamForestLocationView.RabbitMovePoint,
                _dialogueModel,
                _rabbitFactory
            );
            
            RabbitView rabbitView = _rootViewFactory.GetLocationsRootView().DreamForestLocationView.RabbitView;
            
            Rabbit.Rabbit rabbit = _rabbitFactory.CreateRabbit(rabbitView);
            
            rabbit.StateMachine.SetState<RabbitWaitCatchState>();
            _cutsceneManager.Play(startCutscene);
        }
    }
}