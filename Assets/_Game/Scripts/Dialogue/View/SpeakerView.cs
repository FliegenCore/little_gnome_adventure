using _Game.Scripts.PlayerSystems.Animations.Impl;
using Core.Common;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.DialogueSystem.View
{
    public class SpeakerView : MonoBehaviour
    {
        [field: SerializeField] public ECharacters Id { get; private set; }

        [SerializeField] private SpeakWindowView _speakWindowView;
        [SerializeField] private TMP_Text _realText;
        [SerializeField] private SpeakAnimation _speakAnimation;
        
        private EventBus _eventBus;

        private bool _isInitialized;
        
        public void Initialize(EventBus eventBus)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            _eventBus = eventBus;
            _speakAnimation.Construct(_eventBus, Id.ToString());
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