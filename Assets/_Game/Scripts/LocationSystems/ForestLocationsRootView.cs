using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.RoomSystems.Rooms;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class ForestLocationsRootView : MonoBehaviour
    {
        [field: SerializeField] public StartHouseView StartHouseView { get; private set; }
        [field: SerializeField] public InspectsView InspectsView { get; private set; }
    }
}