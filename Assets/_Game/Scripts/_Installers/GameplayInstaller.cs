using _Game.Scripts.CameraSystem;
using _Game.Scripts.ChaptersSystem;
using _Game.Scripts.CutsceneSystem.Impl;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.Hacks;
using _Game.Scripts.InspectSystem.Camera;
using _Game.Scripts.InteractionSystems.HintSystem;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.InventorySystem.Factories;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Factory.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Impl.DreamQuestFirst;
using _Game.Scripts.RoomSystems.Impl.DreamRoom1;
using _Game.Scripts.RoomSystems.Variants;
using _Game.Scripts.Sound;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts._Installers
{
    public class GameplayInstaller: LifetimeScope
    {
        [SerializeField] private ForestChapterConfig _forestChapterConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private InspectCamera _inspectCamera;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private ItemConfigs _itemConfigs;
        
        
        private void Start()
        {
            var hackController = FindObjectOfType<HackController>();
            if (hackController != null)
            {
                Container.InjectGameObject(hackController.gameObject);
                Debug.Log("Injected dependencies into HackController");
            }
            
        }
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_forestChapterConfig);
            builder.RegisterInstance(_inspectCamera);
            builder.RegisterInstance(_mainCamera);
            builder.RegisterInstance(_cinemachineCamera).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(_inventoryView).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(_itemConfigs).AsSelf().AsImplementedInterfaces();
            builder.Register<OutlineHintController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<CameraController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<PlayerFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DoorsService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DoorFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ForestRootViewFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<HouseLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<TestLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ForestLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DreamLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DreamFirstQuestLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<LocationsControllerFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InspectController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InspectForestRegistratorService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InventoryFactoryProvider>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InventoryFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ItemInfoProvider>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InventoryProxy>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DialogueManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InteractableFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<CutsceneManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            RegisterCurrentChapterInitializer(builder);
        }

        private void RegisterCurrentChapterInitializer(IContainerBuilder builder)
        {
            builder.Register<ForestChapter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}