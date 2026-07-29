using _Game.Scripts.InteractionSystems;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesPasswordModel : AbstractInteractableModel
    {
        public readonly ReactiveProperty<int> CurrentIndex = new ReactiveProperty<int>(0);
        public readonly ReactiveProperty<int> WritedCount = new ReactiveProperty<int>(0);
        public readonly ReactiveProperty<string> CurrentPassword = new ReactiveProperty<string>();
        
        public HellGatesPasswordModel(
            IContactTriggerProvider contactTriggerProvider,
            Vector2 position,
            string id
            ) :
            base(contactTriggerProvider, position, id)
        {
            
        }
    }
}