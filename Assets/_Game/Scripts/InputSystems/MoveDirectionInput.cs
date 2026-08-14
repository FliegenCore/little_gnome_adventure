using System;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public class MoveDirectionInput : IMoveDirectionInput
    {
        public event Action JumpEvent;
        public event Action OnStartPlayerMoved;

        private InputSystem_Actions _inputSystemActions;
        
        private bool _canMove;
        private bool _isSprint;
        private bool _isMove;
        
        public MoveDirectionInput(InputSystem_Actions inputSystemActions)
        {
            _inputSystemActions = inputSystemActions;
            _inputSystemActions.Enable();
            
            _inputSystemActions.Player.Sprint.started += _ => SetIsSprint(true);
            _inputSystemActions.Player.Sprint.canceled += _ => SetIsSprint(false);
            _inputSystemActions.Player.Jump.started += _ => JumpEvent?.Invoke();
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
            if (_inputSystemActions.Player.Move.ReadValue<Vector2>() != Vector2.zero && !_isMove)
            {
                OnStartPlayerMoved?.Invoke();
                _isMove = true;
            }
            else if(_inputSystemActions.Player.Move.ReadValue<Vector2>() == Vector2.zero && _isMove)
                _isMove = false;
            
            return _inputSystemActions.Player.Move.ReadValue<Vector2>();
        }
    }
}