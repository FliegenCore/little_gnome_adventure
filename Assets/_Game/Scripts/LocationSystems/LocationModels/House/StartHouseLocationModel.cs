using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;

namespace _Game.Scripts.RoomSystems.LocationModels
{
    public class StartHouseLocationModel : AbstractLocationModel
    {
        private readonly LampModel _mainLampModel;
        public readonly Nightstand _nightstand;
        
        public StartHouseLocationModel(LocationsIdEnum locationsIdEnum, LampModel mainLampModel, Nightstand nightstand) : base(locationsIdEnum)
        {
            _nightstand = nightstand;
            _mainLampModel = mainLampModel;
        }

        public void Update(float deltaTime)
        {
            _mainLampModel.Update(deltaTime);
        }
    }
}