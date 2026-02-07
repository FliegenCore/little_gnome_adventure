using UnityEngine;

namespace _Game.Scripts.InteractionSystems
{
    public abstract class AbstractInteractableModel
    {
        public readonly IContactTriggerProvider ContactTriggerProvider;
        public readonly Vector2 Position;
        public readonly string Id;

        public AbstractInteractableModel(IContactTriggerProvider contactTriggerProvider, Vector2 position, string id)
        {
            ContactTriggerProvider = contactTriggerProvider;
            Position = position;
            Id = id;
        }
    }
}