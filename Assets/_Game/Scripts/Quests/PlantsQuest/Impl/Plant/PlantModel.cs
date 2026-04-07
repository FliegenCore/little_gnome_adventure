using _Game.Scripts.InteractionSystems;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.PlantsQuest.Impl.Plant
{
    public class PlantModel : AbstractInteractableModel
    {
        public ReactiveProperty<int> Height;
        public ReactiveProperty<bool> CanInteract;
        public ReactiveProperty<bool> NeedCallback;
        public ReactiveProperty<bool> ColliderIsEnabled;
        public readonly int NeedHeight;
        public readonly int MaxHeight;
        
        public PlantModel(
            IContactTriggerProvider contactTriggerProvider,
            Vector2 position, 
            string id,
            int needHeight) : 
            base(contactTriggerProvider, position, id)
        {
            ColliderIsEnabled =  new ReactiveProperty<bool>(true);
            NeedCallback = new(true);
            CanInteract = new(true);
            Height = new ReactiveProperty<int>(1);
            NeedHeight = needHeight;
            MaxHeight = 4;
        }
    }
}