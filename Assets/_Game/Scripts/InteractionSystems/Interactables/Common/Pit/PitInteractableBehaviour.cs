using System;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.Quests.StartGameQuest;
using Core.Common;

namespace _Game.Scripts.InteractionSystems.Interactables.Common
{
    public class PitInteractableBehaviour : ACustomBehaviour
    {
        private readonly PitCutscene _pitCutscene;
        
        public PitInteractableBehaviour(EventBus eventBus, PitCutscene pitCutscene) : base(eventBus)
        {
            _pitCutscene = pitCutscene;
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact(Action callback)
        {
            _pitCutscene.Play(callback);
        }
    }
}