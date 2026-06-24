using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _Game.Scripts.CameraSystem
{
    public class CameraController
    {
        public readonly CinemachineCamera CurrentCinemachineCamera;
        private readonly CinemachineConfiner2D _cinemachineConfiner2D;
        
        private Transform _currentFollowTarget;

        private Tween _zoomTween;
        
        private CameraController(CinemachineCamera cinemachineCamera)
        {
            CurrentCinemachineCamera = cinemachineCamera;   
            _cinemachineConfiner2D = CurrentCinemachineCamera.GetComponent<CinemachineConfiner2D>();
        }
        
        public void ZoomTo(float newSize, float duration, Action callback)
        {
            _zoomTween?.Kill();
            
            _zoomTween = DOTween.To(
                () => CurrentCinemachineCamera.Lens.OrthographicSize,
                x => CurrentCinemachineCamera.Lens.OrthographicSize = x,
                newSize,
                duration
            );
            
            _zoomTween.OnComplete(() => callback?.Invoke());
        }
        
        public void SetFollowTarget(Transform followTarget)
        {
            _currentFollowTarget = followTarget;
            
            CurrentCinemachineCamera.Follow = _currentFollowTarget;
        }

        public void SetFollowZone(PolygonCollider2D zone)
        {
            _cinemachineConfiner2D.BoundingShape2D = zone;
        }
    }
}