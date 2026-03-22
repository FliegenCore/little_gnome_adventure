using System;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;

namespace _Game.Scripts.RoomSystems.LocationModels
{
    public class StartHouseLocationModel : AbstractLocationModel
    {
        private readonly LampModel _mainLampModel;
        public readonly Nightstand Nightstand;
        
        public StartHouseLocationModel(Type locationsIdEnum, LampModel mainLampModel, Nightstand nightstand) : base(locationsIdEnum)
        {
            Nightstand = nightstand;
            _mainLampModel = mainLampModel;
        }

        public void Update(float deltaTime)
        {
            _mainLampModel.Update(deltaTime);
        }
    }
}