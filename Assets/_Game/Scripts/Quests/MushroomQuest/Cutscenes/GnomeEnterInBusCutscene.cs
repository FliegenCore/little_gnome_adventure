using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.MotionStates;
using _Game.Scripts.PlayerSystems.PlayerStates;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using _Game.Scripts.Sound;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.MushroomQuest.Cutscenes
{
    public class GnomeEnterInBusCutscene : ACutscene
    {
        private readonly EventBus _eventBus;
        private readonly IPlayerFactory _playerFactory;
        private readonly Transform _busFollowPoint;
        private readonly Fsm _busMachine;
        private readonly CameraController _cameraController;
        private readonly ISoundManager _soundManager;
        
        public GnomeEnterInBusCutscene(
            EventBus eventBus, 
            IPlayerFactory playerFactory, 
            Transform busFollowPoint,
            Fsm busMachine,
            CameraController cameraController,
            ISoundManager soundManager
            )
        {
            _soundManager     = soundManager;
            _cameraController = cameraController;
            _eventBus         = eventBus;
            _playerFactory    = playerFactory;
            _busFollowPoint   = busFollowPoint;
            _busMachine       = busMachine;
        }

        public override void Play(Action _)
        {
            Player player = _playerFactory.GetPlayer();
            
            _eventBus.TriggerEvenet<SetPlayerMotionStateSignal, Type>(typeof(PlayerIdleMotionState));
            _eventBus.TriggerEvenet<SetPlayerStateSignal, Type>(typeof(PlayerDisabledMotionState));
            
            Observable.Timer(TimeSpan.FromSeconds(0.1f))
                .Subscribe(_ =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(0.1f))
                        .Subscribe(_ =>
                        {
                            _soundManager.PlayOnPosition(_busFollowPoint, 20, "BusJump", false);
                        });
                    
                    player.PlayerView.AnimationPlayer.AnimationControl.SetAnimation(0, "body/come_in_bus", false, OnPlayerJumpEnd);
                });
        }

        private void OnPlayerJumpEnd()
        {
            Player player = _playerFactory.GetPlayer();
            PlayerModel playerModel = player.PlayerModel;
            _cameraController.SetFollowTarget(_busFollowPoint);
            _cameraController.ZoomTo(7, 0.25f, null);
            playerModel.IsActive.Value = false;
            
            _busMachine.SetState<BusmanGnomeEnterState>();
        }
    }
}