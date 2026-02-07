using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems;
using Core.Common;

namespace _Game.Scripts.RoomSystems
{
    public class Door : AbstractInteractable
    {
        public readonly DoorModel DoorModel;
        public readonly DoorView DoorView;
        private readonly DoorsService _doorsService;
        private readonly ITeleportable _teleportable;
        private readonly LocationsModel _locationsModel;
        
        public Door(DoorModel doorModel, EventBus eventBus,DoorView doorView, DoorsService doorsService, ITeleportable teleportable) : base(doorModel,doorView, eventBus)
        {
            _teleportable = teleportable;
            _doorsService =  doorsService;
            DoorModel = doorModel;
            DoorView = doorView;
        }
        
        public override  void Interact()
        {
            DoorModel connectedDoor = _doorsService.GetModelDoorById(DoorModel.ConnectedDoorId);
            
            if (connectedDoor != null)
            {
                //do fast fade
                _locationsModel.CurrentLocation.Value = StaticLocationsConnection.LocationsIdEnumMap[connectedDoor.Id];
                _teleportable.Teleport(connectedDoor.Position);
            }
        }

        public override bool CanInteract()
        {
            return HasConnected() && DoorModel.IsOpen;
        }
        
        private bool HasConnected()
        {
            DoorModel connectedDoor = _doorsService.GetModelDoorById(DoorModel.ConnectedDoorId);
            
            if(connectedDoor == null)
                return false;

            return true;
        }
    }
}