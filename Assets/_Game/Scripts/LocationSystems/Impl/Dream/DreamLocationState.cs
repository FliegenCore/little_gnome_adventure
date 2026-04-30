using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamRoom1
{
    public class DreamLocationState: LocationAbstractState
    {
        public readonly DreamLocationView DreamRoomView;

        public DreamLocationState(
            Fsm fsm,
            DreamLocationModel locationModel,
            DreamLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
            DreamRoomView = abstractLocation;
        }
    }
}