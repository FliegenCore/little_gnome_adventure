using _Game.Scripts.FSM;
using UnityEngine;

namespace _Game.Scripts.PlayerSystems.MotionStates
{
    public class PlayerAutoMoveMotionState : PlayerMotionState
    {
        protected readonly PlayerModel _playerModel;

        public PlayerAutoMoveMotionState(Fsm fsm, PlayerModel playerModel) : base(fsm, playerModel)
        {
            _playerModel = playerModel;
        }

        public override void Enter()
        {
            _playerModel.AnimationPlayerModel.IsMove.Value = true;
        }

        public override void Exit()
        {
            _playerModel.AnimationPlayerModel.IsMove.Value = false;
            _playerModel.Transformation.Direction.Value = Vector2.zero;
        }

        public override void Update(float deltaTime)
        {
            Vector2 targetPosition = _playerModel.AutoMoveTransform.transform.position;
            Vector2 moveDirection = (targetPosition - _playerModel.Transformation.Position.Value).normalized;
            _playerModel.Transformation.Direction.Value = moveDirection * _playerModel.MoveSpeed;
            
            if (moveDirection.x != 0)
            {
                Vector3 currentScale = _playerModel.Transformation.Scale.Value; 
                currentScale.x = Mathf.Abs(currentScale.x) * (moveDirection.x > 0 ? 1 : -1);
                _playerModel.Transformation.Scale.Value = currentScale;
            }

            if (Vector2.Distance(_playerModel.Transformation.Position.Value, targetPosition) < 0.1f)
            {
                if (_playerModel.LastInteractableObjectTransform != null)
                {
                    Vector2 interactablePos = (Vector2)_playerModel.LastInteractableObjectTransform.position -
                                              _playerModel.Transformation.Position.Value;
                
                    Vector3 currentScale = _playerModel.Transformation.Scale.Value; 
                    currentScale.x = Mathf.Abs(currentScale.x) * (interactablePos.x > 0 ? 1 : -1);
                
                    _playerModel.Transformation.Scale.Value = currentScale;
                }
                
                _playerModel.OnPosition.Execute();
                _playerModel.Transformation.Direction.Value = Vector2.zero;
            }
        }
    }
}