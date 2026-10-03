using System;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.DialogueSystem;
using _Game.Scripts.HintsSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using _Game.Scripts.Quests.StartGameQuest.Rabbit;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;
using Core.Common;
using Spine;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest
{
    public class StartCutscene : ACutscene, ISkipableCutscene
    {
        private const string DIALOGUE_NAME = "GnomeThoughts_1";
        
        private readonly EventBus _eventBus;
        private readonly Transform _playerMoveEndPoint;
        private readonly Transform _rabbitFirstMovePoint;
        private readonly IPlayerFactory _playerFactory; 
        private readonly DialogueModel _dialogueModel; 
        private readonly RabbitFactory _rabbitFactory; 
        
        private CompositeDisposable _disposables;
        
        public StartCutscene(
            EventBus eventBus, 
            IPlayerFactory playerFactory,
            Transform playerMoveEndPoint,
            Transform rabbitMovePoint,
            DialogueModel dialogueModel,
            RabbitFactory rabbitFactory
            )
        {
            _rabbitFactory        = rabbitFactory;
            _dialogueModel        = dialogueModel;
            _eventBus             = eventBus;
            _playerFactory        = playerFactory;
            _playerMoveEndPoint   = playerMoveEndPoint;
            _rabbitFirstMovePoint = rabbitMovePoint;
            _disposables = new CompositeDisposable();
        }
        
        public override void Play(Action onComplete)
        {
            Player player = _playerFactory.GetPlayer();
            player.PlayerModel.AutoMoveTransform = _playerMoveEndPoint;
            Rabbit.Rabbit rabbit = _rabbitFactory.CachedRabbit;

            rabbit.RabbitModel.AutoMovePoint = _rabbitFirstMovePoint;
            
            _dialogueModel.OnDialogueEnd
                .Subscribe(dialogueName => OnDialogueEnd(dialogueName, onComplete))
                .AddTo(_disposables);
            
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerNoneState));
            player.SetPlayerMotionState(typeof(PlayerAutoMoveMotionState), 2.5f);
            
            player.PlayerView.AnimationPlayer.AnimationControl.SetAnimation(0, PlayerAnimationsName.CROUCH_WALK_ANIMATION_NAME);
            
            player.PlayerModel.OnPosition.Subscribe(_ =>
            {
                player.SetPlayerMotionState(typeof(PlayerEmptyMotionState));
                JumpAnimation(player);
            }).AddTo(_disposables);
        }

        private void JumpAnimation(Player player)
        {
            player.PlayerView.AnimationPlayer.AnimationControl.SubscribeOnEvents(HandleEvent);
            
            player.PlayerView.AnimationPlayer.AnimationControl.SetAnimation(0, PlayerAnimationsName.CATCH_RABBIT_ANIMATION_NAME666, false,
                () =>
                {
                    player.SetPlayerMotionState(typeof(PlayerIdleMotionState));
                    _eventBus.TriggerEvenet<StartNonSkipDialogueSignal, string>("GnomeThoughts_1");
                } );
        }

        private void HandleEvent(TrackEntry trackEntry, Spine.Event e) 
        {
            if (e.Data.Name != "event_rabbit_run")
            {
                return;
            }
            
            Player player = _playerFactory.GetPlayer();
            player.PlayerView.AnimationPlayer.AnimationControl.UnsubscribeOnEvents(HandleEvent);
            Rabbit.Rabbit rabbit = _rabbitFactory.CachedRabbit;
            rabbit.StateMachine.SetState<RabbitAutoWalkState>();
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
            _playerFactory.GetPlayer().PlayerModel.Transformation.Position.Value = _playerMoveEndPoint.position;
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
        }
    }
}