using System;
using System.Collections;
using System.Text;
using _Game.Scripts.DialogueSystem.View;
using Core.Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueWriter
    {
        public event Action OnLetterWrited; //for audio and more

        public event Action OnDialogueStart;
        public event Action OnDialogueEnd;
        
        private DialogueData _currentDialogue;
        private SpeakerView _currentSpeakerView;
        private string _currentWriteText;

        public void SetCurrentText(string text, SpeakerView currentSpeaker)
        {
            _currentWriteText = text;
            _currentSpeakerView = currentSpeaker;
            _currentSpeakerView.ShowDialogueWindow();
            _currentSpeakerView.SetFakeDialogue(text);
        }
        
        public IEnumerator WriteDialogue(Action onComplete)
        {
            var sb = new StringBuilder();
            char[] letters = _currentWriteText.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                sb.Append(letters[i]);
                _currentSpeakerView.SetDialogue(sb.ToString());
                OnLetterWrited?.Invoke();
                yield return new WaitForSeconds(0.05f);
            }
            
            onComplete?.Invoke();
        }
    }
}