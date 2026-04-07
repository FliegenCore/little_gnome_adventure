using _Game.Scripts.DialogueSystem;
using _Game.Scripts.InventorySystem;
using Core.Common;
using UnityEditor;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl
{
    public class GrannyBehaviour : ACustomBehaviour
    {
        private bool _itemGived;
        private bool _questIsDone;
        
        public GrannyBehaviour(EventBus eventBus) : base(eventBus)
        {
            _eventBus.Subscribe<DialogueEventSignal, string>(this, OnItemGive);
            _eventBus.Subscribe<OnFlowersHeightRightSignal>(this,  OnFlowersPlanted);
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact()
        {
            if (_itemGived)
            {
                _eventBus.TriggerEvenet<StartDialogueSignal, string>("granny_d2");
                return;
            }
            
            if (_questIsDone)
            {
                _eventBus.TriggerEvenet<StartDialogueSignal, string>("granny_d3");
                //start win dialogue
                return;
            }
            
            _eventBus.TriggerEvenet<StartDialogueSignal, string>("granny_d1");
        }

        private void OnFlowersPlanted()
        {
            _questIsDone = true;
            _eventBus.TriggerEvenet<DialogueEventSignal, string>("a_Granny_body/end_0_1");
            _eventBus.TriggerEvenet<DialogueEventSignal, string>("a_Granny_head/flower_2_0");
        }

        private void OnItemGive(string message)
        {
            if (message == "granny_complete")
            {
                _eventBus.TriggerEvenet<AddItemSignal, ItemId>(ItemId.RedTriangle);
                _itemGived = true;
            }
        }
    }
}