using System;
using System.Collections.Generic;
using _Game.Scripts.DialogueSystem.View;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.DialogueSystem
{
    public class NonSkipDialogueHandler : IDisposable
    {
        private readonly DialogueWriter _dialogueWriter;
        private readonly EventBus _eventBus;
        private readonly DialogueManager _dialogueManager;
        
        private CompositeDisposable _writeDisposable;
        private IReadOnlyList<DialogueData> _allDatas;
        private DialogueData _currentDialogue;
        private bool _dialogueIsStart;
        private SpeakersProvider _speakersProvider;
        private SpeakerView _currentSpeakerView;

        private string _currentDialogueText;
        
        public NonSkipDialogueHandler(
            EventBus eventBus,
            SpeakersProvider speakersProvider,
            DialogueManager dialogueManager
            )
        {
            _eventBus         = eventBus;
            _speakersProvider = speakersProvider;
            _dialogueManager = dialogueManager;
            _dialogueWriter   = new DialogueWriter();
            Init();
        }

        private void Init()
        {
            _dialogueWriter.OnLetterWrited += _dialogueManager.PlayWriteAudio;
        }
        
        public void StartDialogue(IReadOnlyList<DialogueData> dates, DialogueData currentDialogue)
        {
            _allDatas = dates;
            _dialogueIsStart = true;
            _currentDialogue = currentDialogue;

            ShowCurrentDialogue();
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

            _currentSpeakerView = _speakersProvider.GetSpeakerCharacter(_currentDialogue.Name);
            
            if (_currentSpeakerView == null)
                return;
            
            string dialogueText = _currentDialogue.Text; //todo: получить перевод 
            _currentDialogueText = dialogueText;
            _writeDisposable = new CompositeDisposable();
            
            _dialogueWriter.SetCurrentText(dialogueText, _currentSpeakerView);
            
            Observable.FromCoroutine(() => _dialogueWriter.WriteDialogue(ContinueDialogue))
                .Subscribe()
                .AddTo(_writeDisposable);
        }
        
        private void ContinueDialogue()
        {
            float waitTime = _currentDialogueText.Length * 0.1f;
            
            Observable.Timer(TimeSpan.FromSeconds(waitTime)).Subscribe(_ =>
            {
                if (TrySelectNextDialogue())
                {
                    ShowCurrentDialogue();
                }
                else
                {
                    EndDialogue();
                }
            });
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

        private void EndDialogue()
        {
            _dialogueIsStart = false;
            
            if (_currentSpeakerView != null)
            {
                _currentSpeakerView.SetDialogue(string.Empty);
                _currentSpeakerView.SetFakeDialogue(string.Empty);
                _currentSpeakerView.HideDialogueWindow();
            }
            
            if (_currentDialogue.OnEndEvents.Count > 0)
            {
                foreach (var endEventName in _currentDialogue.OnEndEvents)
                {
                    _eventBus.TriggerEvenet<DialogueEventSignal, string>(endEventName);
                }
            }
        }

        public void Dispose()
        {
            _dialogueManager?.Dispose();
            _writeDisposable?.Dispose();
            _dialogueWriter.OnLetterWrited -= _dialogueManager.PlayWriteAudio;
        }
    }
}