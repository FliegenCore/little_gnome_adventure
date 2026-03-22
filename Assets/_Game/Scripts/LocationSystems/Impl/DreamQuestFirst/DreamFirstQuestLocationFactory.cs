using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.Impl.DreamRoom1;
using _Game.Scripts.RoomSystems.LocationsStates;
using _Game.Scripts.RoomSystems.Variants;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamQuestFirst
{
    public class DreamFirstQuestLocationFactory : ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        private readonly IDialogueManager _dialogueManager;
        private readonly IPlayerFactory _playerFactory;
        private readonly ICharacterFactory _characterFactory;

        public DreamFirstQuestLocationFactory(
            ForestRootViewFactory forestRootViewFactory,
            EventBus eventBus,
            IDialogueManager dialogueManager,
            IPlayerFactory playerFactory,
            ICharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
            _playerFactory = playerFactory;
            _dialogueManager = dialogueManager;
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus = eventBus;
        }

        public LocationAbstractState Create(Fsm fsm)
        {
            DreamQuestFirstLocationModel forestLocationModel = new DreamQuestFirstLocationModel(typeof(DreamQuestFirstLocationState));

            DreamQuestFirstLocationState testState =
                new DreamQuestFirstLocationState(fsm,
                    forestLocationModel,
                    _forestRootViewFactory.GetLocationsRootView().DreamQuestFirstLocationView,
                    _dialogueManager,
                    _eventBus);

            fsm.AddState(testState);
            CreateCharacter(nameof(ECharacters.Granny), new GrannyBehaviour(_eventBus),
                _forestRootViewFactory.GetLocationsRootView().DreamQuestFirstLocationView.GrannyView);

            PlantsQuestManager plantsQuestManager = new PlantsQuestManager(_eventBus, _forestRootViewFactory.GetLocationsRootView());
            plantsQuestManager.Initialize();
                
            return testState;
        }
        
        private Character CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _characterFactory.CreateCharacter(id, customBehaviour, nightstandView);
        }
    }
}