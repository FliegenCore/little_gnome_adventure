using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using _Game.Scripts.Sound;
using Core.Common;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueManager : IDialogueManager, IDisposable
    {
        private const string AUDIO_CLIP_NAME = "TypeEffect";

        private const int MIN_AUDIO_INDEX = 0;
        private const int MAX_AUDIO_INDEX = 7;
        
        private readonly DialogueProvider _dialogueProvider;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        private readonly ISoundManager _soundManager;
        
        private CompositeDisposable _writeDisposable;
        private List<DialogueData> _allDialogues;
        private DialogueData _currentDialogue;
        private List<SpeakerView> _speakerViews = new();
        private SpeakerView _currentSpeakerView;
        
        private bool _dialogueIsStarted;
        private bool _dialogueIsWriteEnd;
        
        private string _currentDialogueText;

        private bool _canContinueDialogue = true;

        private DialogueManager(
            InputSystem_Actions inputSystemActions,
            EventBus eventBus,
            ISoundManager soundManager
            )
        {
            _soundManager       = soundManager;
            _eventBus           = eventBus;
            _inputSystemActions = inputSystemActions;
            _dialogueProvider   = new DialogueProvider();

            Init();
        }

        private void Init()
        {
            _eventBus.Subscribe<StartDialogueSignal, string>(this, StartDialogue);
            _eventBus.Subscribe<DialogueEventSignal, string>(this, HandleDialogueEvent);
        }

        public void EnableInput()
        {
            _inputSystemActions.Player.Interact.performed += ContinueDialogue;
        }

        public void DisableInput()
        {
            _inputSystemActions.Player.Interact.performed -= ContinueDialogue;
            _canContinueDialogue = true;
        }

        public void RegisterSpeakerCharacters(params SpeakerView[] speakerViews)
        {
            foreach (var speaker in speakerViews)
            {
                _speakerViews.Add(speaker);
            }
        }

        public void UnregisterSpeakerCharacters(List<SpeakerView> speakerViews)
        {
            foreach (var speaker in speakerViews)
            {
                _speakerViews.Remove(speaker);
            }
        }

        private void HandleDialogueEvent(string eventName)
        {
            //TODO: доавбить визуальное отображение что нельзя продолжить, например чтобы E пропадала!!!!
            if (eventName == "dialogue_skip_disable")
            {
                _canContinueDialogue = false;
            }
            if (eventName == "dialogue_skip_enable")
            {
                _canContinueDialogue = true;
            }
        }

        private void StartDialogue(string dialogueName)
        {
            if (_dialogueIsStarted)
                return;

            _dialogueIsStarted = true;
            _allDialogues      = _dialogueProvider.GetDialogue(dialogueName);
            _currentDialogue   = _dialogueProvider.GetStartDialogueData(_allDialogues);

            if (_currentDialogue != null)
            {
                ShowCurrentDialogue();
                
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDialogueState));
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            }
            else 
                Debug.LogError($"{nameof(DialogueManager)} has not dialogue by name {dialogueName}");
        }

        
        private void ShowCurrentDialogue()
        {
            if (_currentDialogue.OnStartEvents.Count > 0)
            {
                foreach (var startEventName in _currentDialogue.OnStartEvents)
                {
                    _eventBus.TriggerEvenet<DialogueEventSignal, string>(startEventName);
                }
            }
            
            if (_currentSpeakerView != null)
            {
                _currentSpeakerView.SetDialogue(string.Empty);
                _currentSpeakerView.SetFakeDialogue(string.Empty);
                _currentSpeakerView.HideDialogueWindow();
            }

            _currentSpeakerView = GetSpeakerCharacter(_currentDialogue.Name);
            
            if (_currentSpeakerView == null)
                return;
            
            _dialogueIsWriteEnd = false;
            string dialogueText = _currentDialogue.Text; //todo: получить перевод 
            
            _currentDialogueText = dialogueText;

            _writeDisposable = new CompositeDisposable();
            
            _currentSpeakerView.ShowDialogueWindow();
            
            _currentSpeakerView.SetFakeDialogue(dialogueText);
            
            Observable.FromCoroutine(WriteDialogue)
                .Subscribe()
                .AddTo(_writeDisposable);
        }

        private IEnumerator WriteDialogue()
        {
            var sb = new StringBuilder();
            char[] letters = _currentDialogueText.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                PlayWriteAudio();
                sb.Append(letters[i]);
                _currentSpeakerView.SetDialogue(sb.ToString());
                yield return new WaitForSeconds(0.05f);
            }

            SkipWrite();
        }

        private void PlayWriteAudio()
        {
            int index = UnityEngine.Random.Range(MIN_AUDIO_INDEX, MAX_AUDIO_INDEX);

            string audioName = $"{AUDIO_CLIP_NAME}{index}";

            _soundManager.PlayEffectOnBackground(null, audioName, false);
        }

        private void SkipWrite()
        {
            _writeDisposable?.Dispose();
            _dialogueIsWriteEnd = true;
            _currentSpeakerView.SetDialogue(_currentDialogueText);
        }
        
        private void ContinueDialogue(InputAction.CallbackContext _)
        {
            if (!_dialogueIsWriteEnd)
            {
                SkipWrite();

                return;
            }
            
            if (!_canContinueDialogue)
                return;
            
            if (TrySelectNextDialogue())
            {
                ShowCurrentDialogue();
            }
            else
            {
                StopDialogue();
            }
        }

        private void StopDialogue()
        {
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
            
            _currentSpeakerView.SetDialogue(string.Empty);
            _currentSpeakerView.SetFakeDialogue(string.Empty);
            _currentSpeakerView.HideDialogueWindow();
            _dialogueIsStarted = false;
            
            if (_currentDialogue.OnEndEvents.Count > 0)
            {
                foreach (var endEventName in _currentDialogue.OnEndEvents)
                {
                    _eventBus.TriggerEvenet<DialogueEventSignal, string>(endEventName);
                }
            }
        }

        private bool TrySelectNextDialogue()
        {
            if (_currentDialogue.NextDialogue == null)
            {
                return false;
            }
            
            var nextDialogue = _currentDialogue.NextDialogue;
            
            if (_currentDialogue.OnEndEvents.Count > 0)
            {
                foreach (var endEventName in _currentDialogue.OnEndEvents)
                {
                    _eventBus.TriggerEvenet<DialogueEventSignal, string>(endEventName);
                }
            }
            
            _currentDialogue = nextDialogue;
            
            return true;
        }

        private SpeakerView GetSpeakerCharacter(string speakerName)
        {
            foreach (var speakerView in _speakerViews)
            {
                if (speakerView.Id.ToString() == speakerName)
                    return speakerView;
            }

            Debug.LogError($"{nameof(DialogueManager)} has not speaker by name {speakerName}");
            return null;
        }

        public void Dispose()
        {
            _inputSystemActions.Player.Interact.performed -= ContinueDialogue;
            _eventBus.Unsubscribe<StartDialogueSignal>(this);
        }
    }
}