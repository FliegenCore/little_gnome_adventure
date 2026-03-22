using System;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.Quests.PlantsQuest.Impl.Plant;
using Core.Common;

namespace _Game.Scripts.Quests.PlantsQuest.Impl
{
    public class PlantBehaviour : ACustomBehaviour
    {
        private readonly PlantModel _plantModel;
        private bool _isCompleted;
        
        
        public PlantBehaviour(EventBus eventBus, PlantModel plantModel) : base(eventBus)
        {
            _plantModel = plantModel;
        }

        public override bool CanInteract()
        {
            return !_isCompleted && _plantModel.CanInteract.Value;
        }

        public override void Interact()
        {
            _plantModel.CanInteract.Value = false;
            
            int nextValue = _plantModel.Height.Value;

            if (nextValue == _plantModel.MaxHeight)
                nextValue = 1;
            else
                nextValue++;

            _plantModel.Height.Value = nextValue;
        }
        
        private void PlantsQuestComplete()
        {
            _isCompleted = true;
        }
    }
}