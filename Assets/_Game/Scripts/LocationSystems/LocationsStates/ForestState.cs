using _Game.Scripts.FSM;
using _Game.Scripts.LocationSystems.LocationsView;

namespace _Game.Scripts.RoomSystems.LocationsStates
{
    public class ForestState : LocationAbstractState
    {
        public readonly ForestLocationView ForestLocationView;
        
        public ForestState(Fsm fsm, ForestLocationView forestLocationView) : base(fsm, forestLocationView)
        {
            ForestLocationView = forestLocationView;
        }
    }
}