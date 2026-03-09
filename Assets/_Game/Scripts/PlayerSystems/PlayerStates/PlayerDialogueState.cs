using _Game.Scripts.DialogueSystem;
using _Game.Scripts.FSM;

namespace _Game.Scripts.PlayerSystems.PlayerStates
{
    public class PlayerDialogueState : PlayerState
    {
        private readonly IDialogueManager _dialogueManager;
        
        public PlayerDialogueState(Fsm fsm, PlayerModel playerModel, IDialogueManager dialogueManager) : base(fsm, playerModel)
        {
            _dialogueManager = dialogueManager;
        }

        public override void Enter()
        {
            base.Enter();
            _dialogueManager.EnableInput();
        }
        
        public override void Exit()
        {
            base.Exit();
            _dialogueManager.DisableInput();
        }
    }
}