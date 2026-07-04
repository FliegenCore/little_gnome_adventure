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

        private readonly ICutsceneManager _cutsceneManager;
        private readonly StartCutscene _startCutscene;
        
        public DreamForestLocationState(
            Fsm fsm, 
            AbstractLocationModel locationModel,
            DreamForestLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus,
            StartCutscene startCutscene,
            ICutsceneManager cutsceneManager
            ) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
            DreamForestLocationView = abstractLocation;
            _cutsceneManager = cutsceneManager;
            _startCutscene = startCutscene;
        }

        public override void Enter()
        {
            base.Enter();
            _cutsceneManager.Play(_startCutscene);
        }
    }
}