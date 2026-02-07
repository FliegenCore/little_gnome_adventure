using System;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems
{
    public interface IContactTriggerProvider
    {
        event Action<Collider2D> OnEnter;
        event Action<Collider2D> OnExit;
        event Action<Collider2D> OnStay;
    }
}