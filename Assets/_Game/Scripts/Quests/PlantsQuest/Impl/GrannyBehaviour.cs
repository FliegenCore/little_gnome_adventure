using _Game.Scripts.DialogueSystem;
using Core.Common;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl
{
    public class GrannyBehaviour : ACustomBehaviour
    {
        private bool _itemGived;
        private bool _questIsDone;
        
        public GrannyBehaviour(EventBus eventBus) : base(eventBus)
        {
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact()
        {
            if (_itemGived)
            {
                //диалог о том что тут всё окончено
                return;
            }
            
            if (_questIsDone)
            {
                //start win dialogue
                return;
            }
        }

        private void OnFlowersPlanted()
        {
            _questIsDone = true;
            _eventBus.TriggerEvenet<DialogueEventSignal, string>("a_Granny_name_1_0");
        }

        private void OnItemGive(string message)
        {
            if (message == "granny_complete")
            {
                
            }
        }
    }
}