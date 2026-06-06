using System;
using System.Collections.Generic;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.RoomSystems.LocationsStates;
using _Game.Scripts.UpdateSystems;
using UnityEngine;

namespace _Game.Scripts.RoomSystems
{
    public class LocationsController : IUpdateListener, IDisposable
    {
        public readonly LocationsModel LocationsModel;

        private readonly Fsm _locationsStateMachine;
        private readonly CameraController _cameraController;
        private List<LocationAbstractState> _locationAbstractStates = new();

        public float GetCurrentCameraSize => GetCurrentLocation().AbstractLocationView.CameraSize;
        
        public LocationsController(LocationsModel locationsModel, CameraController cameraController)
        {
            LocationsModel         = locationsModel;
            _cameraController      = cameraController;
            _locationsStateMachine = new Fsm();
        }

        public void Initialize()
        {
            LocationsModel.CurrentLocation.Subscribe(SetCurrentRoom);
        }
        
        public void CreateLocation(ILocationFactory locationFactory)
        {
            _locationAbstractStates.Add(locationFactory.Create(_locationsStateMachine));
        }

        public LocationAbstractState GetLocationByView(AbstractLocationView view)
        {
            foreach (var state in _locationAbstractStates)
            {
                if(state.AbstractLocationView == view)
                    return state;
            }
            
            return null;
        }
        
        private void SetCurrentRoom(Type locationAbstractState)
        {
            _locationsStateMachine.SetState(locationAbstractState);
            _cameraController.SetFollowZone(GetCurrentLocation().AbstractLocationView.CameraCollider);
            _cameraController.ZoomTo(GetCurrentCameraSize, 0, null);
        }

        public LocationAbstractState GetCurrentLocation()
        {
            return (LocationAbstractState)_locationsStateMachine.GetState(LocationsModel.CurrentLocation.Value);
        }
        
        public void Update(float deltaTime)
        {
            _locationsStateMachine?.Update(deltaTime);
        }

        public void Dispose()
        {
            LocationsModel.CurrentLocation.Unsubscribe(SetCurrentRoom);
        }
    }
}