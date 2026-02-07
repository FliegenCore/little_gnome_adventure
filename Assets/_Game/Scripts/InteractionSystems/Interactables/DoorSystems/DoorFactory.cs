using _Game.Scripts.PlayerSystems;
using Core.Common;

namespace _Game.Scripts.RoomSystems
{
    public class DoorFactory
    {
        private readonly DoorsService _doorsService;
        private readonly IPlayerFactory _playerFactory;
        private readonly EventBus _eventBus;
        
        public DoorFactory(DoorsService doorsService, IPlayerFactory playerFactory, EventBus eventBus)
        {
            _playerFactory = playerFactory;
            _doorsService = doorsService;
            _eventBus = eventBus;
        }
        
        public void Create(string id, string connectionId, DoorView view)
        {
            DoorModel doorModel = new DoorModel(view.ContactTriggerProvider, id, view.Position, connectionId);
            
            Door door = new Door(doorModel, _eventBus, view, _doorsService, _playerFactory.GetPlayer());
            
            _doorsService.RegisterDoor(id, door);
        }
    }
}