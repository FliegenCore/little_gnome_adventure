using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts.RoomSystems
{
    public class ForestRootViewFactory
    {
        private const string PREFAB_PATH = "Prefabs/Locations/LocationsRoot";
        private ForestLocationsRootView _forestLocationsRootView;
        private IObjectResolver _container;
        
        public ForestRootViewFactory(IObjectResolver container)
        {
            _container = container;
        }
        
        public ForestLocationsRootView CreateForestLocationsRootView()
        {
            ForestLocationsRootView prefab = Resources.Load<ForestLocationsRootView>(PREFAB_PATH);
            _forestLocationsRootView = _container.Instantiate(prefab);
            return _forestLocationsRootView;
        }

        public ForestLocationsRootView GetLocationsRootView()
        {
            return _forestLocationsRootView;
        }
    }
}