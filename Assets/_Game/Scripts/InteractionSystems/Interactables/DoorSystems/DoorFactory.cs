using _Game.Scripts.PlayerSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class DoorFactory
    {
        private readonly DoorsService _doorsService;
        private readonly IPlayerFactory _playerFactory;
        private readonly EventBus _eventBus;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        
        public DoorFactory(DoorsService doorsService, IPlayerFactory playerFactory,
            EventBus eventBus, 
            LocationsControllerFactory locationsControllerFactory)
        {
            _locationsControllerFactory = locationsControllerFactory;
            _playerFactory = playerFactory;
            _doorsService = doorsService;
            _eventBus = eventBus;
        }
        
        public void Create(string id, string connectionId, DoorView view)
        {
            Debug.Log($"Creating door {id} with connetction id {connectionId}" );
            DoorModel doorModel = new DoorModel(view.ContactTriggerProvider, id, view.SpawnTransform.transform.position, connectionId);
            
            Door door = new Door(
                doorModel, 
                _eventBus, 
                view, 
                _doorsService, 
                _locationsControllerFactory.Get().LocationsModel,
                _playerFactory.GetPlayer(),
                _locationsControllerFactory.Get());
            
            view.HintSelect.Construct(_eventBus, doorModel.IsSelected);
            
            _doorsService.RegisterDoor(id, door);
        }
    }
}