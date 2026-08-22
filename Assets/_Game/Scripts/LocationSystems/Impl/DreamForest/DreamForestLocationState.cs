using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.CutsceneSystem.Impl;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.Quests.StartGameQuest;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationState : LocationAbstractState
    {
        public readonly DreamForestLocationView DreamForestLocationView;

        
        private readonly StartQuestManager _startQuestManager;
        
        public DreamForestLocationState(
            Fsm fsm, 
            AbstractLocationModel locationModel,
            DreamForestLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus,
            
            StartQuestManager startQuestManager
            ) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
            _startQuestManager = startQuestManager;
            DreamForestLocationView = abstractLocation;
            
        }

        public override void Enter()
        {
            base.Enter();
            _startQuestManager.Initialize();
        }
    }
}