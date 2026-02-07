using System;
using System.Collections.Generic;
using _Game.Scripts.RoomSystems.LocationsStates;

namespace _Game.Scripts.RoomSystems
{
    public static class StaticLocationsConnection
    {
        public static Dictionary<string, LocationsIdEnum> LocationsIdEnumMap =
            new Dictionary<string, LocationsIdEnum>()
            {
                [nameof(DoorsIdEnum.MainHouseExit)] = LocationsIdEnum.MainHouse,
                [nameof(DoorsIdEnum.ForestMainHouseEnter)] = LocationsIdEnum.Forest,
            };
        
        public static Dictionary<LocationsIdEnum, Type> LocationsTypeMap =
            new Dictionary<LocationsIdEnum, Type>()
            {
                [LocationsIdEnum.MainHouse] = typeof(StartHouseState),
                [LocationsIdEnum.Forest] = typeof(ForestState),
            };
    }
}