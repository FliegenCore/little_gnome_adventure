using _Game.Scripts.GameInitializeSystems;
using _Game.Scripts.GameStateSystems;
using _Game.Scripts.Input;
using _Game.Scripts.UpdateSystems;
using Core.Common;
using VContainer;
using VContainer.Unity;

namespace _Game.Scripts._Installers
{
    public class BootstrapInstaller : LifetimeScope
    {
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
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}