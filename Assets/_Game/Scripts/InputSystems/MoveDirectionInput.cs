using UnityEngine;

namespace _Game.Scripts.Input
{
    public class MoveDirectionInput : IMoveDirectionInput
    {
        private InputSystem_Actions _inputSystemActions;
        
        private bool _canMove;
        
        public MoveDirectionInput(InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _inputSystemActions.Enable();
        }

        public bool GetCanMove()
        {
            return _canMove;
        }

        public void SetCanMove(bool canMove)
        {
            _canMove = canMove;
        }

        public Vector2 GetDirection()
        {
            return _inputSystemActions.Player.Move.ReadValue<Vector2>();
        }
    }
}