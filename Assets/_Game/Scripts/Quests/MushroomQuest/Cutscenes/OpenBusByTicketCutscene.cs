using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.FSM;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations;
using _Game.Scripts.Quests.MushroomQuest.Busman.States;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.MushroomQuest.Cutscenes
{
    public class OpenBusByTicketCutscene : ACutscene
    {
        private readonly CameraController _cameraController;
        private readonly CameraControllerHelper _cameraControllerHelper;
        private readonly Fsm _busmanStateMachine;
        private readonly AnimationControl _orcWomanAnimationControl;
        private readonly Transform _busmanFollowPoint;
        
        public OpenBusByTicketCutscene(
            Fsm busmanStateMachine,
            CameraController cameraController, 
            CameraControllerHelper cameraControllerHelper, 
            AnimationControl orcWomanAnimationControl,
            Transform busmanFollowPoint
        )
        {
            _cameraControllerHelper   = cameraControllerHelper;
            _busmanFollowPoint        = busmanFollowPoint;
            _cameraController         = cameraController;
            _busmanStateMachine       = busmanStateMachine;
            _orcWomanAnimationControl = orcWomanAnimationControl;
        }
        
        public override void Play(Action onComplete)
        {
            _orcWomanAnimationControl.SetAnimation(0, "yes", false, () => ShowBus(onComplete));
        }

        private void ShowBus(Action onComplete)
        {
            _cameraController.SetFollowSpeed(0.75f);
            _cameraController.SetFollowTarget(_busmanFollowPoint);
            Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ =>
                OpenBus(onComplete)
                );
        }

        private void OpenBus(Action onComplete)
        {
            _busmanStateMachine.SetState<BusmanDoorOpenState>(() => OnDoorOpen(onComplete));
        }

        private void OnDoorOpen(Action onComplete)
        {
            _cameraControllerHelper.SetFollowPlayer();
            
            Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
                onComplete?.Invoke()
            );
        }
    }
}