using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.HintsSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class StartCutscene : ACutscene, ISkipableCutscene
    {
        private const string DIALOGUE_NAME = "GnomeThoughts_1";
        
        private readonly EventBus _eventBus;
        private readonly Transform _playerMovePoint;
        private readonly IPlayerFactory _playerFactory; 
        private readonly DialogueModel _dialogueModel; 
        
        private CompositeDisposable _disposables;
        
        public StartCutscene(
            EventBus eventBus, 
            IPlayerFactory playerFactory,
            Transform playerMovePoint,
            DialogueModel dialogueModel
            )
        {
            _dialogueModel = dialogueModel;
            _eventBus = eventBus;
            _playerFactory = playerFactory;
            _playerMovePoint = playerMovePoint;
            _disposables = new CompositeDisposable();
        }
        
        public override void Play(Action onComplete)
        {
            Player player = _playerFactory.GetPlayer();
            player.PlayerModel.AutoMoveTransform = _playerMovePoint;
            
            _dialogueModel.OnDialogueEnd
                .Subscribe(dialogueName => OnDialogueEnd(dialogueName, onComplete))
                .AddTo(_disposables);

            Observable.Timer(TimeSpan.FromSeconds(2.5f)).Subscribe(_ =>
            {
                _eventBus.TriggerEvenet<StartNonSkipDialogueSignal, string>(DIALOGUE_NAME);
            });

            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerNoneState));
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerAutoMoveMotionState));
            
            player.PlayerModel.OnPosition.Subscribe(_ =>
            {
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            }).AddTo(_disposables);
        }

        private void OnDialogueEnd(string dialogueName, Action callback)
        {
            if (dialogueName != DIALOGUE_NAME)
                return;
            
            callback?.Invoke();
            _eventBus.TriggerEvenet<ShowGameHintSignal, EHintType>(EHintType.MoveHint);
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerBaseState));
            
            _disposables?.Dispose();
        }

        public void Skip()
        {
            _disposables?.Dispose();
            _playerFactory.GetPlayer().PlayerModel.Transformation.Position.Value = _playerMovePoint.position;
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
        }
    }
}