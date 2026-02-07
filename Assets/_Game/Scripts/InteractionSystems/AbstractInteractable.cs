using System;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems
{
    public abstract class AbstractInteractable : IDisposable
    {
        public readonly AbstractInteractableModel AbstractInteractableModel;
        public readonly NightstandView InteractableView;
        protected readonly EventBus EventBus;
        
        public AbstractInteractable(AbstractInteractableModel abstractInteractableModel, NightstandView interactableView, EventBus eventBus)
        {
            EventBus = eventBus;
            AbstractInteractableModel = abstractInteractableModel;
            InteractableView = interactableView;
            if (AbstractInteractableModel.ContactTriggerProvider != null)
            {
                AbstractInteractableModel.ContactTriggerProvider.OnEnter += OnPlayerCollided;
                AbstractInteractableModel.ContactTriggerProvider.OnExit += OnPlayerExit;
            }
        }

        public abstract void Interact();
        public abstract bool CanInteract();
        
        protected virtual void OnPlayerCollided(Collider2D collider2D)
        {
            if (!collider2D.GetComponent<PlayerView>())
            {
                return;
            }
            
            EventBus.TriggerEvenet<SetCurrentInteractableSignal, AbstractInteractable>(this);
        }

        protected virtual void OnPlayerExit(Collider2D collider2D)
        {
            if (!collider2D.GetComponent<PlayerView>())
            {
                return;
            }
            
            EventBus.TriggerEvenet<RemoveCurrentInteractableSignal, AbstractInteractable>(this);
        }
        
        public void Dispose()
        {
            if (AbstractInteractableModel.ContactTriggerProvider != null)
            {
                AbstractInteractableModel.ContactTriggerProvider.OnEnter -= OnPlayerCollided;
                AbstractInteractableModel.ContactTriggerProvider.OnExit -= OnPlayerExit;
            }
        }
    }
}