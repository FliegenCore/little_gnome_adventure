using System;
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
        
        public LocationsController(LocationsModel locationsModel)
        {
            LocationsModel = locationsModel;
            _locationsStateMachine = new Fsm();
        }

        public void Initialize()
        {
            LocationsModel.CurrentLocation.Subscribe(SetCurrentRoom);
        }
        
        public void CreateLocation(ILocationFactory locationFactory)
        {
            locationFactory.Create(_locationsStateMachine);
        }

        private void SetCurrentRoom(LocationsIdEnum locationIdEnum)
        {
            Debug.Log(locationIdEnum.ToString());
            _locationsStateMachine.SetState(StaticLocationsConnection.LocationsTypeMap[locationIdEnum]);
        }

        public LocationAbstractState GetCurrentLocation()
        {
            Type type = StaticLocationsConnection.LocationsTypeMap[LocationsModel.CurrentLocation.Value];
            
            return (LocationAbstractState)_locationsStateMachine.GetState(type);
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