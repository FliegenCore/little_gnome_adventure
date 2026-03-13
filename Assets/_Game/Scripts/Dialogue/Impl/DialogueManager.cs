using System;
using System.Collections.Generic;
using _Game.Scripts.DialogueSystem.View;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace _Game.Scripts.DialogueSystem
{
    public class DialogueManager : IDialogueManager, IDisposable
    {
        private readonly DialogueProvider _dialogueProvider;
        private readonly InputSystem_Actions _inputSystemActions;
        private readonly EventBus _eventBus;
        
        private List<DialogueData> _allDialogues;
        private DialogueData _currentDialogue;
        private List<SpeakerView> _speakerViews = new();
        private SpeakerView _currentSpeakerView;
        
        private bool _dialogueIsStarted;

        private DialogueManager(InputSystem_Actions inputSystemActions, EventBus eventBus)
        {
            _eventBus           = eventBus;
            _inputSystemActions = inputSystemActions;
            _dialogueProvider   = new DialogueProvider();

            Init();
        }

        private void Init()
        {
            _eventBus.Subscribe<StartDialogueSignal, string>(this, StartDialogue);
        }

        public void EnableInput()
        {
            _inputSystemActions.Player.Interact.performed += ContinueDialogue;
        }

        public void DisableInput()
        {
            _inputSystemActions.Player.Interact.performed -= ContinueDialogue;
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

        private void StartDialogue(string dialogueName)
        {
            if (_dialogueIsStarted)
                return;

            _dialogueIsStarted = true;
            _allDialogues    = _dialogueProvider.GetDialogue(dialogueName);
            _currentDialogue = _dialogueProvider.GetStartDialogueData(_allDialogues);

            if (_currentDialogue != null)
            {
                ShowCurrentDialogue();
                
                _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDialogueState));
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            }
            else 
                Debug.LogError($"{nameof(DialogueManager)} has not dialogue by name{dialogueName}");
        }

        private void ShowCurrentDialogue()
        {
            if (_currentSpeakerView != null)
            {
                _currentSpeakerView.SetDialogue(string.Empty);
                _currentSpeakerView.HideDialogueWindow();
            }
            
            _currentSpeakerView = GetSpeakerCharacter(_currentDialogue.Name);
            
            if (_currentSpeakerView == null)
                return;
            
            string dialogueText = _currentDialogue.Text; //todo: получить перевод 
            _currentSpeakerView.SetDialogue(dialogueText);
            
            _currentSpeakerView.ShowDialogueWindow();
        }
        
        private void ContinueDialogue(InputAction.CallbackContext _)
        {
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