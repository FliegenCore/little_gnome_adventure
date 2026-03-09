using _Game.Scripts.CameraSystem;

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
            LocationsModel locationsModel = new LocationsModel(LocationsIdEnum.MainHouse);
            
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