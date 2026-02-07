using System.Collections.Generic;
using _Game.Scripts.UpdateSystems;

namespace _Game.Scripts.RoomSystems.LocationModels
{
    public class AbstractLocationModel
    {
        public readonly LocationsIdEnum LocationId;
        
        public AbstractLocationModel(LocationsIdEnum locationId)
        {
            LocationId = locationId;
        }
    }
}