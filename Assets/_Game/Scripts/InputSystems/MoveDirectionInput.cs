using UnityEngine;

namespace _Game.Scripts.Input
{
    public class MoveDirectionInput : IMoveDirectionInput
    {
        private InputSystem_Actions _inputSystemActions;
        
        private bool _canMove;
        private bool _isSprint;
        
        public MoveDirectionInput(InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _inputSystemActions.Enable();
            
            _inputSystemActions.Player.Sprint.started += _ => SetIsSprint(true);
            _inputSystemActions.Player.Sprint.canceled += _ => SetIsSprint(false);
        }

        public bool GetCanMove()
        {
            return _canMove;
        }

        public bool GetIsSprint()
        {
            return _isSprint;
        }
        
        private void SetIsSprint(bool value)
        {
            _isSprint = value;
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