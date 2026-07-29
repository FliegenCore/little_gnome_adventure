using System;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Core.Common;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesBehaviour : ACustomBehaviour
    {
        public HellGatesBehaviour(EventBus eventBus) : base(eventBus)
        {
            
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            
            _eventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>("GatesPassword");
        }
    }
}