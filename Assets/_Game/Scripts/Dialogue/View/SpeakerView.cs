using _Game.Scripts.PlayerSystems.Animations.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using Core.Common;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem.View
{
    [RequireComponent(typeof(SpeakAnimation))]
    public class SpeakerView : MonoBehaviour
    {
        [field: SerializeField] public ECharacters Id { get; private set; }

        [SerializeField] private SpeakWindowView _speakWindowView;
        [SerializeField] private SpeakAnimation _speakAnimation;
        
        private EventBus _eventBus;
        
        private TMP_Text _realText;
        private TMP_Text _fakeText;

        private bool _isInitialized;

        public void Initialize(EventBus eventBus)
        {
            if (_isInitialized)
                return;
            
            _realText = _speakWindowView.RealText;
            _fakeText = _speakWindowView.FakeText;
                
            _isInitialized = true;
            _eventBus = eventBus;
            _speakAnimation.Construct(_eventBus, Id.ToString());
        }

        public void SetFakeDialogue(string dialogueText)
        {
            _speakWindowView.FakeText.text = dialogueText;
            Canvas.ForceUpdateCanvases();
            SetRealTextTransform();
        }
        
        public void SetDialogue(string dialogueText)
        {
            _speakWindowView.RealText.text = dialogueText;
        }

        private void SetRealTextTransform()
        {
            _realText.rectTransform.sizeDelta = _fakeText.rectTransform.sizeDelta;
            _realText.transform.localScale = _fakeText.transform.localScale;
            _realText.rectTransform.anchoredPosition = _fakeText.rectTransform.anchoredPosition;
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