using System;
using _Game.Scripts.PlayerSystems.Animations.Impl.Behaviours;
using _Game.Scripts.Quests.PlantsQuest.Impl.Plant;
using Core.Common;

namespace _Game.Scripts.Quests.PlantsQuest.Impl
{
    public class PlantBehaviour : ACustomBehaviour
    {
        private readonly PlantModel _plantModel;
        
        public PlantBehaviour(EventBus eventBus, PlantModel plantModel) : base(eventBus)
        {
            _plantModel = plantModel;
        }

        public override bool CanInteract()
        {
            return _plantModel.CanInteract.Value;
        }

        public override void Interact(Action callback)
        {
            int nextValue = _plantModel.Height.Value;

            if (nextValue == _plantModel.MaxHeight)
                nextValue = 1;
            else
                nextValue++;

            _plantModel.Height.Value = nextValue;
        }
    }
}