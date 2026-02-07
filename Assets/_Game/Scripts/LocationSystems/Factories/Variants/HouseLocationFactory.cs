using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.InspectWindows;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.RoomSystems.LocationModels;
using _Game.Scripts.RoomSystems.LocationsStates;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.RoomSystems.Variants
{
    public class HouseLocationFactory : ILocationFactory
    {
        private readonly ForestRootViewFactory _forestRootViewFactory;
        private readonly EventBus _eventBus;
        
        public HouseLocationFactory(ForestRootViewFactory forestRootViewFactory, EventBus eventBus)
        {
            _forestRootViewFactory = forestRootViewFactory;
            _eventBus = eventBus;
        }
        
        public void Create(Fsm fsm)
        {
            LampModel lampModel = new LampModel(0.6f, 1.6f);
            
            var nightstand = CreateInteractable("Nightstand", _forestRootViewFactory.GetLocationsRootView().StartHouseView.NightstandView);
            CreateInteractable("Table", _forestRootViewFactory.GetLocationsRootView().StartHouseView.Table);
            
            StartHouseLocationModel startHouseLocationModel = new StartHouseLocationModel(LocationsIdEnum.Forest, lampModel, nightstand);
            
            StartHouseState startHouseState = new StartHouseState(fsm, startHouseLocationModel, _forestRootViewFactory.GetLocationsRootView().StartHouseView);
            
            _forestRootViewFactory.GetLocationsRootView().StartHouseView.Construct(lampModel);
            
            fsm.AddState(startHouseState);
        }

        private Nightstand CreateInteractable(string id, NightstandView view)
        {
            NightstandView nightstandView = view;
            NightstandModel nightstandModel = new NightstandModel(nightstandView.transform.position,id, nightstandView.ContactTriggerProvider);
            Nightstand nightstand = new Nightstand(_eventBus, nightstandModel, nightstandView);
            
            return nightstand;
        }
    }
}