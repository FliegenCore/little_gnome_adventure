using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.Quests.StartGameQuest.Rabbit.States
{
    public class RabbitAutoWalkState : RabbitState
    {
        public RabbitAutoWalkState(Fsm fsm, RabbitModel rabbitModel) : base(fsm, rabbitModel)
        {
            
        }

        public override void Enter()
        {
            _rabbitModel.AnimationModel.IsWalkAnimation.Value = true;
        }

        public override void Update(float deltaTime)
        {
            Vector2 targetPosition = _rabbitModel.AutoMovePoint.transform.position;
            Vector2 moveDirection = (targetPosition - _rabbitModel.Transformation.Position.Value).normalized;
            _rabbitModel.Transformation.Direction.Value = moveDirection * _rabbitModel.Speed;
            
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _rabbitModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _rabbitModel.Transformation.Scale.Value = currentScale;
            }

            if (Vector2.Distance(_rabbitModel.Transformation.Position.Value, targetPosition) < 0.1f)
            {
                _rabbitModel.Transformation.Direction.Value = Vector2.zero;
                _fsm.SetState<RabbitIdleState>();
            }
        }

        public override void Exit()
        {
            _rabbitModel.AnimationModel.IsWalkAnimation.Value = false;
        }
    }
}