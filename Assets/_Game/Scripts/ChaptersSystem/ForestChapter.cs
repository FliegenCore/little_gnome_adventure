using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Variants;
using _Game.Scripts.UpdateSystems;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.ChaptersSystem
{
    public class ForestChapter: IInitializable
    {
        private readonly DoorFactory _doorFactory;
        private readonly ForestChapterConfig _forestChapterConfig;
        private readonly IPlayerFactory _playerFactory;
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        private readonly UpdateController _updateController;
        private readonly InspectForestRegistratorService _inspectForestRegistratorService;
        private readonly CameraController _cameraController;
        
        private readonly HouseLocationFactory _houseLocationFactory;
        private readonly ForestLocationFactory _forestLocationFactory;
        private readonly TestLocationFactory _testLocationFactory;
        
        private LocationsController _locationsController;
        private List<DoorView> _allDoorsView = new();
        
        public ForestChapter(DoorFactory doorFactory, 
            ForestChapterConfig forestChapterConfig,
            IPlayerFactory playerFactory,
            ForestRootViewFactory forestRootViewFactory,
            HouseLocationFactory houseLocationFactory,
            TestLocationFactory testLocationFactory,
            LocationsControllerFactory locationsControllerFactory,
            UpdateController updateController,
            InspectForestRegistratorService inspectForestRegistratorService,
            CameraController cameraController,
            ForestLocationFactory forestLocationFactory)
        {
            _testLocationFactory = testLocationFactory;
            _cameraController = cameraController;
            _inspectForestRegistratorService = inspectForestRegistratorService;
            _updateController = updateController;
            _locationsControllerFactory = locationsControllerFactory;
            _forestRootViewFactory = forestRootViewFactory;
            _forestLocationFactory = forestLocationFactory;
            _playerFactory = playerFactory;
            _forestChapterConfig = forestChapterConfig;
            _doorFactory = doorFactory;
            _houseLocationFactory =  houseLocationFactory;
        }
        
        public void Initialize()
        {
            CreateLocation();
            CreatePlayer();
            TieCamera();
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
            //create locations objects, characters;
            _locationsController.CreateLocation(_houseLocationFactory);
            _locationsController.CreateLocation(_forestLocationFactory);
            _locationsController.CreateLocation(_testLocationFactory);
            //------------
            _locationsController.LocationsModel.CurrentLocation.Value = LocationsIdEnum.MainHouse;
            
            _locationsController.Initialize();
        }
        
        private void CreatePlayer()
        {
            _playerFactory.CreatePlayer();
        }
        
        private void TieCamera()
        {
            _cameraController.SetFollowTarget(_playerFactory.GetPlayer().PlayerView.transform);
            _cameraController.SetFollowZone(_locationsController.GetCurrentLocation().AbstractLocationView.CameraCollider);
        }
        
        private void CacheAllDoorView()
        {
            _allDoorsView.AddRange(_forestRootViewFactory.GetLocationsRootView().StartHouseView.Doors);
            _allDoorsView.AddRange(_forestRootViewFactory.GetLocationsRootView().ForestLocationView.Doors);
            _allDoorsView.AddRange(_forestRootViewFactory.GetLocationsRootView().TestRoom.Doors);
        }

        private void CreateDoorConnections()
        {
            foreach (var connection in _forestChapterConfig.DoorConnections)
            {
                DoorView view = _allDoorsView.FirstOrDefault(x => x.MarkId == connection.Id);
                
                _doorFactory.Create(connection.Id.ToString(), 
                    connection.ConnectionId.ToString(),
                    view);
                
                DoorView view2 = _allDoorsView.FirstOrDefault(x => x.MarkId == connection.ConnectionId);
                
                _doorFactory.Create(connection.ConnectionId.ToString(), 
                    connection.Id.ToString(),
                    view2);
            }
        }
        
        private void RegisterUpdates()
        {
            _updateController.AddListener(_playerFactory.GetPlayer());
            _updateController.AddListener(_locationsController);
        }
    }
}