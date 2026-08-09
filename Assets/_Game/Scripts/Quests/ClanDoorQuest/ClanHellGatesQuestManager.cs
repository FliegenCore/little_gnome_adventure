using System;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.Quests.ClanDoorQuest.Gates;
using _Game.Scripts.Quests.ClanDoorQuest.Gates.Signals;
using _Game.Scripts.Quests.ClanDoorQuest.Gates.View;
using _Game.Scripts.RoomSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest
{
    public class ClanHellGatesQuestManager : IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly LocationsRootView _locationsRootView;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly IInteractableFactory _interactableFactory;
        private readonly ItemFactory _itemFactory;
        private HellGatesPasswordService _hellGatesPasswordService;
        
        public ClanHellGatesQuestManager(
            EventBus eventBus, 
            InspectRegistratorService inspectRegistratorService,
            RootViewFactory rootViewFactory,
            InputSystem_Actions inputSystemActions,
            IInteractableFactory interactableFactory,
            ItemFactory itemFactory
            )
        {
            _interactableFactory       = interactableFactory;
            _inputSystemActions        = inputSystemActions; 
            _inspectRegistratorService = inspectRegistratorService;
            _locationsRootView         = rootViewFactory.GetLocationsRootView();
            _eventBus                  = eventBus;
            _itemFactory               = itemFactory;
            
        }
        
        public void Initialize()
        {
            HellGatesModel hellGatesModel = CreateHellGates();
            CreateGatesQuestInspect(hellGatesModel);

            _eventBus.Subscribe<AcceptAnimationHellGatesPasswordSignal, Action>(this, AcceptAnimationPassword);
            _eventBus.Subscribe<RejectAnimationHellGatesPasswordSignal, Action>(this, RejectAnimationPassword);
        }

        private void RejectAnimationPassword(Action callback)
        {
            HellGatesPasswordInspectView interactableAnimationView = _locationsRootView.InspectsView.GatesPassword;
            
            interactableAnimationView.RejectBarAnimation(callback);
        }
        
        private void AcceptAnimationPassword(Action callback)
        {
            HellGatesPasswordInspectView interactableAnimationView = _locationsRootView.InspectsView.GatesPassword;
            
            interactableAnimationView.AcceptBarAnimation(callback);
        }
        
        private void CreateGatesHintItem()
        {
           
        }

        private void CreateGatesQuestInspect(HellGatesModel hellGatesModel)
        {
            HellGatesPasswordInspectView interactableAnimationView = _locationsRootView.InspectsView.GatesPassword;
           
            HellGatesPasswordModel hellGatesPasswordModel = new HellGatesPasswordModel(
                null, 
                Vector2.zero, 
                nameof(HellGatesPasswordModel));
            
            interactableAnimationView.Construct(hellGatesPasswordModel);
            
            _hellGatesPasswordService = 
                new HellGatesPasswordService(hellGatesPasswordModel, hellGatesModel, _eventBus);
            _hellGatesPasswordService.Initialize();
            
            HellGatesPasswordInput hellGatesPasswordInput = new HellGatesPasswordInput(
                _inputSystemActions,
                _eventBus,
                hellGatesPasswordModel
                );
            
            _inspectRegistratorService.RegisterInspect(
                "GatesPassword",
                interactableAnimationView,
                inspectInputHandler: hellGatesPasswordInput);
        }

        private HellGatesModel CreateHellGates()
        {
            HellGatesView hellGatesView = _locationsRootView.FuckingHellWithGatesLocationView.HellGatesView;
            
            HellGatesModel hellGatesModel = new HellGatesModel(
                hellGatesView.ContactTriggerProvider,
                hellGatesView.Position,
                "HellGates"
            );
            hellGatesView.Construct(hellGatesModel.OpenDoor);
            HellGatesBehaviour hellGatesBehaviour = new HellGatesBehaviour(_eventBus, hellGatesModel);

            _interactableFactory.CreateInteractable(hellGatesBehaviour, hellGatesView, hellGatesModel);

            return hellGatesModel;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AcceptAnimationHellGatesPasswordSignal>(this);
            _eventBus.Unsubscribe<RejectAnimationHellGatesPasswordSignal>(this);
            _hellGatesPasswordService?.Dispose();
        }
    }
}