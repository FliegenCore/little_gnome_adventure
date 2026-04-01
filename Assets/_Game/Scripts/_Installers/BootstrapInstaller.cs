using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.GameStateSystems;
using _Game.Scripts.Input;
using _Game.Scripts.Sound;
using _Game.Scripts.UpdateSystems;
using Core.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts._Installers
{
    public class BootstrapInstaller : LifetimeScope
    {
        [SerializeField] private AudioSourceStorage _audioSourceStorage;
        [SerializeField] private AudioStorageConfig _audioStorageConfig;
        
        protected override void Awake()
        {
            base.Awake();
            
            if (FindObjectsOfType<BootstrapInstaller>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<InputSystem_Actions>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<MoveDirectionInput>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.RegisterComponentOnNewGameObject<UpdateController>(Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
            builder.RegisterComponentOnNewGameObject<FixedUpdateController>(Lifetime.Singleton).DontDestroyOnLoad().AsSelf();
            builder.Register<GameStateController>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SoundManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.RegisterInstance(_audioStorageConfig).AsSelf().AsImplementedInterfaces();
            
            DontDestroyOnLoad(_audioSourceStorage);
            builder.RegisterInstance(_audioSourceStorage);
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}