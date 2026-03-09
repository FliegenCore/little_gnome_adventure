using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class StartHouseState : LocationAbstractState
    {
        public readonly StartHouseLocationModel StartHouseLocationModel;
        public readonly StartHouseView StartHouseView;


        public StartHouseState(
            Fsm fsm, 
            StartHouseView abstractLocation,
            IDialogueManager dialogueManager, 
            StartHouseLocationModel startHouseLocationModel) : base(fsm, abstractLocation, dialogueManager)
        {
            StartHouseLocationModel = startHouseLocationModel;
            StartHouseView = abstractLocation;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            StartHouseLocationModel.Update(deltaTime);
        }
    }
}