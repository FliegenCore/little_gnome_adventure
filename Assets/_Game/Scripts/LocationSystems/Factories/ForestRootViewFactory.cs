using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class ForestRootViewFactory
    {
        private const string PREFAB_PATH = "Prefabs/Locations/LocationsRoot";
        
        private ForestLocationsRootView _forestLocationsRootView;
        
        public ForestLocationsRootView CreateForestLocationsRootView()
        {
            ForestLocationsRootView prefab = Resources.Load<ForestLocationsRootView>(PREFAB_PATH);
            _forestLocationsRootView = Object.Instantiate(prefab);
            return _forestLocationsRootView;
        }

        public ForestLocationsRootView GetLocationsRootView()
        {
            return _forestLocationsRootView;
        }
    }
}