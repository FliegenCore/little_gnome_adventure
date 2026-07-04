using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
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
        private readonly EventBus _eventBus;
        private readonly Transform _playerMovePoint;
        private readonly IPlayerFactory _playerFactory; 
        
        private CompositeDisposable _disposables;
        
        public StartCutscene(EventBus eventBus, IPlayerFactory playerFactory, Transform playerMovePoint)
        {
            _eventBus = eventBus;
            _playerFactory = playerFactory;
            _playerMovePoint = playerMovePoint;
        }
        
        public override void Play(Action onComplete)
        {
            _disposables = new CompositeDisposable();
            Player player = _playerFactory.GetPlayer();
            player.PlayerModel.AutoMoveTransform = _playerMovePoint;
            
            _eventBus.TriggerEvenet<StartNonSkipDialogueSignal, string>("GnomeThoughts_1");
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerNoneState));
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerAutoMoveMotionState));
            
            player.PlayerModel.OnPosition.Subscribe(_ =>
            {
                _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
                onComplete?.Invoke();   
            }).AddTo(_disposables);
        }

        public void Skip()
        {
            _disposables?.Dispose();
            _playerFactory.GetPlayer().PlayerModel.Transformation.Position.Value = _playerMovePoint.position;
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
        }
    }
}