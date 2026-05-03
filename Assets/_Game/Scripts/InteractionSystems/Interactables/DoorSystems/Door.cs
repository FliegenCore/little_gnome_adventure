using System;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class Door : AbstractInteractable
    {
        public readonly DoorModel DoorModel;
        public readonly DoorView DoorView;
        private readonly DoorsService _doorsService;
        private readonly ITeleportable _teleportable;
        private readonly LocationsModel _locationsModel;
        private readonly LocationsController _locationsController;
        
        public Door(DoorModel doorModel,
            EventBus eventBus, 
            DoorView doorView, 
            DoorsService doorsService, 
            LocationsModel locationsModel,
            ITeleportable teleportable,
            LocationsController locationsController) : base(doorModel,doorView, eventBus)
        {
            _locationsController = locationsController;
            _locationsModel      = locationsModel;
            _teleportable        = teleportable;
            _doorsService        =  doorsService;
            DoorModel            = doorModel;
            DoorView             = doorView;
        }
        
        public override void Interact(Action _)
        {
            DoorView connectedDoor = _doorsService.GetViewDoorById(DoorModel.ConnectedDoorId);
            DoorModel modelConnectedDoor = _doorsService.GetModelDoorById(DoorModel.ConnectedDoorId);
            
            if (connectedDoor != null)
            {
                //do fast fade
                _locationsModel.CurrentLocation.Value =
                    _locationsController.GetLocationByView(connectedDoor.ConnectedLocationView).GetType();
                _teleportable.Teleport(modelConnectedDoor.Position);
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