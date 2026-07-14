using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.InspectSystem;
using _Game.Scripts.PlayerSystems.InspectSystem.ViewVariants;
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
        private readonly InspectAnimationView _busmanJumpAnimationInspect;
        
        public GnomeEnterInBusCutscene(
            EventBus eventBus, 
            IPlayerFactory playerFactory, 
            Transform busFollowPoint,
            Fsm busMachine,
            CameraController cameraController,
            ISoundManager soundManager,
            InspectAnimationView busmanJumpAnimationInspect
            )
        {
            _busmanJumpAnimationInspect = busmanJumpAnimationInspect;
            _soundManager               = soundManager;
            _cameraController           = cameraController;
            _eventBus                   = eventBus;
            _playerFactory              = playerFactory;
            _busFollowPoint             = busFollowPoint;
            _busMachine                 = busMachine;
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
            
            _busMachine.SetState<BusmanGnomeEnterState>(OnBusmanLeft);
        }

        private void OnBusmanLeft()
        {
            _busmanJumpAnimationInspect.AnimationControl.SetAnimation(0, "2", callback: OnBusmanFlyAnimationEnd);
            _eventBus.TriggerEvenet<ShowInspectWindowByIdSignal, string>(MushroomQuestManager.BUSMAN_JUMP_INSPECT_ANIMATION);
        }

        private void OnBusmanFlyAnimationEnd()
        {
            //_eventBus.TriggerEvenet<HideCurrentInspectWindowSignal>(); //TODO: переключить руму
        }
    }
}