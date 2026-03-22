using System;
using System.Collections.Generic;
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
        private readonly ForestLocationsRootView _forestLocationsRootView;
        private List<Plant> _plants;
        
        public PlantsQuestManager(EventBus eventBus, ForestLocationsRootView locationsRootView)
        {
            _forestLocationsRootView = locationsRootView;
            _eventBus                = eventBus;
            _plants                  = new();
        }

        public void Initialize()
        {
            CreatePlants();
        }

        private void CreatePlants()
        {
            DreamQuestFirstLocationView dreamQuestFirstLocationView =
                _forestLocationsRootView.DreamQuestFirstLocationView;
            
            Plant boot = CreatePlant(dreamQuestFirstLocationView.BootPlant, "Boot", 2);
            Plant cactus = CreatePlant(dreamQuestFirstLocationView.CactusPlant, "Cactus", 4);
            Plant column = CreatePlant(dreamQuestFirstLocationView.ColumnPlant, "Column", 3);

            _plants.Add(boot);
            _plants.Add(cactus);
            _plants.Add(column);
        }

        private Plant CreatePlant(PlantView plantView, string id, int neededHeight)
        {
            PlantModel plantModels = new PlantModel(plantView.ContactTriggerProvider, plantView.Position, id, neededHeight);
            plantView.Construct(plantModels.Height, plantModels.CanInteract, plantModels.NeedCallback);
            plantView.HintSelect.Construct(_eventBus, plantModels.IsSelected);
            Plant plant = new Plant(plantModels, plantView, _eventBus, new PlantBehaviour(_eventBus, plantModels));
            plantModels.Height.Subscribe(OnHeightChanged).AddTo(_compositeDisposable);
            return plant;
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
            foreach (var plant in _plants)
            {
                PlantModel model = (PlantModel)plant.AbstractInteractableModel;
                model.NeedCallback.Value = false;
                model.CanInteract.Value = false;

                model.Height.Value = 5;
            }
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}