using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.Rooms;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class LocationAbstractState : FsmAbstractState
    {
        public readonly AbstractLocationView AbstractLocationView;
        
        public LocationAbstractState(Fsm fsm, AbstractLocationView abstractLocation) : base(fsm)
        {
            AbstractLocationView = abstractLocation;
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