using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems.Interactables.Items;
using _Game.Scripts.InteractionSystems.Interactables.Items.Managers;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using _Game.Scripts.Quests.MushroomQuest.Cutscenes;
using _Game.Scripts.RoomSystems.Impl.DreamForest;
using _Game.Scripts.Sound;
using Core.Common;

namespace _Game.Scripts.Quests.MushroomQuest
{
    public class MushroomQuestManager
    {
        public const string BUSMAN_JUMP_INSPECT_ANIMATION = "BusmanJumpInspectAnimation";
        
        private readonly EventBus _eventBus;
        private readonly IInteractableFactory _interactableFactory;
        private readonly InventoryProxy _inventoryProxy;
        private readonly ICutsceneManager _cutsceneManager;
        private readonly DreamForestLocationState _dreamForestLocationState;
        private readonly ItemFactory _itemFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly CameraController _cameraController;
        private readonly ISoundManager _soundManager;
        private readonly InspectRegistratorService _inspectRegistratorService;
        private readonly InspectAnimationView _busmanJumpAnimation;
        private readonly CameraControllerHelper _cameraControllerHelper;

        private BusmanInitializer _busmanInitializer;
        
        public MushroomQuestManager(
            IInteractableFactory interactableFactory,
            EventBus eventBus,
            InventoryProxy inventoryProxy,
            ICutsceneManager cutsceneManager,
            DreamForestLocationState dreamForestLocationState,
            ItemFactory itemFactory,
            IPlayerFactory playerFactory,
            CameraController cameraController,
            ISoundManager soundManager,
            InspectRegistratorService inspectRegistratorService,
            InspectAnimationView busmanJumpAnimationInspect,
            CameraControllerHelper cameraControllerHelper
            )
        {
            _cameraControllerHelper    = cameraControllerHelper;
            _busmanJumpAnimation       = busmanJumpAnimationInspect;
            _inspectRegistratorService = inspectRegistratorService;
            _soundManager              = soundManager;
            _cameraController          = cameraController;
            _playerFactory             = playerFactory;
            _itemFactory               = itemFactory;
            _dreamForestLocationState  = dreamForestLocationState;
            _eventBus                  = eventBus;
            _inventoryProxy            = inventoryProxy;
            _interactableFactory       = interactableFactory;            
            _cutsceneManager           = cutsceneManager;

            _busmanInitializer         = new BusmanInitializer();
        }

        public void Initialize()
        {
            CreateMcMushroom();
            CreateMushrooms();
            CreateBusman();
            CreateOrcWoman();
            RegisterBusmanJumpAnimationInspect();
        }

        private void CreateMcMushroom()
        {
            McMushroomBehaviour mcMushroomBehaviour = new McMushroomBehaviour(_eventBus, _inventoryProxy);

            CreateCharacter(nameof(ECharacters.McMushroom),
                mcMushroomBehaviour,
                _dreamForestLocationState.DreamForestLocationView.McMushroomView);
        }

        private void CreateOrcWoman()
        {
            OrcWomanBehaviour orcWomanBehaviour = new OrcWomanBehaviour(_eventBus,
                _dreamForestLocationState.DreamForestLocationView.OrcWomanView.AnimationControl,
                CreateOpenBusByTicketCutscene(_busmanInitializer.Fsm),
                _cutsceneManager
                );
            
            CreateCharacter(nameof(ECharacters.OrcWoman),
                orcWomanBehaviour,
                _dreamForestLocationState.DreamForestLocationView.OrcWomanView);
        }

        private OpenBusByTicketCutscene CreateOpenBusByTicketCutscene(Fsm busmanFsm)
        {
            BusmanView busmanView = _dreamForestLocationState.DreamForestLocationView.BusmanView;
            
            OpenBusByTicketCutscene openBusByTicketCutscene = new OpenBusByTicketCutscene(
                busmanFsm,
                _cameraController,
                _cameraControllerHelper,
                _dreamForestLocationState.DreamForestLocationView.OrcWomanView.AnimationControl,
                busmanView.CameraFollowPoint
                );
            
            return openBusByTicketCutscene;
        }

        private void CreateMushrooms()
        {
            foreach (var mushroom in _dreamForestLocationState.DreamForestLocationView.Mushrooms)
            {
                _itemFactory.CreateItem(mushroom, ItemId.Mushroom);
            }
        }

        private void CreateBusman()
        {
            BusmanView busmanView = _dreamForestLocationState.DreamForestLocationView.BusmanView;
            
            _busmanInitializer.Initialize(busmanView.AnimationControl);

            GnomeEnterInBusCutscene gnomeEnterInBusCutscene = 
                new GnomeEnterInBusCutscene(
                    _eventBus,
                    _playerFactory,
                    busmanView.CameraFollowPoint,
                    _busmanInitializer.Fsm, 
                    _cameraController,
                    _soundManager,
                    _busmanJumpAnimation
                    );
            
            CreateCharacter(
                nameof(ECharacters.Busman), 
                new BusmanBehaviour(_eventBus, _busmanInitializer.Fsm, _cutsceneManager, gnomeEnterInBusCutscene),
                busmanView);
        }
        
        private Interactable CreateCharacter(string id, ACustomBehaviour customBehaviour, NightstandView nightstandView)
        {
            return _interactableFactory.CreateInteractable(customBehaviour, nightstandView, 
                new CharacterModel(nightstandView.ContactTriggerProvider, nightstandView.Position, id));
        }
        
        private void RegisterBusmanJumpAnimationInspect()
        {
            _inspectRegistratorService.RegisterInspect(BUSMAN_JUMP_INSPECT_ANIMATION, _busmanJumpAnimation);
        }
    }
}