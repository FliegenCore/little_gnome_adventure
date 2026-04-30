using _Game.Scripts.InteractionSystems;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl.Needle
{
    public class NeedleModel : AbstractInteractableModel
    {
        public readonly ReactiveProperty<int> Depth;
        
        public NeedleModel(
            IContactTriggerProvider contactTriggerProvider,
            Vector2 position,
            string id
            ) : base(contactTriggerProvider, position, id)
        {
            Depth = new ReactiveProperty<int>(0);
        }
    }
}