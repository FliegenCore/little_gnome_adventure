using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.Animations.Impl
{
    public class CharacterModel : AbstractInteractableModel
    {
        public CharacterModel(
            IContactTriggerProvider contactTriggerProvider,
            Vector2 position,
            string id) 
            : base(contactTriggerProvider, position, id)
        {
        }
    }
}