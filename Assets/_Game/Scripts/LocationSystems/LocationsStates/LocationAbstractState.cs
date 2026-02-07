using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class LocationAbstractState : FsmAbstractState
    {
        public LocationAbstractState(Fsm fsm) : base(fsm)
        {
        }

        public override void Enter()
        {
                        
        }

        public override void Exit()
        {
        }

        public override void Update(float deltaTime)
        {
            //_locationView?.SelfUpdate(deltaTime);
        }
    }
}