using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl.Needle
{
    public class NeedleModel : AbstractInteractableModel
    {
        public NeedleModel(
            IContactTriggerProvider contactTriggerProvider,
            Vector2 position,
            string id
            ) : base(contactTriggerProvider, position, id)
        {
            
        }
    }
}