using System.Collections.Generic;
using _Game.Scripts.Characters.Rabbit.OtherComponents;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;
using _Game.Scripts.RoomSystems;

namespace _Game.Scripts.Quests.StartGameQuest.RabbitBehaviours
{
    public class StartGameQuestRabbitInitializer
    {
        private readonly Rabbit.Rabbit _rabbit;
        private readonly MovePointTransform _movePointTransform;
        private readonly LocationsControllerFactory _locationsControllerFactory;
        private readonly IContactTriggerProvider[] _triggerProviders;

        private List<MovePointTransform> _movePointTransforms = new();
        
        public StartGameQuestRabbitInitializer(
            LocationsControllerFactory locationsControllerFactory, 
            Rabbit.Rabbit rabbit,
            MovePointTransform firstMovePointTransform,
            IContactTriggerProvider[] triggerProviders
            )
        {
            _triggerProviders           = triggerProviders;
            _locationsControllerFactory = locationsControllerFactory;
            _movePointTransform         = firstMovePointTransform;
            _rabbit                     = rabbit;
        }

        public void Initialize()
        {
            MovePointTransform currentMovePointTransform = _movePointTransform;
            
            foreach (var triggerProvider in _triggerProviders)
            {
                if (currentMovePointTransform == null)
                {
                    
                }
                else
                {
                    TriggerFsmStateEnabler triggerFsmStateEnabler = new TriggerFsmStateEnabler(
                        _rabbit.StateMachine,
                        triggerProvider, 
                        typeof(RabbitAutoWalkState), 
                        true
                        );

                    triggerFsmStateEnabler.PreareSetState += () =>
                    {
                        _rabbit.RabbitModel.AutoMovePoint = currentMovePointTransform.transform;
                    };

                    if (currentMovePointTransform.TryGetNextPoint(out var nextMovePointTransform))
                    {
                        currentMovePointTransform = nextMovePointTransform;
                    }
                }
            }
        }
    }
}