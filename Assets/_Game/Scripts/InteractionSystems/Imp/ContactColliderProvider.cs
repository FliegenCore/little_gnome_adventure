using System;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems.Imp
{
    public class ContactColliderProvider: MonoBehaviour, IContactColliderProvider
    {
        public event Action<Collision2D> OnEnter;
        public event Action<Collision2D> OnExit;
        public event Action<Collision2D> OnStay;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnEnter?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnExit?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnStay?.Invoke(other);
        }
    }
}