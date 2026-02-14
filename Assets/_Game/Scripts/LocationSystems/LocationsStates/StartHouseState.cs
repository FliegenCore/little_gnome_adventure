using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class StartHouseState : LocationAbstractState
    {
        public readonly StartHouseLocationModel StartHouseLocationModel;
        public readonly StartHouseView StartHouseView;
        
        public StartHouseState(Fsm fsm, StartHouseLocationModel startHouseLocationModel, StartHouseView startHouseView) : base(fsm, startHouseView)
        {
            StartHouseLocationModel = startHouseLocationModel;
            StartHouseView = startHouseView;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            
            StartHouseLocationModel.Update(deltaTime);
        }
    }
}