using _Game.Scripts.Common;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems.LocationModels;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Rooms
{
    public class StartHouseView : AbstractLocationView
    {
        [field: SerializeField] public DoorView[] Doors { get; private set; }
        [field: SerializeField] public NightstandView NightstandView { get; private set; }
        [field: SerializeField] public NightstandView Table { get; private set; }
        
        [SerializeField] private LightBlicker _mainLightBlicker;

        private LampModel _lampModel;
        
        public void Construct(LampModel mainLampModel)
        {
            _lampModel = mainLampModel;
            
            _lampModel.LampLightValue.Subscribe(SetLightForMainBlicker);
        }

        private void SetLightForMainBlicker(float value)
        {
            _mainLightBlicker.SetLightIntensity(value);
        }
        
        private void OnDestroy()
        {
            _lampModel.LampLightValue.Unsubscribe(SetLightForMainBlicker);
        }
    }
}