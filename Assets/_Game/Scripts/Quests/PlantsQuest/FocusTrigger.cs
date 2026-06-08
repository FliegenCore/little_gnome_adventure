using System;
using _Game.Scripts.CameraSystem;
using _Game.Scripts.PlayerSystems;
using _Game.Scripts.RoomSystems;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Quests.PlantsQuest
{
    public class FocusTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _focusPoint;
        [SerializeField] private float _cameraSize;

        private LocationsControllerFactory _locationsControllerFactory;
        private PlayerFactory _playerFactory;
        private CameraController _cameraController;

        public event Action OnFucused;
        public event Action OnUnfocused;
        
        [Inject]
        private void Construct(
            CameraController cameraController, 
            PlayerFactory playerFactory, 
            LocationsControllerFactory locationsControllerFactory)
        {
            _locationsControllerFactory = locationsControllerFactory;
            _playerFactory    = playerFactory;
            _cameraController = cameraController;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.GetComponent<PlayerView>())
                return;
            
            _cameraController.SetFollowTarget(_focusPoint);

            _cameraController.ZoomTo(_cameraSize, 1, null);
            OnFucused?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.GetComponent<PlayerView>())
                return;
            
            _cameraController.SetFollowTarget(_playerFactory.GetPlayer().PlayerView.transform);
            _cameraController.ZoomTo(5, 1, null);
            OnUnfocused?.Invoke();
        }
    }
}