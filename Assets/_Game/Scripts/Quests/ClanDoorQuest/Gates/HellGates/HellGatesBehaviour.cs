using System;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.PlayerSystems.InspectSystem;
using Core.Common;

namespace _Game.Scripts.Quests.ClanDoorQuest.Gates
{
    public class HellGatesBehaviour : ACustomBehaviour
    {
        private readonly HellGatesModel _hellGatesModel;
        
        public HellGatesBehaviour(
            EventBus eventBus,
            HellGatesModel hellGatesModel
        ) : base(eventBus)
        {
            _hellGatesModel = hellGatesModel;
        }

        public override bool CanInteract()
        {
            return _hellGatesModel.CanInteract;
        }

        public override void Interact(Action callback)
        {
            callback?.Invoke();
            
            _eventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>("GatesPassword");
        }
    }
}