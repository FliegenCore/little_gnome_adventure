using System;
using _Game.Scripts.InteractionSystems;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner.Hand
{
    public class GroundChecker : IDisposable
    {
        public event Action<bool> OnGroundChange; 
        
        private readonly IContactColliderProvider _contactColliderProvider;
        private readonly IContactTriggerProvider _contactTriggerProvider;

        private bool _onTriggered;
        private bool _onCollided;

        private bool _onGround;

        public bool OnGround => _onGround;
        
        public GroundChecker(IContactColliderProvider colliderProvider, IContactTriggerProvider contactTriggerProvider)
        {
            _contactColliderProvider = colliderProvider;
            _contactTriggerProvider = contactTriggerProvider;

            _contactColliderProvider.OnEnter += OnEnterCollider;
            _contactColliderProvider.OnExit += OnExitCollider;
            
            _contactTriggerProvider.OnEnter += OnEnterTrigger;
            _contactTriggerProvider.OnExit += OnExitTrigger;
        }

        private void OnEnterTrigger(Collider2D _)
        {
            Debug.Log("OnEnterTrigger");
            _onTriggered = true;
            
            ChangeOnGround();
        }

        private void OnExitTrigger(Collider2D _)
        {
            Debug.Log("OnExitTrigger");
            _onTriggered = false;
            
            ChangeOnGround();
        }

        private void OnEnterCollider(Collision2D _)
        {
            Debug.Log("OnEnterCollider");
            _onCollided = true;
            
            ChangeOnGround();
        }

        private void OnExitCollider(Collision2D _)
        {
            Debug.Log("OnExitCollider");
            
            _onCollided = false;

            ChangeOnGround();
        }

        private void ChangeOnGround()
        {
            if (_onTriggered && _onCollided)
            {
                _onGround = true;
            }
            else
            {
                _onGround = false;
            }
            
            OnGroundChange?.Invoke(_onGround);
        }

        public void Dispose()
        {
            _contactColliderProvider.OnEnter -= OnEnterCollider;
            _contactColliderProvider.OnExit -= OnExitCollider;
        }
    }
}