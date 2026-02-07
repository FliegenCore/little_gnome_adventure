using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Variants;
using _Game.Scripts.UpdateSystems;
using VContainer.Unity;

namespace _Game.Scripts.ChaptersSystem
{
    public class ForestChapter: IInitializable
    {
        private readonly DoorFactory _doorFactory;
        private readonly ForestChapterConfig _forestChapterConfig;
        private readonly IPlayerFactory _playerFactory;
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly HouseLocationFactory _houseLocationFactory;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        private readonly UpdateController _updateController;
        private readonly InspectForestRegistratorService _inspectForestRegistratorService;
        
        private LocationsController _locationsController;
        private List<DoorView> _allDoors = new();
        
        public ForestChapter(DoorFactory doorFactory, 
            ForestChapterConfig forestChapterConfig,
            IPlayerFactory playerFactory,
            ForestRootViewFactory forestRootViewFactory,
            HouseLocationFactory houseLocationFactory,
            LocationsControllerFactory locationsControllerFactory,
            UpdateController updateController,
            InspectForestRegistratorService inspectForestRegistratorService)
        {
            _inspectForestRegistratorService = inspectForestRegistratorService;
            _updateController = updateController;
            _locationsControllerFactory = locationsControllerFactory;
            _forestRootViewFactory = forestRootViewFactory;
            _playerFactory = playerFactory;
            _forestChapterConfig = forestChapterConfig;
            _doorFactory = doorFactory;
            _houseLocationFactory =  houseLocationFactory;
        }
        
        public void Initialize()
        {
            CreateLocation();
            CreatePlayer();
            CacheAllDoorView();
            CreateDoorConnections();
            RegisterInspects();
            RegisterUpdates();
        }

        private void RegisterInspects()
        {
            _inspectForestRegistratorService.Initialize();
        }

        private void CreateLocation()
        {
            _forestRootViewFactory.CreateForestLocationsRootView();
            _locationsController = _locationsControllerFactory.Create();
            _locationsController.CreateLocation(_houseLocationFactory);
            
            _locationsController.Initialize();
        }
        
        private void CreatePlayer()
        {
            _playerFactory.CreatePlayer();
        }

        private void CacheAllDoorView()
        {
            _allDoors.AddRange(_forestRootViewFactory.GetLocationsRootView().StartHouseView.Doors);
        }

        private void CreateDoorConnections()
        {
            foreach (var connection in _forestChapterConfig.DoorConnections)
            {
                DoorView view = _allDoors.FirstOrDefault(x => x.MarkId == connection.Id);
                
                _doorFactory.Create(nameof(connection.Id), 
                    nameof(connection.ConnectionId),
                    view);
            }
        }
        
        private void RegisterUpdates()
        {
            _updateController.AddListener(_playerFactory.GetPlayer());
            _updateController.AddListener(_locationsController);
        }
    }
}