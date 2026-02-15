using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class TestLocState : LocationAbstractState
    {
        public readonly TestRoom TestRoom;
        
        public TestLocState(Fsm fsm, TestRoom abstractLocation) : base(fsm, abstractLocation)
        {
            TestRoom = abstractLocation;
        }
    }
}