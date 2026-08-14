using System;
using _Game.Scripts.PlayerSystems;
using UnityEngine;
using VContainer;

namespace _Game.Scripts.Common
{
    public class HandlePlayerPosition : MonoBehaviour
    {
        protected event Action _onPlayerRight;
        protected event Action _onPlayerLeft;

        [SerializeField] private Transform _origin;
        
        private IPlayerFactory _playerFactory;
        
        private Vector2 _lastPlayerPosition;
        private EPlayerPosition _playerPosition;    
        
        [Inject]
        private void Construct(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        private void Update()
        {
            if (_playerFactory.GetPlayer() == null)
                return;

            Handle();
        }

        protected void Handle()
        {
            Player player = _playerFactory.GetPlayer();
            Vector2 playerPosition = player.PlayerModel.Transformation.Position.Value;
            Vector2 originPosition = _origin.position;
            
            if(playerPosition == _lastPlayerPosition)
                return;

            _lastPlayerPosition = playerPosition;
            
            //правее
            if (playerPosition.x > originPosition.x && _playerPosition !=  EPlayerPosition.Right)
            {
                _playerPosition = EPlayerPosition.Right;
                _onPlayerRight?.Invoke();
            }
            //левее
            else if (playerPosition.x < originPosition.x && _playerPosition != EPlayerPosition.Left)
            {
                _playerPosition = EPlayerPosition.Left;
                _onPlayerLeft?.Invoke();
            }
        }
        
        private enum EPlayerPosition
        {
            None,
            Left,
            Right
        }
    }
    
    
}