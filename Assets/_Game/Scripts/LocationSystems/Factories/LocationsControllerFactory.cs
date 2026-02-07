namespace _Game.Scripts.RoomSystems
{
    public class LocationsControllerFactory
    {
        private LocationsController _locationsController;
        
        public LocationsController Create()
        {
            //from playerData
            LocationsModel locationsModel = new LocationsModel(LocationsIdEnum.MainHouse);
            
            LocationsController locationsController = new LocationsController(locationsModel);
            _locationsController = locationsController;
            return locationsController;
        }

        public LocationsController Get()
        {
            return _locationsController;
        }
    }
}