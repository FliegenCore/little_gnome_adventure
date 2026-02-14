using Unity.Cinemachine;
using UnityEngine;

namespace _Game.Scripts.CameraSystem
{
    public class CameraController
    {
        public readonly CinemachineCamera CurrentCinemachineCamera;
        private readonly CinemachineConfiner2D _cinemachineConfiner2D;
        
        private Transform _currentFollowTarget;
        
        private CameraController(CinemachineCamera cinemachineCamera)
        {
            CurrentCinemachineCamera = cinemachineCamera;   
            _cinemachineConfiner2D = CurrentCinemachineCamera.GetComponent<CinemachineConfiner2D>();
        }

        public void SetFollowTarget(Transform followTarget)
        {
            _currentFollowTarget = followTarget;
            
            CurrentCinemachineCamera.Follow = followTarget;
        }

        public void SetFollowZone(PolygonCollider2D zone)
        {
            _cinemachineConfiner2D.BoundingShape2D = zone;
        }
    }
}