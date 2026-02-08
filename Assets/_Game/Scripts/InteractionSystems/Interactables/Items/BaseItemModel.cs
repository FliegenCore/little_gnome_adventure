using UniRx;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Interactables.Items
{
    public class BaseItemModel : AbstractInteractableModel
    {
        public readonly ReactiveProperty<bool> IsEnabled;
        
        public BaseItemModel(IContactTriggerProvider contactTriggerProvider,
            Vector2 position, 
            string id,
            bool isEnabled) :
            base(contactTriggerProvider, position, id)
        {
            IsEnabled = new ReactiveProperty<bool>(isEnabled);
        }
    }
}