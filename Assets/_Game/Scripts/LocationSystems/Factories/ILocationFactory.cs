using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationsStates;

namespace _Game.Scripts.RoomSystems
{
    public interface ILocationFactory
    {
        LocationAbstractState Create(Fsm fsm);
    }
}