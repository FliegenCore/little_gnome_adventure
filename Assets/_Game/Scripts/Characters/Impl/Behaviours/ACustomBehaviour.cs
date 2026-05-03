using System;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours
{
    public abstract class ACustomBehaviour
    {
        protected readonly EventBus _eventBus;
        
        public ACustomBehaviour(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public abstract bool CanInteract();
        public abstract void Interact(Action callback);
    }
}