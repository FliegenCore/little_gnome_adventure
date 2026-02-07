using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Input
{
    public interface IMoveDirectionInput
    {
        bool GetCanMove();
        void SetCanMove(bool canMove);
        Vector2 GetDirection();
    }
}