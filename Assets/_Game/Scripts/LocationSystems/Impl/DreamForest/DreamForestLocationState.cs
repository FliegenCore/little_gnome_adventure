using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.DreamForest
{
    public class DreamForestLocationState :  LocationAbstractState
    {
        public DreamForestLocationState(
            Fsm fsm, AbstractLocationModel locationModel,
            AbstractLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus
            ) : base(fsm, locationModel, abstractLocation, dialogueManager, eventBus)
        {
        }
    }
}