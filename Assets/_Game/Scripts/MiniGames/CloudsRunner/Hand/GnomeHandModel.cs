using _Game.Scripts.Input;
using Game.PlayerSystem;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GnomeHandModel
    {
        public readonly Transformation Transformation;
        public readonly IMoveDirectionInput MoveDirectionInput;
        public readonly float MoveSpeed;
        
        
        public GnomeHandModel(Transformation transformation, IMoveDirectionInput moveDirectionInput, float moveSpeed)
        {
            MoveSpeed          = moveSpeed;
            MoveDirectionInput = moveDirectionInput;
            Transformation     = transformation;
        }

    }
}