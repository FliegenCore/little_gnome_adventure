using System;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public interface IMoveDirectionInput
    {
        event Action JumpEvent;
        event Action OnStartPlayerMoved;
        
        bool GetCanMove();
        bool GetIsSprint();
        void SetCanMove(bool canMove);
        Vector2 GetDirection();
    }
}