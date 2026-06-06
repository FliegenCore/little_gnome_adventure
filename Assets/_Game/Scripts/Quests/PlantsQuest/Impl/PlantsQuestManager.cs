using System;
using System.Collections.Generic;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.CutsceneSystem;
using _Game.Scripts.PlayerSystems.Animations.Factory;
using _Game.Scripts.PlayerSystems.InspectSystem.Interactable.View;
using _Game.Scripts.Quests.PlantsQuest;
using _Game.Scripts.Quests.PlantsQuest.Impl;
using _Game.Scripts.Quests.PlantsQuest.Impl.Plant;
using _Game.Scripts.RoomSystems;
using _Game.Scripts.RoomSystems.Impl.DreamQuestFirst;
using Core.Common;
using UniRx;

namespace _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours.Impl.Impl
{
    public class PlantsQuestManager : IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly LocationsRootView _locationsRootView;
        private readonly CameraController _cameraController;
        private readonly ICutsceneManger _cutsceneManger;
        private readonly IPlayerFactory _playerFactory;
        private readonly IInteractableFactory _interactableFactory;
        private PlantsCutscene _plantsCutscene; 
            
        private List<Interactable> _plants;

        
        public PlantsQuestManager(
            EventBus eventBus,
            LocationsRootView locationsRootView,
            CameraController cameraController,
            IPlayerFactory playerFactory,
            ICutsceneManger cutsceneManger,
            IInteractableFactory interactableFactory)
        {
            _interactableFactory     = interactableFactory;
            _cameraController        = cameraController;
            _locationsRootView = locationsRootView;
            _eventBus                = eventBus;
            _playerFactory           = playerFactory;
            _cutsceneManger          = cutsceneManger;
            _plants                  = new();
        }

        public void Initialize()
        {
            CreatePlants();

            _locationsRootView.DreamQuestFirstLocationView.FocusTrigger.OnFucused += OnFocus;
            _locationsRootView.DreamQuestFirstLocationView.FocusTrigger.OnUnfocused += OnUnfocus;
        }

        private void OnFocus()
        {
            foreach (var plants in _plants)
            {
                PlantView view = (PlantView)plants.InteractableView;
                
                view.EnablePoints();
            }
        }

        private void OnUnfocus()
        {
            foreach (var plants in _plants)
            {
                PlantView view = (PlantView)plants.InteractableView;
                
                view.DisablePoints();
            }
        }

        private void CreateCutscene()
        {
            DreamQuestFirstLocationView dreamQuestFirstLocationView =
                _locationsRootView.DreamQuestFirstLocationView;

            NightstandView grannyView = dreamQuestFirstLocationView.GrannyView;
            NightstandView centerPlantView = dreamQuestFirstLocationView.BootPlant;
            
            _plantsCutscene = new PlantsCutscene(
                _cameraController, 
                centerPlantView,
                grannyView, 
                _eventBus, 
                _playerFactory.GetPlayer().PlayerView, 
                _plants);
        }

        private void CreatePlants()
        {
            DreamQuestFirstLocationView dreamQuestFirstLocationView =
                _locationsRootView.DreamQuestFirstLocationView;
            
            Interactable boot = CreatePlant(dreamQuestFirstLocationView.BootPlant, "Boot", 2);
            Interactable cactus = CreatePlant(dreamQuestFirstLocationView.CactusPlant, "Cactus", 4);
            Interactable column = CreatePlant(dreamQuestFirstLocationView.ColumnPlant, "Column", 3);

            _plants.Add(boot);
            _plants.Add(cactus);
            _plants.Add(column);
        }

        private Interactable CreatePlant(PlantView plantView, string id, int neededHeight)
        {
            PlantModel plantModels = new PlantModel(plantView.ContactTriggerProvider, plantView.Position, id, neededHeight);
            
            Interactable interactable =
                _interactableFactory.CreateInteractable(new PlantBehaviour(_eventBus, plantModels), plantView,
                    plantModels);
            
            plantView.Construct(plantModels.Height, plantModels.CanInteract, plantModels.NeedCallback, plantModels.ColliderIsEnabled);
            plantView.HintSelect.Construct(_eventBus, plantModels.IsSelected);
            plantModels.Height.Subscribe(OnHeightChanged).AddTo(_compositeDisposable);
            return interactable;
        }

        private void OnHeightChanged(int _)
        {
            int rightHeight = 0;
            
            foreach (var plant in _plants)
            {
                PlantModel model = (PlantModel)plant.AbstractInteractableModel;

                if (model.Height.Value == model.NeedHeight)
                {
                    rightHeight++;
                }
            }

            if (rightHeight == 3)
            {
                OnAllHeightRight();
            }
        }

        private void OnAllHeightRight()
        {
            CreateCutscene();

            _cutsceneManger.Play(_plantsCutscene);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _locationsRootView.DreamQuestFirstLocationView.FocusTrigger.OnFucused -= OnFocus;
            _locationsRootView.DreamQuestFirstLocationView.FocusTrigger.OnUnfocused -= OnUnfocus;
        }
    }
}