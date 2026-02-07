using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.InventorySystem
{
    public class InventoryItemModel : AbstractInteractableModel
    {
        public readonly SpriteStorage SpriteStorage;
        
        public InventoryItemModel(IContactTriggerProvider contactTriggerProvider, Vector2 position, string id, SpriteStorage spriteStorage) : base(contactTriggerProvider, position, id)
        {
            SpriteStorage = spriteStorage;
        }
    }
}