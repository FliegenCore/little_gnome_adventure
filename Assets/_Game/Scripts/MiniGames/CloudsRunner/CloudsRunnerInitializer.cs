using _Game.Scripts.CameraSystem;
using _Game.Scripts.MiniGames.CloudsRunner.Hand;
using _Game.Scripts.MiniGames.CloudsRunner.Hand.States;
using _Game.Scripts.PlayerSystems;
using UnityEngine;

namespace _Game.Scripts.MiniGames.CloudsRunner
{
    public class CloudsRunnerInitializer
    {
        private readonly GnomeHandFactory _gnomeHandFactory;
        private readonly IPlayerFactory _playerFactory;
        
        public CloudsRunnerInitializer(
            GnomeHandFactory gnomeHandFactory,
            IPlayerFactory playerFactory
            )
        {
            _playerFactory    =  playerFactory;
            _gnomeHandFactory = gnomeHandFactory;
        }
        
        public void Initialize()
        {
            _playerFactory.GetPlayer().PlayerModel.IsActive.Value = false;
            GnomeHand hand = _gnomeHandFactory.Create(Vector2.zero);
            hand.SetState<GnomeHandIdleState>();
            
            hand.HandModel.MoveDirectionInput.SetCanMove(true);
        }
    }
}