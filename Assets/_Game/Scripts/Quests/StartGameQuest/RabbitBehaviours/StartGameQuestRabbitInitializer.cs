using System.Collections.Generic;
using _Game.Scripts.Characters.Rabbit.OtherComponents;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.Quests.StartGameQuest.Rabbit.States;
using _Game.Scripts.RoomSystems;
using UnityEngine;

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
            _movePointTransforms.Add(currentMovePointTransform);

            int index = 0;

            foreach (var triggerProvider in _triggerProviders)
            {
                if (currentMovePointTransform == null)
                {
                    TriggerFsmStateEnabler triggerFsmStateEnabler = new TriggerFsmStateEnabler(
                        _rabbit.StateMachine,
                        triggerProvider, 
                        typeof(RabbitJumpState), 
                        true
                    );
                }
                else
                {
                    TriggerFsmStateEnabler triggerFsmStateEnabler = new TriggerFsmStateEnabler(
                        _rabbit.StateMachine,
                        triggerProvider, 
                        typeof(RabbitAutoWalkState), 
                        true
                        );

                    Transform movePoint = _movePointTransforms[index].transform;
                    triggerFsmStateEnabler.PrepareSetState += () =>
                    {
                        _rabbit.RabbitModel.AutoMovePoint = movePoint;
                    };

                    currentMovePointTransform.TryGetNextPoint(out var nextMovePointTransform);
                    
                    currentMovePointTransform = nextMovePointTransform;
                    
                    
                    _movePointTransforms.Add(currentMovePointTransform);
                    index++;
                }
            }
        }
    }
}