using System.Collections.Generic;
using _Game.Scripts.DialogueSystem.View;

namespace _Game.Scripts.DialogueSystem
{
    public interface IDialogueManager
    {
        void EnableInput();
        void DisableInput();
        void RegisterSpeakerCharacters(params SpeakerView[] speakerViews);
        void UnregisterSpeakerCharacters(List<SpeakerView> speakerViews);
    }
}