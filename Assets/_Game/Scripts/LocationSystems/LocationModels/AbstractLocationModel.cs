using System;
using System.Collections.Generic;
using _Game.Scripts.UpdateSystems;

namespace _Game.Scripts.RoomSystems.LocationModels
{
    public class AbstractLocationModel
    {
        public readonly Type LocationStateType;
        
        public AbstractLocationModel(Type locationStateType)
        {
            LocationStateType = locationStateType;
        }
    }
}