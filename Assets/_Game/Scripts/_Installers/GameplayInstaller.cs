using _Game.Scripts.ChaptersSystem;
using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Variants;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts._Installers
{
    public class GameplayInstaller: LifetimeScope
    {
        [SerializeField] private ForestChapterConfig _forestChapterConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerConfig);
            builder.RegisterInstance(_forestChapterConfig);
            builder.Register<PlayerFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DoorsService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<DoorFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<ForestRootViewFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<HouseLocationFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<LocationsControllerFactory>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InspectController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InspectForestRegistratorService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            RegisterCurrentChapterInitializer(builder);
        }

        private void RegisterCurrentChapterInitializer(IContainerBuilder builder)
        {
            builder.Register<ForestChapter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}