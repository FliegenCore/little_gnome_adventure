using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.RoomSystems.LocationsStates;

namespace _Game.Scripts.RoomSystems
{
    public class LocationsControllerFactory
    {
        private LocationsController _locationsController;
        private readonly CameraController _cameraController;

        private LocationsControllerFactory(CameraController cameraController)
        {
            _cameraController = cameraController;
        }
        
        public LocationsController Create()
        {
            //current = from playerData
            Type locationsControllerType = typeof(StartHouseState);
            LocationsModel locationsModel = new LocationsModel(locationsControllerType);
            
            LocationsController locationsController = new LocationsController(locationsModel, _cameraController);
            _locationsController = locationsController;
            return locationsController;
        }

        public LocationsController Get()
        {
            return _locationsController;
        }
    }
}