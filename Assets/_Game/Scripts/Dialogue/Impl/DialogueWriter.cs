using System;
using System.Collections;
using System.Text;
using _Game.Scripts.DialogueSystem.View;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueWriter
    {
        private string _currentWriteText;
        private SpeakerView _currentSpeakerView;

        public event Action OnLetterWrited; //for audio and more
        
        public void SetCurrentText(string text, SpeakerView currentSpeaker)
        {
            _currentWriteText = text;
            _currentSpeakerView = currentSpeaker;
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