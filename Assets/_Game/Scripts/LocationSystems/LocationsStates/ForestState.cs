using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.PlayerSystems;
using Core.Common;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class ForestState : LocationAbstractState
    {
        public readonly ForestLocationView ForestLocationView;


        public ForestState(
            Fsm fsm,
            ForestLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm, abstractLocation, dialogueManager, eventBus)
        {
            ForestLocationView = abstractLocation;
        }
    }
}