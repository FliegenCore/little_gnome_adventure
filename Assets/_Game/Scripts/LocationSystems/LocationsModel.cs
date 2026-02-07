using _Game.Scripts.Utils;

namespace _Game.Scripts.RoomSystems
{
    public class LocationsModel
    {
        public readonly Observable<LocationsIdEnum> CurrentLocation;

        public LocationsModel(LocationsIdEnum currentLocation)
        {
            CurrentLocation = new Observable<LocationsIdEnum>(currentLocation);
        }
    }
}