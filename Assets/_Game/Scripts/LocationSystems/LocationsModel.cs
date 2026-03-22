using System;
using System.Collections.Generic;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.Utils;

namespace _Game.Scripts.RoomSystems
{
    public class LocationsModel
    {
        public List<AbstractLocationModel> LocationModels = new List<AbstractLocationModel>();
        public readonly Observable<Type> CurrentLocation;

        public LocationsModel(Type currentLocation)
        {
            CurrentLocation = new Observable<Type>(currentLocation);
        }
    }
}