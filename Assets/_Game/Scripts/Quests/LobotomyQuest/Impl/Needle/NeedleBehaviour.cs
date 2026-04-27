using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using Core.Common;

namespace _Game.Scripts.Quests.LobotomyQuest.Impl.Needle
{
    public class NeedleBehaviour : ACustomBehaviour
    {
        public NeedleBehaviour(EventBus eventBus) : base(eventBus)
        {
            
        }

        public override bool CanInteract()
        {
            return true;
        }

        public override void Interact()
        {
            
        }
    }
}