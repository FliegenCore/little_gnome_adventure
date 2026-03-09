using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems.Rooms;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class ForestLocationsRootView : MonoBehaviour
    {
        [field: SerializeField] public InspectsView InspectsView { get; private set; }
        [field: SerializeField] public StartHouseView StartHouseView { get; private set; }
        [field: SerializeField] public ForestLocationView ForestLocationView { get; private set; }
        [field: SerializeField] public TestRoom TestRoom { get; private set; }
    }
}