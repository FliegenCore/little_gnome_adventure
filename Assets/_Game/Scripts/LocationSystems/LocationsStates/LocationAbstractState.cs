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
            AbstractLocationView.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            AbstractLocationView.gameObject.SetActive(false);
        }

        public override void Update(float deltaTime)
        {
        }
    }
}