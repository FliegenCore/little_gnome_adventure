using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.RoomSystems
{
    public class ForestRootViewFactory
    {
        private const string PREFAB_PATH = "Prefabs/Locations/LocationsRoot";
        private LocationsRootView _locationsRootView;
        private IObjectResolver _container;
        
        public ForestRootViewFactory(IObjectResolver container)
        {
            _container = container;
        }
        
        public LocationsRootView CreateForestLocationsRootView()
        {
            LocationsRootView prefab = Resources.Load<LocationsRootView>(PREFAB_PATH);
            _locationsRootView = _container.Instantiate(prefab);
            return _locationsRootView;
        }

        public LocationsRootView GetLocationsRootView()
        {
            return _locationsRootView;
        }
    }
}