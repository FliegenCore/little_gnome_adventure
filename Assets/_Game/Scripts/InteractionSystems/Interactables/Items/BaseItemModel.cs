using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItemModel : AbstractInteractableModel
    {
        public BaseItemModel(IContactTriggerProvider contactTriggerProvider, Vector2 position, string id) : base(contactTriggerProvider, position, id)
        {
        }
    }
}