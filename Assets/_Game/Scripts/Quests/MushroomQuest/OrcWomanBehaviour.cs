using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.InventorySystem;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.Quests.MushroomQuest.Cutscenes;
using Core.Common;

namespace _Game.Scripts.Quests.MushroomQuest
{
    public class OrcWomanBehaviour : ACustomBehaviour, IItemNeeder
    {
        private readonly AnimationControl _animationControl;
        private readonly OpenBusByTicketCutscene _openBusByTicketCutscene;
        private readonly ICutsceneManager _cutsceneManager;
        
        private bool _canInteract = true;
        private bool _isComplete = false;
        
        public OrcWomanBehaviour(
            EventBus eventBus,
            AnimationControl animationControl,
            OpenBusByTicketCutscene openBusByTicketCutscene,
            ICutsceneManager cutsceneManager
            ) : base(eventBus)
        {
            _cutsceneManager         = cutsceneManager;
            _openBusByTicketCutscene = openBusByTicketCutscene;
            _animationControl        = animationControl;
        }

        public override bool CanInteract()
        {
            if (_isComplete)
                return false;
            
            return _canInteract;
        }

        public override void Interact(Action callback)
        {
            _animationControl.SetAnimation(0, "no", false, SetIdleAnimation);
            _canInteract = false;
            callback?.Invoke();
        }

        public void InteractWithItem(InventoryItem item, Action callback)
        {
            callback?.Invoke();

            if (item.ItemId != ItemId.Ticket)
            {
                _animationControl.SetAnimation(0, "no", false, SetIdleAnimation);
                return;
            }

            _eventBus.TriggerEvenet<RemoveItemSignal, InventoryItem>(item);
            _cutsceneManager.Play(_openBusByTicketCutscene);
            _isComplete = true;
            _canInteract = false;
        }

        private void SetIdleAnimation()
        {
            _animationControl.SetAnimation(0, "idle");
            _canInteract = true;
        }
    }
}