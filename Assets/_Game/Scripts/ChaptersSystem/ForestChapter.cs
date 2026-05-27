using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Impl.DreamQuestFirst;
using _Game.Scripts.RoomSystems.Impl.DreamRoom1;
using _Game.Scripts.RoomSystems.LocationsStates;
using _Game.Scripts.RoomSystems.Variants;
using _Game.Scripts.UpdateSystems;
using UnityEngine;
using VContainer.Unity;

namespace _Game.Scripts.ChaptersSystem
{
    public class ForestChapter: IInitializable
    {
        private readonly DoorFactory _doorFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        private readonly UpdateController _updateController;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly CameraController _cameraController;
        
        private readonly IReadOnlyList<ILocationFactory> _locationFactories;

        private LocationsController _locationsController;
        private List<DoorView> _allDoorsView = new();
        
        public ForestChapter(DoorFactory doorFactory, 
            IPlayerFactory playerFactory,
            LocationsControllerFactory locationsControllerFactory,
            UpdateController updateController,
            InspectRegistratorService inspectRegistratorService,
            CameraController cameraController,
            ForestRootViewFactory forestRootViewFactory,
            IReadOnlyList<ILocationFactory> locationsFactory
            )
        {
            _forestRootViewFactory      = forestRootViewFactory;
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
            CreatePlayer();
            TieCamera();
            CacheAllDoorView();
            CreateDoorConnections();
            RegisterInspects();
            RegisterUpdates();
        }
        
        private void RegisterInspects()
        {
            _inspectRegistratorService.Initialize();
        }

        private void CreateLocation()
        {
            _forestRootViewFactory.CreateForestLocationsRootView();
            _locationsController = _locationsControllerFactory.Create();
            //create locations ;

            foreach (var locationFactory in _locationFactories)
            {
                _locationsController.CreateLocation(locationFactory);
            }
            
            //------------
            _locationsController.LocationsModel.CurrentLocation.Value = typeof(StartHouseState);
            
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
            _allDoorsView.AddRange(_forestRootViewFactory.GetLocationsRootView().DreamLocationView.Doors);
            _allDoorsView.AddRange(_forestRootViewFactory.GetLocationsRootView().DreamQuestFirstLocationView.Doors);
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