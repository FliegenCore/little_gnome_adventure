using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;

namespace _Game.Scripts.RoomSystems.Impl.FuckingHellWithGates
{
    public class FuckingHellWithGatesLocationState : LocationAbstractState
    {
        public readonly FuckingHellWithGatesLocationView FuckingHellWithGatesLocationView;

        public FuckingHellWithGatesLocationState(
            Fsm fsm,
            AbstractLocationModel model,
            FuckingHellWithGatesLocationView abstractLocation,
            IDialogueManager dialogueManager,
            EventBus eventBus) : base(fsm, model, abstractLocation, dialogueManager, eventBus)
        {
            FuckingHellWithGatesLocationView = abstractLocation;
        }
    }
}