using System;
using System.Collections.Generic;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.PlantsQuest.Impl.Plant;
using Core.Common;
using UniRx;
using UnityEngine;

namespace _Game.Scripts.Quests.PlantsQuest
{
    public class PlantsCutscene : ACutscene, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable;
        private readonly CameraController _cameraController;
        private readonly NightstandView _grannyView;
        private readonly NightstandView _plantView;
        private readonly PlayerView _playerView;
        private readonly EventBus _eventBus;
        private readonly List<Plant> _plantsList;
        
        
        public PlantsCutscene(
            CameraController cameraController, 
            NightstandView plantView, 
            NightstandView grannyView, 
            EventBus eventBus,
            PlayerView playerView,
            List<Plant> plants)
        {
            _eventBus            = eventBus;
            _cameraController    = cameraController;
            _grannyView          = grannyView;
            _plantView           = plantView;
            _playerView          = playerView;
            _plantsList          = plants;
            _compositeDisposable = new CompositeDisposable();
        }
        
        
        public override void Play(Action onComplete)
        {
            _cameraController.SetFollowTarget(_plantView.transform);
            
            _cameraController.ZoomTo(4f, 3f, () =>
            {
                foreach (var plant in _plantsList)
                {
                    PlantModel model = (PlantModel)plant.AbstractInteractableModel;
                    model.NeedCallback.Value = false;
                    model.CanInteract.Value = false;
                    model.ColliderIsEnabled.Value = false;
                    model.Height.Value = 5;
                }
                
                Observable.Return(Unit.Default)
                    .Delay(TimeSpan.FromSeconds(3f))
                    .Do(_ =>_cameraController.ZoomTo(5f, 1.5f, null))
                    .Do(_ => _cameraController.SetFollowTarget(_grannyView.transform))
                    .Delay(TimeSpan.FromSeconds(2f))
                    .Do(_ => _eventBus.TriggerEvenet<OnFlowersHeightRightSignal>())
                    .Delay(TimeSpan.FromSeconds(3))
                    .Subscribe(_ =>
                    {
                        _cameraController.SetFollowTarget(_playerView.transform);
                        onComplete?.Invoke();
                    })
                    .AddTo(_compositeDisposable);
            });
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}