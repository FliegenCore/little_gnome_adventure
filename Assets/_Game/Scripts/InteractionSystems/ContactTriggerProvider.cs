using System;
using UnityEngine;

namespace _Game.Scripts.InteractionSystems
{
    public class ContactTriggerProvider : MonoBehaviour, IContactTriggerProvider
    {
        public event Action<Collider2D> OnEnter;
        public event Action<Collider2D> OnExit;
        public event Action<Collider2D> OnStay;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnExit?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnStay?.Invoke(other);
        }
    }
}