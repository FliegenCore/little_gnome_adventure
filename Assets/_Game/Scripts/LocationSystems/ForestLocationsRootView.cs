using _Game.Scripts.LocationSystems.LocationsView;
using _Game.Scripts.RoomSystems.Impl.DreamQuestFirst;
using _Game.Scripts.RoomSystems.Impl.DreamRoom1;
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
        [field: SerializeField] public DreamLocationView DreamLocationView { get; private set; }
        [field: SerializeField] public DreamQuestFirstLocationView DreamQuestFirstLocationView { get; private set; }
    }
}