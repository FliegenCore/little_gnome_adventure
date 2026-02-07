using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.InspectSystem.Interactable.Nightstand
{
    public class NightstandModel : AbstractInteractableModel
    {
        public NightstandModel(Vector2 position, string id, IContactTriggerProvider contactTriggerProvider) : base(contactTriggerProvider, position, id)
        {
        }
    }
}