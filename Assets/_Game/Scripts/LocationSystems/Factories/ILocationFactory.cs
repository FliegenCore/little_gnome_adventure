using _Game.Scripts.FSM;

namespace _Game.Scripts.RoomSystems
{
    public interface ILocationFactory
    {
        void Create(Fsm fsm);
    }
}