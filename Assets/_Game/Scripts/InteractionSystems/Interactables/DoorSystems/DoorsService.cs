using System.Collections.Generic;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;

namespace _Game.Scripts.RoomSystems
{
    public class DoorsService
    {
        private Dictionary<string, Door> _doors = new();

        public void RegisterDoor(string id, Door door)
        {
            _doors.Add(id, door);
        }

        public Door GetDoorById(string doorId)
        {
            return _doors[doorId];
        }
        
        public DoorModel GetModelDoorById(string doorId)
        {
            return _doors[doorId].DoorModel;
        }
        
        public NightstandView GetViewDoorById(string doorId)
        {
            return _doors[doorId].DoorView;
        }
    }
}