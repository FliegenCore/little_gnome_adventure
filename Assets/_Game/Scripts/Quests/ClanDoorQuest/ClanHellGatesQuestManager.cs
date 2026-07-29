using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.Quests.ClanDoorQuest.Gates;
using _Game.Scripts.Quests.ClanDoorQuest.Gates.View;
using _Game.Scripts.RoomSystems;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest
{
    public class ClanHellGatesQuestManager
    {
        private readonly EventBus _eventBus;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly LocationsRootView _locationsRootView;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly IInteractableFactory _interactableFactory;
        private readonly ItemFactory _itemFactory;
        
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
            CreateGatesQuestInspect();
            CreateHellGates();
        }

        private void CreateGatesHintItem()
        {
            
        }

        private void CreateGatesQuestInspect()
        {
            HellGatesPasswordInspectView interactableAnimationView = _locationsRootView.InspectsView.GatesPassword;
           
            HellGatesPasswordModel hellGatesPasswordModel = new HellGatesPasswordModel(
                null, 
                Vector2.zero, 
                nameof(HellGatesPasswordModel));
            
            interactableAnimationView.Construct(hellGatesPasswordModel);
            
            HellGatesPasswordInput hellGatesPasswordInput = new HellGatesPasswordInput(
                _inputSystemActions,
                _eventBus,
                hellGatesPasswordModel
                );
            
            _inspectRegistratorService.RegisterInspect("GatesPassword", interactableAnimationView, inspectInputHandler: hellGatesPasswordInput);
        }

        private void CreateHellGates()
        {
            HellGatesBehaviour hellGatesBehaviour = new HellGatesBehaviour(_eventBus);

            HellGatesView hellGatesView = _locationsRootView.FuckingHellWithGatesLocationView.HellGatesView;
            
            NightstandModel nightstandModel = new NightstandModel(
                hellGatesView.Position, 
                "HellGates",
                hellGatesView.ContactTriggerProvider
                );

            _interactableFactory.CreateInteractable(hellGatesBehaviour, hellGatesView, nightstandModel);
        }
    }
}