using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.Impl.DreamRoom1;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamQuestFirst
{
    public class DreamQuestFirstLocationState: LocationAbstractState
    {
        public readonly DreamQuestFirstLocationView DreamRoomView;

        public DreamQuestFirstLocationState(
            Fsm fsm,
            AbstractLocationModel locationModel,
            DreamQuestFirstLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
            DreamRoomView = abstractLocation;
        }
    }
}