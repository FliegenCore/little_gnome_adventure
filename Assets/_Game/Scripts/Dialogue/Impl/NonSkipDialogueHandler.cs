using System.Collections.Generic;

namespace _Game.Scripts.DialogueSystem
{
    public class NonSkipDialogueHandler
    {
        private readonly DialogueProvider _dialogueProvider;
        
        private IReadOnlyList<DialogueData> _allDatas;
        private DialogueData _currentDialogue;        
        private bool _dialogueIsStart;
        

        public void StartDialogue(IReadOnlyList<DialogueData> dates, DialogueData currentDialogue)
        {
            _allDatas = dates;
            _dialogueIsStart = true;
            _currentDialogue = currentDialogue;

        }
        
        private void ContinueDialogue()
        {
            
        }

        private void EndDialogue()
        {
            _dialogueIsStart = false;
        }
    }
}