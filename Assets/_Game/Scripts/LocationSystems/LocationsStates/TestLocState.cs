using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.PlayerSystems;
using Core.Common;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class TestLocState : LocationAbstractState
    {
        public readonly TestRoom TestRoom;


        public TestLocState(
            Fsm fsm,
            TestRoom abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm, abstractLocation, dialogueManager, eventBus)
        {
            TestRoom = abstractLocation;
        }
    }
}