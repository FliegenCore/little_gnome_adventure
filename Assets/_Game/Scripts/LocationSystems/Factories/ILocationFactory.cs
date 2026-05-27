using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.Impl.DreamForest;
using _Game.Scripts.RoomSystems.LocationsStates;

namespace _Game.Scripts.RoomSystems
{
    public interface ILocationFactory
    {
        LocationAbstractState Create(Fsm fsm);
        LocationAbstractState GetLastCreated();
    }
}