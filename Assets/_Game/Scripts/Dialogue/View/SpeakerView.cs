using _Game.Scripts.PlayerSystems.Animations.Impl;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem.View
{
    public class SpeakerView : MonoBehaviour
    {
        [field: SerializeField] public ECharacters Id { get; private set; }

        [SerializeField] private SpeakWindowView _speakWindowView;
        [SerializeField] private TMP_Text _alphaText;
        [SerializeField] private TMP_Text _realText;

        /// <summary>
        /// Используется для расширения диалогового окна
        /// </summary>
        /// <param name="dialogueText"></param>
        public void SetFakeText(string dialogueText)
        {
            //_alphaText.text = dialogueText;
        }
        
        public void SetDialogue(string dialogueText)
        {
            _realText.text = dialogueText;
        }
        
        public void HideDialogueWindow()
        {
            _speakWindowView.Hide();
        }

        public void ShowDialogueWindow()
        {
            _speakWindowView.Show();
        }
    }
}