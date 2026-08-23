using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.InteractionSystems;
using _Game.Scripts.PlayerSystems;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Common.Triggers
{
    public class CameraSizeSetTrigger : ContactTriggerProvider
    {
        [SerializeField] private float _cameraSize; 
        
        private CameraController _cameraController;

        [Inject]
        private void Construct(CameraController cameraController)
        {
            _cameraController = cameraController;
        }
        
        private void OnEnable()
        {
            OnEnter += SetCameraSize;
        }

        private void OnDisable()
        {
            OnEnter -= SetCameraSize;
        }

        private void SetCameraSize(Collider2D collider)
        {
            if (!collider.transform.GetComponent<PlayerView>())
            {
                return;
            }
            
            _cameraController.ZoomTo(_cameraSize, 0.5f, null);
            
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            float previewAspect = 16f / 9f;

            float height = _cameraSize * 2f;
            float width = height * previewAspect;

            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
            
            Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
            Gizmos.DrawCube(transform.position, new Vector3(width, height, 0f));
        }
    }
}