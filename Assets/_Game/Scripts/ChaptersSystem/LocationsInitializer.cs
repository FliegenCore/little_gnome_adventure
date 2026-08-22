using System.Collections.Generic;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.UpdateSystems;
using VContainer.Unity;

namespace _Game.Scripts.ChaptersSystem
{
    public class LocationsInitializer : IInitializable
    {
        private readonly DoorFactory _doorFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly RootViewFactory _rootViewFactory;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        private readonly UpdateController _updateController;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly CameraController _cameraController;
        
        private readonly IReadOnlyList<ILocationFactory> _locationFactories;

        private LocationsController _locationsController;
        private List<DoorView> _allDoorsView = new();
        
        public LocationsInitializer(DoorFactory doorFactory, 
            IPlayerFactory playerFactory,
            LocationsControllerFactory locationsControllerFactory,
            UpdateController updateController,
            InspectRegistratorService inspectRegistratorService,
            CameraController cameraController,
            RootViewFactory rootViewFactory,
            IReadOnlyList<ILocationFactory> locationsFactory
            )
        {
            _rootViewFactory      = rootViewFactory;
            _locationFactories          = locationsFactory;
            _cameraController           = cameraController;
            _inspectRegistratorService  = inspectRegistratorService;
            _updateController           = updateController;
            _locationsControllerFactory = locationsControllerFactory;
            _playerFactory              = playerFactory;
            _doorFactory                = doorFactory;
        }
        
        public void Initialize()
        {
            CreateLocation();
            CacheAllDoorView();
            CreateDoorConnections();
            SetStartLocation();
            CreatePlayer();
            TieCamera();
            RegisterInspects();
            RegisterUpdates();
        }
        
        private void RegisterInspects()
        {
            _inspectRegistratorService.Initialize();
        }

        private void CreateLocation()
        {
            _rootViewFactory.CreateForestLocationsRootView();
            _locationsController = _locationsControllerFactory.Create();

            foreach (var locationFactory in _locationFactories)
            {
                _locationsController.CreateLocation(locationFactory);
            }
        }
        
        private void CreatePlayer()
        {
            _playerFactory.CreatePlayer();
        }

        private void SetStartLocation()
        {
            _locationsController.Initialize();
        }
        
        private void TieCamera()
        {
            _cameraController.SetFollowTarget(_playerFactory.GetPlayer().PlayerView.transform);
        }
        
        private void CacheAllDoorView()
        {
            foreach (var locationFactory in _locationFactories)
            {
                foreach (var door in locationFactory.GetLastCreated().AbstractLocationView.Doors)
                {
                    _allDoorsView.Add(door);
                }
            }
        }

        private void CreateDoorConnections()
        {
            foreach (var doorView in _allDoorsView)
            {
                string doorId = $"{doorView.ConnectedLocationView.name} {doorView.name}";

                DoorView view2 = null;
                string door2Id = "";
                
                if (doorView.ConnectedDoor != null)
                {
                    view2 = doorView.ConnectedDoor;
                    door2Id = $"{view2.ConnectedLocationView.name} {view2.name}";
                }
                

                if (view2 != null)
                {
                    _doorFactory.Create(doorId, 
                        door2Id,
                        doorView);
                }
                else
                {
                    _doorFactory.Create(doorId, 
                        doorId,
                        doorView);
                }
                
            }
        }
        
        private void RegisterUpdates()
        {
            _updateController.AddListener(_playerFactory.GetPlayer());
            _updateController.AddListener(_locationsController);
        }
    }
}