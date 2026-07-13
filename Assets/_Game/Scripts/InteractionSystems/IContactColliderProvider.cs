using System;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems
{
    public interface IContactColliderProvider
    {
        event Action<Collision2D> OnEnter;
        event Action<Collision2D> OnExit;
        event Action<Collision2D> OnStay;
    }
}